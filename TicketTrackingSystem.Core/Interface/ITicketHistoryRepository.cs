using TicketTrackingSystem.Core.Model;

namespace TicketTrackingSystem.Core.Interface;
public interface ITicketHistoryRepository : IRepository<TicketHistory>
{
    void DeleteAllHistoryForUser(Guid userId);
}
