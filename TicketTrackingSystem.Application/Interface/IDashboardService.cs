using TicketTrackingSystem.Application.Model;

namespace TicketTrackingSystem.Application.Interface;

public interface IDashboardService
{
    Task<Result<int>> GetTotalTicketsAsync();
    Task<Result<int>> GetTotalUsersAsync();
    Task<Result<int>> GetTotalMemberTicketAsync(Guid userId);
    Task<Result<int>> GetTotalClientTicketAsync(Guid userId);
}