using TicketTrackingSystem.Core.Interface;
using TicketTrackingSystem.Core.Model;

namespace TicketTrackingSystem.DAL.Implementation;
public class UserRepository : Repository<ApplicationUser>, IUserRepository
{
    public UserRepository(TicketTrackingSystemDbContext context) : base(context)
    {
    }
}

