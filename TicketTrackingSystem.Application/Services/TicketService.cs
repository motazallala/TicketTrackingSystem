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
public class TicketService : ITicketService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public TicketService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<DataTablesResponse<TicketDto>>> GetAllTicketForUserPaginatedAsync(DataTablesRequest request, Guid projectId, Guid userId)
    {
        try
        {
            var query = _unitOfWork.Tickets.GetAllAsQueryable().AsNoTracking();
            var user = await _unitOfWork.Users.GetByIdAsync(userId);
            if (user.UserType.Equals(UserType.Client) || _unitOfWork.Users.IsInRole(user, "Admin"))
            {
                query = query.Where(p => p.ProjectId == projectId);
            }
            else
            {
                var member = await _unitOfWork.ProjectMembers.GetAllAsQueryable().FirstOrDefaultAsync(p => p.ProjectId == projectId && p.UserId == userId);
                query = query.Where(p => p.Stage.Equals(member.Stage));
            }
            if (!string.IsNullOrEmpty(request.Search?.Value))
            {
                var searchValue = request.Search.Value.ToLower();
                query = query.Where(p => p.Title.ToLower().Contains(searchValue) || p.Description.ToLower().Contains(searchValue));
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
            var departmentDtos = _mapper.Map<IEnumerable<TicketDto>>(paginatedData);
            var response = new DataTablesResponse<TicketDto>
            {
                Draw = request.Draw,
                RecordsTotal = recordsTotal,
                RecordsFiltered = recordsTotal,
                Data = departmentDtos
            };
            return Result<DataTablesResponse<TicketDto>>.Success(response);
        }
        catch (Exception ex)
        {

            return Result<DataTablesResponse<TicketDto>>.Failure(ex.Message);
        }
    }

    // get all paginated tickets
    public async Task<Result<DataTablesResponse<TicketDto>>> GetAllTicketPaginatedAsync(DataTablesRequest request, Guid projectId)
    {
        try
        {
            var query = _unitOfWork.Tickets.GetAllAsQueryable().AsNoTracking().Where(p => p.ProjectId == projectId);
            if (!string.IsNullOrEmpty(request.Search?.Value))
            {
                var searchValue = request.Search.Value.ToLower();
                query = query.Where(p => p.Title.ToLower().Contains(searchValue) || p.Description.ToLower().Contains(searchValue));
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
            var departmentDtos = _mapper.Map<IEnumerable<TicketDto>>(paginatedData);
            var response = new DataTablesResponse<TicketDto>
            {
                Draw = request.Draw,
                RecordsTotal = recordsTotal,
                RecordsFiltered = recordsTotal,
                Data = departmentDtos
            };
            return Result<DataTablesResponse<TicketDto>>.Success(response);
        }
        catch (Exception ex)
        {

            return Result<DataTablesResponse<TicketDto>>.Failure(ex.Message);
        }
    }

    // get ticket by id
    public async Task<Result<TicketDto>> GetTicketByIdAsync(Guid id)
    {
        try
        {
            var ticket = await _unitOfWork.Tickets.GetByIdAsync(id);
            if (ticket == null)
            {
                return Result<TicketDto>.Failure("Ticket not found");
            }
            var ticketDto = _mapper.Map<TicketDto>(ticket);
            return Result<TicketDto>.Success(ticketDto);
        }
        catch (Exception ex)
        {
            return Result<TicketDto>.Failure(ex.Message);
        }
    }
    // add new ticket
    public async Task<Result<TicketDto>> AddTicketAsync(CreateTicketDto ticketDto)
    {
        try
        {
            var user = await _unitOfWork.Users.GetByIdAsync(ticketDto.CreatorId);
            if (user == null)
            {
                return Result<TicketDto>.Failure("User not found");
            }
            if (!user.UserType.Equals(UserType.Client))
            {
                return Result<TicketDto>.Failure("Only clients can create tickets");
            }
            var project = await _unitOfWork.Projects.GetByIdAsync(ticketDto.ProjectId);
            if (project == null)
            {
                return Result<TicketDto>.Failure("Project not found");
            }
            var ticket = new Ticket
            {
                CreatorId = ticketDto.CreatorId,
                ProjectId = ticketDto.ProjectId,
                Title = ticketDto.Title,
                Description = ticketDto.Description,
            };
            ticket.CreatedAt = DateTime.Now;
            ticket.UpdatedAt = DateTime.Now;
            await _unitOfWork.Tickets.AddAsync(ticket);
            await _unitOfWork.CompleteAsync();
            return Result<TicketDto>.Success(_mapper.Map<TicketDto>(ticket));
        }
        catch (Exception ex)
        {
            return Result<TicketDto>.Failure(ex.Message);
        }
    }

    public async Task<Result<TicketDto>> UpdateTicketStatusWithAutoStageAsync(Guid id, int status, bool isFinished)
    {
        try
        {
            var ticket = await _unitOfWork.Tickets.GetByIdAsync(id);
            if (ticket == null)
            {
                return Result<TicketDto>.Failure("Ticket not found");
            }

            // Check if the status value is valid
            if (!Enum.IsDefined(typeof(TicketStatus), status))
            {
                return Result<TicketDto>.Failure("Invalid status value");
            }
            if (ticket.Stage.Equals(Stage.Stage1))
            {
                ticket.Status = (TicketStatus)status; // Explicitly cast the status to TicketStatus
                ticket.Stage = Stage.Stage2; // Explicitly cast the stage to Stage

            }
            else if (ticket.Stage.Equals(Stage.Stage2))
            {
                if (isFinished)
                {
                    ticket.Status = TicketStatus.Closed;
                    ticket.Stage = Stage.NoStage; // Explicitly cast the stage to Stage

                }
                else
                {
                    ticket.Status = (TicketStatus)status; // Explicitly cast the status to TicketStatus
                    ticket.Stage = Stage.Stage2; // Explicitly cast the stage to Stage
                }
            }
            else
            {
                return Result<TicketDto>.Failure("Ticket is already closed");
            }

            ticket.UpdatedAt = DateTime.Now;
            _unitOfWork.Tickets.Update(ticket);
            await _unitOfWork.CompleteAsync();
            return Result<TicketDto>.Success(_mapper.Map<TicketDto>(ticket));
        }
        catch (Exception ex)
        {
            return Result<TicketDto>.Failure(ex.Message);
        }
    }
    // update ticket status
    public async Task<Result<TicketDto>> UpdateTicketStatusAsync(Guid id, int status)
    {
        try
        {
            var ticket = await _unitOfWork.Tickets.GetByIdAsync(id);
            if (ticket == null)
            {
                return Result<TicketDto>.Failure("Ticket not found");
            }

            // Check if the status value is valid
            if (!Enum.IsDefined(typeof(TicketStatus), status))
            {
                return Result<TicketDto>.Failure("Invalid status value");
            }

            ticket.Status = (TicketStatus)status; // Explicitly cast the status to TicketStatus
            ticket.UpdatedAt = DateTime.Now;
            _unitOfWork.Tickets.Update(ticket);
            await _unitOfWork.CompleteAsync();
            return Result<TicketDto>.Success(_mapper.Map<TicketDto>(ticket));
        }
        catch (Exception ex)
        {
            return Result<TicketDto>.Failure(ex.Message);
        }
    }
    // update ticket stage
    public async Task<Result<TicketDto>> UpdateTicketStageAsync(Guid id, int stage)
    {
        try
        {
            var ticket = await _unitOfWork.Tickets.GetByIdAsync(id);
            if (ticket == null)
            {
                return Result<TicketDto>.Failure("Ticket not found");
            }
            // Check if the stage value is valid
            if (!Enum.IsDefined(typeof(Stage), stage))
            {
                return Result<TicketDto>.Failure("Invalid stage value");
            }
            ticket.Stage = (Stage)stage; // Explicitly cast the stage to Stage
            ticket.UpdatedAt = DateTime.Now;
            _unitOfWork.Tickets.Update(ticket);
            await _unitOfWork.CompleteAsync();
            return Result<TicketDto>.Success(_mapper.Map<TicketDto>(ticket));
        }
        catch (Exception ex)
        {
            return Result<TicketDto>.Failure(ex.Message);
        }
    }
    // update ticket message
    public async Task<Result<TicketDto>> UpdateTicketMessageAsync(Guid id, string message)
    {
        try
        {
            var ticket = await _unitOfWork.Tickets.GetByIdAsync(id);
            if (ticket == null)
            {
                return Result<TicketDto>.Failure("Ticket not found");
            }
            ticket.Message = message;
            ticket.UpdatedAt = DateTime.Now;
            _unitOfWork.Tickets.Update(ticket);
            await _unitOfWork.CompleteAsync();
            return Result<TicketDto>.Success(_mapper.Map<TicketDto>(ticket));
        }
        catch (Exception ex)
        {
            return Result<TicketDto>.Failure(ex.Message);
        }
    }

    public string GetTicketStatusDropdown()
    {
        var stage = Enum.GetValues(typeof(TicketStatus)).Cast<TicketStatus>().Select(v => new SelectListItem
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
