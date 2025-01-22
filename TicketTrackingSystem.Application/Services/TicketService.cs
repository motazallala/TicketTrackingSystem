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
    //assign ticket to user 
    //new function
    public async Task<Result<TicketDto>> AssignTicketToUserAsync(Guid ticketId, Guid userId, DateTime estimationTime)
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

            var isAssigned = await _unitOfWork.Tickets.GetAllAsQueryable().AnyAsync(p => p.AssignedToId == member.UserId && p.DeliveryStatus != DeliveryStatus.Late);
            if (isAssigned)
            {
                return Result<TicketDto>.Failure("User is already assigned to another ticket");
            }
            //get the last history of the ticket
            var lastHistory = await _unitOfWork.TicketHistory.GetAllAsQueryable()
                                                             .Where(p => p.TicketId == ticket.Id && p.HistoryType.Equals(HistoryType.Assignment))
                                                             .OrderByDescending(p => p.Date)
                                                             .FirstOrDefaultAsync();
            if (member.Stage.Equals(Stage.Stage1))
            {
                if (ticket.Status.Equals(TicketStatus.Returned) && !ticket.AssignedToId.HasValue)
                {
                    lastHistory.AssignedToId = user.Id;
                    lastHistory.EstimatedCompletionDate = estimationTime;
                    ticket.Status = TicketStatus.Returned;
                    ticket.AssignedToId = user.Id;
                    ticket.Stage = Stage.Stage1;
                    _unitOfWork.TicketHistory.Update(lastHistory);
                }
                else
                {
                    if (lastHistory is null)
                    {
                        await _unitOfWork.TicketHistory.AddAsync(new TicketHistory
                        {
                            UserId = ticket.CreatorId,
                            TicketId = ticket.Id,
                            StageBeforeChange = ticket.Stage,
                            StageAfterChange = Stage.Stage1,
                            HistoryType = HistoryType.Assignment,
                            AssignedToId = user.Id,
                            EstimatedCompletionDate = estimationTime,


                        });
                    }
                    else
                    {
                        lastHistory.AssignedToId = user.Id;
                        lastHistory.EstimatedCompletionDate = estimationTime;
                        _unitOfWork.TicketHistory.Update(lastHistory);
                    }
                    ticket.Status = TicketStatus.Assigned;
                    ticket.AssignedToId = user.Id;
                    ticket.Stage = Stage.Stage1;

                }
            }
            else if (member.Stage.Equals(Stage.Stage2))
            {

                //make sure the last history is not null
                if (lastHistory == null)
                {
                    return Result<TicketDto>.Failure("The ticket has no history");
                }
                if (lastHistory.AssignedToId.HasValue)
                {
                    return Result<TicketDto>.Failure("there is error in history");
                }
                if (ticket.Status.Equals(TicketStatus.Returned) && !ticket.AssignedToId.HasValue)
                {
                    lastHistory.AssignedToId = user.Id;
                    lastHistory.EstimatedCompletionDate = estimationTime;
                    ticket.Status = TicketStatus.Returned;
                    ticket.AssignedToId = member.UserId;
                    ticket.Stage = Stage.Stage2;
                }
                else
                {
                    lastHistory.AssignedToId = member.UserId;
                    lastHistory.EstimatedCompletionDate = estimationTime;
                    ticket.Status = TicketStatus.Assigned;
                    ticket.AssignedToId = member.UserId;
                    ticket.Stage = Stage.Stage2;
                }
                _unitOfWork.TicketHistory.Update(lastHistory);
            }
            else
            {
                return Result<TicketDto>.Failure("User is not in the stage one or two");
            }

            ticket.UpdatedAt = DateTime.UtcNow;
            _unitOfWork.Tickets.Update(ticket);
            await _unitOfWork.CompleteAsync();
            return Result<TicketDto>.Success(_mapper.Map<TicketDto>(ticket));
        }
        catch (Exception ex)
        {
            return Result<TicketDto>.Failure(ex.Message);
        }
    }
    //remove assing from user
    public async Task<Result<TicketDto>> RemoveTicketFromUserAsync(Guid ticketId, Guid userId)
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
        //get the last history of the ticket
        var lastHistory = await _unitOfWork.TicketHistory.GetAllAsQueryable()
                                                         .Where(p => p.TicketId == ticket.Id && p.HistoryType.Equals(HistoryType.Assignment))
                                                         .OrderByDescending(p => p.Date)
                                                         .FirstOrDefaultAsync();
        if (member.Stage.Equals(Stage.Stage1))
        {
            if (ticket.Status.Equals(TicketStatus.Assigned))
            {
                await _unitOfWork.TicketHistory.AddAsync(new TicketHistory
                {
                    UserId = ticket.CreatorId,
                    TicketId = ticket.Id,
                    StageBeforeChange = ticket.Stage,
                    StageAfterChange = Stage.Stage1,
                    HistoryType = HistoryType.Assignment,
                });
                ticket.Status = TicketStatus.Pending;
                ticket.AssignedToId = null;
                ticket.Stage = Stage.Stage1;
            }
            else
            {
                await _unitOfWork.TicketHistory.AddAsync(new TicketHistory
                {
                    UserId = lastHistory.UserId,
                    TicketId = ticket.Id,
                    StageBeforeChange = ticket.Stage,
                    StageAfterChange = Stage.Stage1,
                    HistoryType = HistoryType.Assignment,
                    ParentId = lastHistory.ParentId
                });
                lastHistory.ParentId = null;
                ticket.Status = TicketStatus.Returned;
                ticket.AssignedToId = null;
                ticket.Stage = Stage.Stage1;

            }
        }
        else if (member.Stage.Equals(Stage.Stage2))
        {

            //make sure the last history is not null
            if (lastHistory == null)
            {
                return Result<TicketDto>.Failure("The ticket has no history");
            }
            if (!lastHistory.AssignedToId.HasValue)
            {
                return Result<TicketDto>.Failure("The ticket has no assigned user");
            }
            if (ticket.Status.Equals(TicketStatus.Assigned))
            {
                //add new history to the ticket
                await _unitOfWork.TicketHistory.AddAsync(new TicketHistory
                {
                    UserId = lastHistory.UserId,
                    TicketId = ticket.Id,
                    StageBeforeChange = ticket.Stage,
                    StageAfterChange = Stage.Stage2,
                    HistoryType = HistoryType.Assignment,
                    ParentId = lastHistory.ParentId
                });
                lastHistory.ParentId = null;
                ticket.Status = TicketStatus.InProgress;
                ticket.AssignedToId = null;
                ticket.Stage = Stage.Stage2;
            }
            else
            {
                //add new history to the ticket
                await _unitOfWork.TicketHistory.AddAsync(new TicketHistory
                {
                    UserId = lastHistory.UserId,
                    TicketId = ticket.Id,
                    StageBeforeChange = ticket.Stage,
                    StageAfterChange = Stage.Stage2,
                    HistoryType = HistoryType.Assignment,
                    ParentId = lastHistory.ParentId
                });
                lastHistory.ParentId = null;
                ticket.Status = TicketStatus.Returned;
                ticket.AssignedToId = null;
                ticket.Stage = Stage.Stage2;

            }
        }
        else
        {
            return Result<TicketDto>.Failure("User is not in the stage one or two");
        }

        ticket.UpdatedAt = DateTime.UtcNow;
        _unitOfWork.TicketHistory.Update(lastHistory);
        _unitOfWork.Tickets.Update(ticket);
        await _unitOfWork.CompleteAsync();
        return Result<TicketDto>.Success(_mapper.Map<TicketDto>(ticket));

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
                        query = query.Where(p => p.Stage.Equals(member.Stage) && p.ProjectId == member.ProjectId && (p.Status.Equals(TicketStatus.Pending) || (p.Status.Equals(TicketStatus.Returned) && !p.AssignedToId.HasValue)));
                    }
                    else if (member.Stage.Equals(Stage.Stage2))
                    {
                        query = query.Where(p => p.Stage.Equals(member.Stage) && p.ProjectId == member.ProjectId && (p.Status.Equals(TicketStatus.InProgress) || (p.Status.Equals(TicketStatus.Returned) && !p.AssignedToId.HasValue)));
                    }
                    else
                    {
                        return Result<DataTablesResponse<TicketDto>>.Failure("The member is not in the stage one or two");
                    }

                }
                else
                {
                    query = query.Where(p => p.Stage.Equals(member.Stage) &&
                                            p.ProjectId == member.ProjectId &&
                                            (p.Status.Equals(TicketStatus.Assigned) || p.Status.Equals(TicketStatus.Returned)) &&
                                            p.AssignedToId == member.UserId);
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
            var lastHistory = await _unitOfWork.TicketHistory.GetAllAsQueryable()
                                 .Where(p => p.TicketId == ticket.Id && p.HistoryType.Equals(HistoryType.Assignment))
                                 .OrderByDescending(p => p.Date)
                                 .FirstOrDefaultAsync();
            if (lastHistory == null)
            {
                return Result<TicketDto>.Failure("The ticket has no history");
            }

            if (lastHistory.EstimatedCompletionDate > DateTime.Now)
            {
                lastHistory.DeliveryStatus = DeliveryStatus.OnTime;
            }
            else
            {
                lastHistory.DeliveryStatus = DeliveryStatus.Late;
            }
            _unitOfWork.TicketHistory.Update(lastHistory);
            //if user is finished the ticket
            if (isFinished)
            {
                //check if the user is accepted or rejected the ticket
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
                //add new history to the ticket that log the change of the stage
                await _unitOfWork.TicketHistory.AddAsync(new TicketHistory
                {
                    UserId = user.Id,
                    TicketId = ticket.Id,
                    StageBeforeChange = ticket.Stage,
                    StageAfterChange = Stage.NoStage,
                    HistoryType = HistoryType.StageChange,
                });
                //check if the user in any stage to know if the message is visible for the user or not
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
            //handle the next stage operation 
            else
            {
                //the member in the stage 1
                if (ticket.Stage.Equals(Stage.Stage1))
                {
                    //add new history to the ticket to log the change of the stage to 2
                    await _unitOfWork.TicketHistory.AddAsync(new TicketHistory
                    {
                        UserId = user.Id,
                        TicketId = ticket.Id,
                        StageBeforeChange = ticket.Stage,
                        StageAfterChange = Stage.Stage2,
                        HistoryType = HistoryType.StageChange,
                    });
                    //message for the member in the stage 2 from stage 1
                    await _unitOfWork.TicketMessage.AddAsync(new TicketMessage
                    {
                        UserId = user.Id,
                        TicketId = ticket.Id,
                        StageAtTimeOfMessage = ticket.Stage,
                        Content = message
                    });
                    //check if the ticket is returned to re assing ticket for him
                    if (ticket.Status.Equals(TicketStatus.Returned))
                    {
                        await _unitOfWork.TicketHistory.AddAsync(new TicketHistory
                        {
                            UserId = user.Id,
                            TicketId = ticket.Id,
                            StageBeforeChange = ticket.Stage,
                            StageAfterChange = Stage.Stage2,
                            HistoryType = HistoryType.Assignment,
                            AssignedToId = lastHistory.UserId,
                            ParentId = lastHistory.Id
                        });
                        ticket.AssignedToId = lastHistory.UserId;
                        ticket.DeliveryStatus = null;
                        ticket.Status = TicketStatus.Returned;
                    }
                    //if the ticket is for the first time log that the ticket is not assing to any member
                    else
                    {
                        await _unitOfWork.TicketHistory.AddAsync(new TicketHistory
                        {
                            UserId = user.Id,
                            TicketId = ticket.Id,
                            StageBeforeChange = ticket.Stage,
                            StageAfterChange = Stage.Stage2,
                            HistoryType = HistoryType.Assignment,
                            AssignedToId = null,
                            ParentId = lastHistory.Id

                        });
                        ticket.Status = TicketStatus.InProgress;
                        ticket.AssignedToId = null;
                        ticket.DeliveryStatus = null;
                    }
                    ticket.Stage = Stage.Stage2;
                }
                //the member in the stage 2
                else
                {
                    //check if the member wont to return to the ticket to stage 1
                    if (status.Equals("returned"))
                    {
                        //log re assign the user how send the ticket
                        await _unitOfWork.TicketHistory.AddAsync(new TicketHistory
                        {
                            UserId = user.Id,
                            TicketId = ticket.Id,
                            StageBeforeChange = ticket.Stage,
                            StageAfterChange = Stage.Stage1,
                            HistoryType = HistoryType.Assignment,
                            AssignedToId = lastHistory.UserId,
                            ParentId = lastHistory.Id
                        });
                        //log the change of the stage
                        await _unitOfWork.TicketHistory.AddAsync(new TicketHistory
                        {
                            UserId = user.Id,
                            TicketId = ticket.Id,
                            StageBeforeChange = ticket.Stage,
                            StageAfterChange = Stage.Stage1,
                            HistoryType = HistoryType.StageChange,
                        });
                        //message for the user for the stage 1
                        await _unitOfWork.TicketMessage.AddAsync(new TicketMessage
                        {
                            UserId = user.Id,
                            TicketId = ticket.Id,
                            StageAtTimeOfMessage = ticket.Stage,
                            Content = message
                        });
                        //re assign the user how returned the ticket
                        ticket.Status = TicketStatus.Returned;
                        ticket.AssignedToId = lastHistory.UserId;
                        ticket.DeliveryStatus = null;
                        ticket.Stage = Stage.Stage1;
                    }
                    //if the member solve the ticket and finish the ticket
                    else
                    {
                        // log the change of the stage  and add a meesage
                        await _unitOfWork.TicketHistory.AddAsync(new TicketHistory
                        {
                            UserId = user.Id,
                            TicketId = ticket.Id,
                            StageBeforeChange = ticket.Stage,
                            StageAfterChange = Stage.NoStage,
                            HistoryType = HistoryType.StageChange,
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

    //re assign ticket to user
    public async Task<Result<TicketDto>> ReAssignTicketAsync(Guid ticketId, Guid userId)
    {
        try
        {
            var lastHistory = await _unitOfWork.TicketHistory.GetAllAsQueryable()
                 .Where(p => p.TicketId == ticketId && p.HistoryType.Equals(HistoryType.Assignment))
                 .OrderByDescending(p => p.Date)
                 .FirstOrDefaultAsync();
            if (lastHistory == null)
            {
                return Result<TicketDto>.Failure("The ticket has no history");
            }
            if (lastHistory.EstimatedCompletionDate > DateTime.Now)
            {
                lastHistory.DeliveryStatus = DeliveryStatus.OnTime;
            }
            else
            {
                lastHistory.DeliveryStatus = DeliveryStatus.Late;
            }
            if (!lastHistory.EstimatedCompletionDate.HasValue)
            {
                lastHistory.DeliveryStatus = null;
            }
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
            if (lastHistory.AssignedToId == userId)
            {
                return Result<TicketDto>.Failure("This Ticket already assign to this user.");
            }
            var isAssigned = await _unitOfWork.Tickets.GetAllAsQueryable().AnyAsync(p => p.AssignedToId == user.Id && p.DeliveryStatus != DeliveryStatus.Late);
            if (isAssigned)
            {
                return Result<TicketDto>.Failure("User is already assigned to another ticket");
            }

            await _unitOfWork.TicketHistory.AddAsync(new TicketHistory
            {
                UserId = lastHistory.UserId,
                TicketId = ticketId,
                StageBeforeChange = lastHistory.StageBeforeChange,
                StageAfterChange = lastHistory.StageAfterChange,
                HistoryType = HistoryType.Assignment,
                AssignedToId = user.Id,
                ParentId = lastHistory.ParentId
            });
            ticket.AssignedToId = user.Id;
            ticket.DeliveryStatus = null;
            lastHistory.ParentId = null;
            _unitOfWork.Tickets.Update(ticket);
            _unitOfWork.TicketHistory.Update(lastHistory);
            await _unitOfWork.CompleteAsync();
            return Result<TicketDto>.Success(_mapper.Map<TicketDto>(ticket));
        }
        catch (Exception ex)
        {

            return Result<TicketDto>.Failure($"Something went wrong {ex.Message}.");
        }
    }
    public async Task<Result<bool>> CheckEstimatedCompletionDateAsync(Guid ticketId)
    {
        var lastHistory = await _unitOfWork.TicketHistory.GetAllAsQueryable()
                     .Where(p => p.TicketId == ticketId && p.HistoryType.Equals(HistoryType.Assignment))
                     .OrderByDescending(p => p.Date)
                     .FirstOrDefaultAsync();
        if (lastHistory == null)
        {
            return Result<bool>.Failure("The ticket has no history");
        }
        if (lastHistory.EstimatedCompletionDate.HasValue)
        {
            return Result<bool>.Success(true);
        }
        else
        {
            return Result<bool>.Success(false);
        }

    }

    public async Task<Result<bool>> SetEstimatedCompletionDateForReassignTicketAsync(Guid ticketId, Guid userId, DateTime estimationTime)
    {
        var lastHistory = await _unitOfWork.TicketHistory.GetAllAsQueryable()
                     .Where(p => p.TicketId == ticketId && p.HistoryType.Equals(HistoryType.Assignment))
                     .OrderByDescending(p => p.Date)
                     .FirstOrDefaultAsync();
        if (lastHistory == null)
        {
            return Result<bool>.Failure("The ticket has no history");
        }
        if (lastHistory.EstimatedCompletionDate.HasValue)
        {
            return Result<bool>.Failure("The ticket has already estimated completion date");
        }
        if (lastHistory.AssignedToId != userId)
        {
            return Result<bool>.Failure("This Ticket is not for the user.");
        }
        lastHistory.EstimatedCompletionDate = estimationTime;
        _unitOfWork.TicketHistory.Update(lastHistory);
        await _unitOfWork.CompleteAsync();
        return Result<bool>.Success(true);

    }


    public async Task<string> GetAllFreeMembersDropdownAsync(Guid projectId, Guid userId)
    {
        // Check if the user is a member of the specified project
        var member = await _unitOfWork.ProjectMembers
            .GetAllAsQueryable()
            .FirstOrDefaultAsync(p => p.ProjectId == projectId && p.UserId == userId);

        // If the member doesn't exist, return an empty dropdown
        if (member == null)
        {
            return string.Empty;
        }

        // Fetch all tickets in the same project and stage as the member
        var relevantTickets = await _unitOfWork.Tickets
            .GetAllAsQueryable()
            .Where(p => p.ProjectId == projectId && p.Stage == member.Stage)
            .ToListAsync();

        // Identify member IDs assigned to tickets (excluding ones marked 'Late')
        var assignedMemberIds = relevantTickets
            .Where(t => t.AssignedToId.HasValue && t.DeliveryStatus != DeliveryStatus.Late)
            .Select(t => t.AssignedToId.Value)
            .Distinct()
            .ToHashSet();

        // Fetch project members in the same stage and exclude those already assigned
        var freeMembers = await _unitOfWork.ProjectMembers
            .GetAllAsQueryable()
            .Include(p => p.User)
            .Where(p => p.ProjectId == projectId && p.Stage == member.Stage && !assignedMemberIds.Contains(p.UserId) && p.UserId != member.UserId)
            .ToListAsync();

        // Build the dropdown options
        var dropdownOptions = freeMembers
            .Select(p => new SelectListItem
            {
                Value = p.UserId.ToString(),
                Text = p.User.UserName
            })
            .ToList();

        // Generate the HTML string
        var htmlString = new System.Text.StringBuilder();
        foreach (var item in dropdownOptions)
        {
            htmlString.Append($"<option value='{item.Value}'>{item.Text}</option>");
        }

        return htmlString.ToString();
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
