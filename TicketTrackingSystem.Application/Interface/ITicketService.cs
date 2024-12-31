using TicketTrackingSystem.Application.Dto;
using TicketTrackingSystem.Application.Model;
using TicketTrackingSystem.Common.Model;

namespace TicketTrackingSystem.Application.Interface;
public interface ITicketService
{
    Task<Result<DataTablesResponse<TicketDto>>> GetAllTicketPaginatedAsync(DataTablesRequest request, Guid projectId);
    Task<Result<DataTablesResponse<TicketDto>>> GetAllTicketForUserPaginatedAsync(DataTablesRequest request, Guid projectId, Guid userId);
    Task<Result<TicketDto>> GetTicketByIdAsync(Guid id);
    Task<Result<TicketDto>> AddTicketAsync(CreateTicketDto ticketDto);
    Task<Result<TicketDto>> UpdateTicketStatusAsync(Guid id, int status);
    Task<Result<TicketDto>> UpdateTicketStageAsync(Guid id, int stage);
    Task<Result<TicketDto>> UpdateTicketMessageAsync(Guid id, string message);
    Task<Result<TicketDto>> UpdateTicketStatusWithAutoStageAsync(Guid id, int status, bool isFinished);
    Task<Result<TicketDto>> UpdateTicketWithAutoStageAsync(Guid id, string status, bool isFinished, string message = null);
    string GetTicketStatusDropdown();
}
