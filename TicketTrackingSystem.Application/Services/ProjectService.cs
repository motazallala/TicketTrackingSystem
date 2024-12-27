using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TicketTrackingSystem.Application.Dto;
using TicketTrackingSystem.Application.Interface;
using TicketTrackingSystem.Application.Model;
using TicketTrackingSystem.Common.ExtensionMethod;
using TicketTrackingSystem.Common.Model;
using TicketTrackingSystem.Core.Model;
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

}
