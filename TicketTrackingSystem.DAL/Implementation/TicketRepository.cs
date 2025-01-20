using Microsoft.EntityFrameworkCore;
using TicketTrackingSystem.Core.Interface;
using TicketTrackingSystem.Core.Model;
using TicketTrackingSystem.Core.Model.Enum;

namespace TicketTrackingSystem.DAL.Implementation;
public class TicketRepository : Repository<Ticket>, ITicketRepository
{
    private readonly TicketTrackingSystemDbContext _context;
    public TicketRepository(TicketTrackingSystemDbContext context) : base(context)
    {
        _context = context;
    }
    public async Task SetLateTicketsAsync(Guid ticketId)
    {
        var ticket = await _context.Ticket.Include(c => c.TicketHistories).SingleOrDefaultAsync(v => v.Id == ticketId);
        ticket.DeliveryStatus = DeliveryStatus.Late;
        var lastHistory = ticket.TicketHistories.OrderByDescending(p => p.Date).FirstOrDefault();
        lastHistory.DeliveryStatus = DeliveryStatus.Late;
        _context.TicketHistory.Update(lastHistory);
        _context.Ticket.Update(ticket);
        await _context.SaveChangesAsync();
    }
}

