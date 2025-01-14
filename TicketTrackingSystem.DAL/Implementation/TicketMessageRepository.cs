using TicketTrackingSystem.Core.Interface;
using TicketTrackingSystem.Core.Model;

namespace TicketTrackingSystem.DAL.Implementation;
public class TicketMessageRepository : Repository<TicketMessage>, ITicketMessageRepository
{
    private readonly TicketTrackingSystemDbContext _context;
    public TicketMessageRepository(TicketTrackingSystemDbContext context) : base(context)
    {
        _context = context;
    }
    public void DeleteAllMessageForUser(Guid userId)
    {
        var history = _context.TicketMessage.Where(x => x.UserId == userId);
        _context.TicketMessage.RemoveRange(history);
    }
}
