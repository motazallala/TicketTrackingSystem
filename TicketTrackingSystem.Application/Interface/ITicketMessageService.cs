using TicketTrackingSystem.Application.Dto;
using TicketTrackingSystem.Application.Model;
using TicketTrackingSystem.Common.Model;

namespace TicketTrackingSystem.Application.Interface;
public interface ITicketMessageService
{
    Task<Result<DataTablesResponse<TicketMessageDto>>> GetAllTicketMessagesPaginatedAsync(DataTablesRequest request, Guid ticketId, Guid userId);
}
