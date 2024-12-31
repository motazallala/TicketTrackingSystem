using TicketTrackingSystem.Core.Model;

namespace TicketTrackingSystem.Core.Interface;
public interface IUserRepository : IRepository<ApplicationUser>
{
    bool IsInRole(ApplicationUser user, string roleName);
}
