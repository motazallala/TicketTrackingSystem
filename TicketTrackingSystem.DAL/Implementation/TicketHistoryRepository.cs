using TicketTrackingSystem.Core.Interface;
using TicketTrackingSystem.Core.Model;

namespace TicketTrackingSystem.DAL.Implementation;
public class TicketHistoryRepository : Repository<TicketHistory>, ITicketHistoryRepository
{
    private readonly TicketTrackingSystemDbContext _context;
    public TicketHistoryRepository(TicketTrackingSystemDbContext context) : base(context)
    {
        _context = context;
    }

    public void DeleteAllHistoryForUser(Guid userId)
    {
        var history = _context.TicketHistory.Where(x => x.UserId == userId || x.AssignedToId == userId);
        _context.TicketHistory.RemoveRange(history);
    }
}
