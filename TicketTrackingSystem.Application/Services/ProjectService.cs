using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TicketTrackingSystem.Application.Dto;
using TicketTrackingSystem.Application.Interface;
using TicketTrackingSystem.Application.Model;
using TicketTrackingSystem.Common.ExtensionMethod;
using TicketTrackingSystem.Common.Model;
using TicketTrackingSystem.Core.Model;
using TicketTrackingSystem.Core.Model.Enum;
using TicketTrackingSystem.DAL.Interface;

namespace TicketTrackingSystem.Application.Services;
public class ProjectService : IProjectService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public ProjectService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    // GetAllProjectPaginatedAsync
    public async Task<Result<DataTablesResponse<ProjectDto>>> GetAllProjectPaginatedAsync(DataTablesRequest request)
    {
        try
        {
            var query = _unitOfWork.Projects.GetAllAsQueryable().AsNoTracking();
            if (!string.IsNullOrEmpty(request.Search?.Value))
            {
                var searchValue = request.Search.Value.ToLower();
                query = query.Where(p => p.Name.ToLower().Contains(searchValue)
                                    || p.Description.ToLower().Contains(searchValue));
            }
            // Apply ordering
            if (request.Order != null && request.Order.Any())
            {
                var order = request.Order.First();
                var columnName = request.Columns[order.Column].Data;
                var direction = order.Dir;
                // Dynamically apply ordering
                query = direction == "asc"
                    ? query.OrderByDynamic(columnName, true)
                    : query.OrderByDynamic(columnName, false);
            }
            // Get the total count before pagination
            var recordsTotal = await query.CountAsync();
            // Apply pagination
            var paginatedData = await query
                .Skip(request.Start)
                .Take(request.Length)
                .ToListAsync();
            // Now project the roles separately in the mapping phase
            var projectDtos = _mapper.Map<IEnumerable<ProjectDto>>(paginatedData);
            var response = new DataTablesResponse<ProjectDto>
            {
                Draw = request.Draw,
                RecordsTotal = recordsTotal,
                RecordsFiltered = recordsTotal,
                Data = projectDtos
            };
            return Result<DataTablesResponse<ProjectDto>>.Success(response);
        }
        catch (Exception ex)
        {

            return Result<DataTablesResponse<ProjectDto>>.Failure(ex.Message);
        }
    }

    //get project by id
    public async Task<Result<ProjectDto>> GetProjectByIdAsync(Guid id)
    {
        var project = await _unitOfWork.Projects.GetByIdAsync(id);
        if (project == null)
            return Result<ProjectDto>.Failure("There is no project with this id.");
        return Result<ProjectDto>.Success(_mapper.Map<ProjectDto>(project));
    }

    // Add project
    public async Task<Result<ProjectDto>> CreateProjectAsync(CreateProjectDto newProject)
    {
        var project = new Project
        {
            Name = newProject.Name,
            Description = newProject.Description,
            CreatedAt = DateTime.Now
        };
        await _unitOfWork.Projects.AddAsync(project);
        try
        {
            await _unitOfWork.CompleteAsync();
            return Result<ProjectDto>.Success(_mapper.Map<ProjectDto>(project));
        }
        catch (Exception ex)
        {

            return Result<ProjectDto>.Failure(ex.Message);
        }
    }
    // update the project
    public async Task<Result<ProjectDto>> UpdateProjectAsync(UpdateProjectDto projectDto)
    {
        try
        {
            var project = await _unitOfWork.Projects.GetByIdAsync(projectDto.Id);
            if (project == null)
                return Result<ProjectDto>.Failure("There is no product with this id.");
            project.Name = projectDto.Name;
            project.Description = projectDto.Description;
            _unitOfWork.Projects.Update(project);
            await _unitOfWork.CompleteAsync();
            return Result<ProjectDto>.Success(_mapper.Map<ProjectDto>(project));
        }
        catch (Exception ex)
        {

            return Result<ProjectDto>.Failure(ex.Message);
        }
    }
    // Delete the project
    public async Task<Result<string>> DeleteProjectAsync(Guid id)
    {
        try
        {
            var project = await _unitOfWork.Projects.GetByIdAsync(id);
            if (project == null)
                return Result<string>.Failure("There is no product with this id.");
            _unitOfWork.Projects.Remove(project);
            await _unitOfWork.CompleteAsync();
            return Result<string>.Success("Project deleted successfully.");
        }
        catch (Exception ex)
        {
            return Result<string>.Failure(ex.Message);
        }
    }

    public async Task<Result<DataTablesResponse<ProjectDto>>> GetAllUserProjectsAsync(DataTablesRequest request, Guid clientId)
    {
        try
        {
            if (clientId == Guid.Empty)
                return Result<DataTablesResponse<ProjectDto>>.Failure("Client id is not valid.");
            var user = await _unitOfWork.Users.GetByIdAsync(clientId);
            if (user == null)
                return Result<DataTablesResponse<ProjectDto>>.Failure("There is no user with this id.");
            var query = _unitOfWork.ProjectMembers.GetAllAsQueryable().Where(p => p.UserId == clientId);
            if (!string.IsNullOrEmpty(request.Search?.Value))
            {
                var searchValue = request.Search.Value.ToLower();
                query = query.Where(p => p.Project.Name.ToLower().Contains(searchValue)
                                    || p.Project.Description.ToLower().Contains(searchValue));
            }
            // Apply ordering
            // Apply ordering
            //if (request.Order != null && request.Order.Any())
            //{
            //    var order = request.Order.First();
            //    var columnName = request.Columns[order.Column].Data;
            //    var direction = order.Dir;
            //    // Dynamically apply ordering
            //    query = direction == "asc"
            //        ? query.OrderByDynamic(columnName, true)
            //        : query.OrderByDynamic(columnName, false);
            //}
            // Get the total count before pagination
            var recordsTotal = await query.CountAsync();
            var paginatedData = await query
                        .Include(p => p.Project)
                        .Select(p => p.Project)
                        .Skip(request.Start)
                        .Take(request.Length)
                        .ToListAsync();
            var projectDtos = _mapper.Map<IEnumerable<ProjectDto>>(paginatedData);
            var response = new DataTablesResponse<ProjectDto>
            {
                Draw = request.Draw,
                RecordsTotal = recordsTotal,
                RecordsFiltered = recordsTotal,
                Data = projectDtos
            };

            return Result<DataTablesResponse<ProjectDto>>.Success(response);
        }
        catch (Exception ex)
        {
            return Result<DataTablesResponse<ProjectDto>>.Failure(ex.Message);
        }
    }

    //set user for project
    public async Task<Result<string>> SetUserForProjectAsync(Guid userId, Guid projectId, int? stage)
    {
        try
        {
            var user = await _unitOfWork.Users.GetByIdAsync(userId);
            if (user == null)
                return Result<string>.Failure("There is no user with this id.");
            var project = await _unitOfWork.Projects.GetByIdAsync(projectId);
            if (project == null)
                return Result<string>.Failure("There is no project with this id.");
            var projectMember = await _unitOfWork.ProjectMembers.GetByIdAsync(userId, projectId);
            if (projectMember != null)
                return Result<string>.Failure("User is already assigned to this project.");

            if (user.UserType.Equals(UserType.Client))
            {
                projectMember = new ProjectMember
                {
                    UserId = userId,
                    ProjectId = projectId,
                    JoinDate = DateTime.Now,
                    Stage = Stage.NoStage,
                };
            }
            else
            {
                if (!Enum.IsDefined(typeof(Stage), stage))
                    return Result<string>.Failure("Invalid stage value.");

                projectMember = new ProjectMember
                {
                    UserId = userId,
                    ProjectId = projectId,
                    JoinDate = DateTime.Now,
                    Stage = (Stage)stage,
                };
            }
            await _unitOfWork.ProjectMembers.AddAsync(projectMember);
            await _unitOfWork.CompleteAsync();
            return Result<string>.Success("User assigned to project successfully.");
        }
        catch (Exception ex)
        {
            return Result<string>.Failure(ex.Message);
        }
    }
    //remove user from the project
    public async Task<Result<string>> RemoveUserFromProjectAsync(Guid userId, Guid projectId)
    {
        try
        {
            var projectMember = await _unitOfWork.ProjectMembers.GetAllAsQueryable().FirstOrDefaultAsync(c => c.UserId == userId && c.ProjectId == projectId);

            if (projectMember == null)
                return Result<string>.Failure("User is not assigned to this project.");
            _unitOfWork.ProjectMembers.Remove(projectMember);
            await _unitOfWork.CompleteAsync();
            return Result<string>.Success("User removed from project successfully.");
        }
        catch (Exception ex)
        {
            return Result<string>.Failure(ex.Message);
        }
    }
    public string GetStageDropdown()
    {
        var stage = Enum.GetValues(typeof(Stage)).Cast<Stage>().Select(v => new SelectListItem
        {
            Value = ((int)v).ToString(),
            Text = v.ToString()
        }).ToList();

        var htmlString = new System.Text.StringBuilder();
        foreach (var item in stage)
        {
            htmlString.Append($"<option value='{item.Value}'>{item.Text}</option>");
        }
        return htmlString.ToString();
    }
}
