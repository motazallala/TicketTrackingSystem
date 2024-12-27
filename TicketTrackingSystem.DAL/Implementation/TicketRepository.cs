using TicketTrackingSystem.Core.Interface;
using TicketTrackingSystem.Core.Model;

namespace TicketTrackingSystem.DAL.Implementation;
public class TicketRepository : Repository<Ticket>, ITicketRepository
{
    public TicketRepository(TicketTrackingSystemDbContext context) : base(context)
    {
    }
}

