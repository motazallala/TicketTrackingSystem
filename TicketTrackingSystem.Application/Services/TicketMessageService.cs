using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TicketTrackingSystem.Application.Dto;
using TicketTrackingSystem.Application.Interface;
using TicketTrackingSystem.Application.Model;
using TicketTrackingSystem.Common.ExtensionMethod;
using TicketTrackingSystem.Common.Model;
using TicketTrackingSystem.Core.Model.Enum;
using TicketTrackingSystem.DAL.Interface;

namespace TicketTrackingSystem.Application.Services;
public class TicketMessageService : ITicketMessageService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public TicketMessageService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<Result<DataTablesResponse<TicketMessageDto>>> GetAllTicketMessagesPaginatedAsync(DataTablesRequest request, Guid ticketId, Guid userId)
    {
        try
        {
            var ticket = await _unitOfWork.Tickets.GetByIdAsync(ticketId);
            var member = await _unitOfWork.ProjectMembers.GetAllAsQueryable().AsNoTracking().Include(p => p.User).FirstOrDefaultAsync(p => p.UserId == userId);
            if (member == null)
            {
                return Result<DataTablesResponse<TicketMessageDto>>.Failure("Member not found");
            }
            if (ticket == null)
            {
                return Result<DataTablesResponse<TicketMessageDto>>.Failure("Ticket not found");
            }
            var query = _unitOfWork.TicketMessage.GetAllAsQueryable().AsNoTracking().Where(p => p.TicketId == ticketId);

            //message for client
            if (member.User.UserType.Equals(UserType.Client))
            {
                query = query.Where(p => (p.StageAtTimeOfMessage.Equals(Stage.Stage1) || p.StageAtTimeOfMessage.Equals(Stage.NoStage)) && p.IsVisibleToClient);
            }
            // message for member on stage 2
            else if (member.User.UserType.Equals(UserType.Member) && member.Stage.Equals(Stage.Stage2))
            {
                query = query.Where(p => p.StageAtTimeOfMessage.Equals(Stage.Stage2) || p.StageAtTimeOfMessage.Equals(Stage.Stage1) && !p.IsVisibleToClient);
            }
            // All Messages for member on stage 1 and user with no stage
            else if (member.User.UserType.Equals(UserType.Member) && member.Stage.Equals(Stage.Stage1) || member.Stage.Equals(Stage.NoStage))
            {
                // this is for Future if there any change on message for member on stage 1
                query = query;
            }
            // otherwise the user is not a member or client
            else
            {
                return Result<DataTablesResponse<TicketMessageDto>>.Failure("User Is Not A Member Or Client");
            }

            // Apply search
            if (!string.IsNullOrEmpty(request.Search?.Value))
            {
                var searchValue = request.Search.Value.ToLower();
                query = query.Where(p => p.Content.ToLower().Contains(searchValue) || p.User.UserName.ToLower().Contains(searchValue));
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
            var departmentDtos = _mapper.Map<IEnumerable<TicketMessageDto>>(paginatedData);
            var response = new DataTablesResponse<TicketMessageDto>
            {
                Draw = request.Draw,
                RecordsTotal = recordsTotal,
                RecordsFiltered = recordsTotal,
                Data = departmentDtos
            };
            return Result<DataTablesResponse<TicketMessageDto>>.Success(response);
        }
        catch (Exception ex)
        {

            return Result<DataTablesResponse<TicketMessageDto>>.Failure($"Something went wrong : {ex.Message}");
        }

    }
}
