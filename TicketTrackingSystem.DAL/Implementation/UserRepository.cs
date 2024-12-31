using TicketTrackingSystem.Core.Interface;
using TicketTrackingSystem.Core.Model;

namespace TicketTrackingSystem.DAL.Implementation;
public class UserRepository : Repository<ApplicationUser>, IUserRepository
{
    private readonly TicketTrackingSystemDbContext _context;
    public UserRepository(TicketTrackingSystemDbContext context) : base(context)
    {
        _context = context;
    }
    // check if the user is in role
    public bool IsInRole(ApplicationUser user, string roleName)
    {
        return _context.UserRoles.Any(ur => ur.UserId == user.Id && ur.Role.Name == roleName);
    }
}

