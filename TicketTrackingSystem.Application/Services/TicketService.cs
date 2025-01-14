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

    //assinge ticket to user 
    public async Task<Result<TicketDto>> AssignTicketToUserAsync(Guid ticketId, Guid userId)
    {
        try
        {
            var ticket = await _unitOfWork.Tickets.GetByIdAsync(ticketId);
            if (ticket == null)
            {
                return Result<TicketDto>.Failure("Ticket not found");
            }
            var user = await _unitOfWork.Users.GetByIdAsync(userId);
            if (user == null)
            {
                return Result<TicketDto>.Failure("User not found");
            }

            var member = await _unitOfWork.ProjectMembers.GetAllAsQueryable().FirstOrDefaultAsync(p => p.ProjectId == ticket.ProjectId && p.UserId == userId);
            if (member == null)
            {
                return Result<TicketDto>.Failure("User is not a member of the project");
            }
            if (ticket.Status.Equals(TicketStatus.Assigned))
            {
                return Result<TicketDto>.Failure("The Ticket is already taken.");
            }
            if (member.Stage.Equals(Stage.Stage1))
            {
                await _unitOfWork.TicketHistory.AddAsync(new TicketHistory
                {
                    UserId = user.Id,
                    TicketId = ticket.Id,
                    StageBeforeChange = ticket.Stage,
                    StageAfterChange = Stage.Stage1
                });
                ticket.Status = TicketStatus.Assigned;
                ticket.Stage = Stage.Stage1;
            }
            else if (member.Stage.Equals(Stage.Stage2))
            {
                await _unitOfWork.TicketHistory.AddAsync(new TicketHistory
                {
                    UserId = user.Id,
                    TicketId = ticket.Id,
                    StageBeforeChange = ticket.Stage,
                    StageAfterChange = Stage.Stage2
                });
                ticket.Status = TicketStatus.Assigned;
                ticket.Stage = Stage.Stage2;
            }
            else
            {
                return Result<TicketDto>.Failure("User is not in the stage one or two");
            }

            ticket.UpdatedAt = DateTime.UtcNow;
            await _unitOfWork.CompleteAsync();
            return Result<TicketDto>.Success(_mapper.Map<TicketDto>(ticket));
        }
        catch (Exception ex)
        {
            return Result<TicketDto>.Failure(ex.Message);
        }
    }
    public async Task<Result<DataTablesResponse<TicketDto>>> GetAllTicketForMemberPaginatedAsync(DataTablesRequest request, Guid projectId, Guid userId, bool reserved)
    {
        try
        {


            var query = _unitOfWork.Tickets.GetAllAsQueryable().AsNoTracking();
            var user = await _unitOfWork.Users.GetByIdAsync(userId);
            if (_unitOfWork.Users.IsInRole(user, "Admin"))
            {
                query = query.Where(p => p.ProjectId == projectId);
            }
            else
            {
                var member = await _unitOfWork.ProjectMembers.GetAllAsQueryable().FirstOrDefaultAsync(p => p.ProjectId == projectId && p.UserId == userId);
                if (!reserved)
                {
                    if (member.Stage.Equals(Stage.Stage1))
                    {
                        query = query.Where(p => p.Stage.Equals(member.Stage) && p.ProjectId == member.ProjectId && p.Status.Equals(TicketStatus.Pending));
                    }
                    else if (member.Stage.Equals(Stage.Stage2))
                    {
                        query = query.Where(p => p.Stage.Equals(member.Stage) && p.ProjectId == member.ProjectId && p.Status.Equals(TicketStatus.InProgress));
                    }
                    else
                    {
                        return Result<DataTablesResponse<TicketDto>>.Failure("The member is not in the stage one or two");
                    }

                }
                else
                {
                    query = query.Where(p => p.Stage.Equals(member.Stage) && p.ProjectId == member.ProjectId && (p.Status.Equals(TicketStatus.Assigned) || p.Status.Equals(TicketStatus.Returned)) && p.TicketHistories.Any(u => u.UserId == user.Id));
                }
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

    public async Task<Result<DataTablesResponse<TicketDto>>> GetAllTicketForUserPaginatedAsync(DataTablesRequest request, Guid projectId, Guid userId)
    {
        try
        {
            var query = _unitOfWork.Tickets.GetAllAsQueryable().AsNoTracking();
            var user = await _unitOfWork.Users.GetByIdAsync(userId);
            if (_unitOfWork.Users.IsInRole(user, "Admin"))
            {
                query = query.Where(p => p.ProjectId == projectId);
            }
            else if (user.UserType.Equals(UserType.Client))
            {
                query = query.Where(p => p.CreatorId == userId && p.ProjectId == projectId);
            }
            else
            {
                var member = await _unitOfWork.ProjectMembers.GetAllAsQueryable().FirstOrDefaultAsync(p => p.ProjectId == projectId && p.UserId == userId);
                //in this line the bug
                query = query.Where(p => p.Stage.Equals(member.Stage) && p.ProjectId == member.ProjectId);
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

    public async Task<Result<TicketDto>> CheckIfUserHas(Guid id)
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

    //new code
    public async Task<Result<TicketDto>> UpdateTicketWithAutoStageAsync(Guid id, string status, bool isFinished, Guid userId, string message = null)
    {
        try
        {
            var ticket = await _unitOfWork.Tickets.GetByIdAsync(id);
            var user = await _unitOfWork.Users.GetByIdAsync(userId);
            if (user == null)
            {
                return Result<TicketDto>.Failure("User not found");
            }
            if (ticket == null)
            {
                return Result<TicketDto>.Failure("Ticket not found");
            }

            if (isFinished)
            {
                if (status.Equals("accept"))
                {
                    ticket.Status = TicketStatus.Completed;
                }
                else
                {
                    if (ticket.Status.Equals(TicketStatus.Returned))
                    {
                        return Result<TicketDto>.Failure("The returned tickets can not be rejected");
                    }
                    ticket.Status = TicketStatus.Rejected;
                }

                await _unitOfWork.TicketHistory.AddAsync(new TicketHistory
                {
                    UserId = user.Id,
                    TicketId = ticket.Id,
                    StageBeforeChange = ticket.Stage,
                    StageAfterChange = Stage.NoStage
                });
                if (ticket.Stage.Equals(Stage.Stage1))
                {
                    await _unitOfWork.TicketMessage.AddAsync(new TicketMessage
                    {
                        UserId = user.Id,
                        TicketId = ticket.Id,
                        StageAtTimeOfMessage = ticket.Stage,
                        Content = message,
                        IsVisibleToClient = true
                    });
                }
                else
                {
                    await _unitOfWork.TicketMessage.AddAsync(new TicketMessage
                    {
                        UserId = user.Id,
                        TicketId = ticket.Id,
                        StageAtTimeOfMessage = ticket.Stage,
                        Content = message
                    });
                }
                ticket.Stage = Stage.NoStage;
            }
            else
            {
                if (ticket.Stage.Equals(Stage.Stage1))
                {
                    await _unitOfWork.TicketHistory.AddAsync(new TicketHistory
                    {
                        UserId = user.Id,
                        TicketId = ticket.Id,
                        StageBeforeChange = ticket.Stage,
                        StageAfterChange = Stage.Stage2
                    });
                    await _unitOfWork.TicketMessage.AddAsync(new TicketMessage
                    {
                        UserId = user.Id,
                        TicketId = ticket.Id,
                        StageAtTimeOfMessage = ticket.Stage,
                        Content = message
                    });

                    if (ticket.Status.Equals(TicketStatus.Returned))
                    {
                        ticket.Status = TicketStatus.Returned; // Explicitly cast the status to TicketStatus 
                    }
                    else
                    {
                        ticket.Status = TicketStatus.InProgress;
                    }
                    ticket.Stage = Stage.Stage2; // Explicitly cast the stage to Stage 
                }
                else
                {
                    if (status.Equals("returned"))
                    {
                        await _unitOfWork.TicketHistory.AddAsync(new TicketHistory
                        {
                            UserId = user.Id,
                            TicketId = ticket.Id,
                            StageBeforeChange = ticket.Stage,
                            StageAfterChange = Stage.Stage1
                        });
                        await _unitOfWork.TicketMessage.AddAsync(new TicketMessage
                        {
                            UserId = user.Id,
                            TicketId = ticket.Id,
                            StageAtTimeOfMessage = ticket.Stage,
                            Content = message
                        });
                        ticket.Status = TicketStatus.Returned;
                        ticket.Stage = Stage.Stage1;
                    }
                    else
                    {
                        await _unitOfWork.TicketHistory.AddAsync(new TicketHistory
                        {
                            UserId = user.Id,
                            TicketId = ticket.Id,
                            StageBeforeChange = ticket.Stage,
                            StageAfterChange = Stage.NoStage
                        });
                        await _unitOfWork.TicketMessage.AddAsync(new TicketMessage
                        {
                            UserId = user.Id,
                            TicketId = ticket.Id,
                            StageAtTimeOfMessage = ticket.Stage,
                            Content = message
                        });
                        ticket.Status = TicketStatus.Completed;
                        ticket.Stage = Stage.NoStage;
                    }
                }
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
