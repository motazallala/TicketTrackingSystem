using TicketTrackingSystem.Core.Model;

namespace TicketTrackingSystem.Core.Interface;
public interface ITicketRepository : IRepository<Ticket>
{
    Task SetLateTicketsAsync(Guid ticketId);
    Task RemoveAssignFromTicketAsync(Guid userId);
}
