using TicketTrackingSystem.Core.Model;

namespace TicketTrackingSystem.Core.Interface;
public interface ITicketMessageRepository : IRepository<TicketMessage>
{
    void DeleteAllMessageForUser(Guid userId);
}
