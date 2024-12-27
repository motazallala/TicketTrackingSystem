using TicketTrackingSystem.Core.Interface;
using TicketTrackingSystem.Core.Model;

namespace TicketTrackingSystem.DAL.Implementation;
public class RoleRepository : Repository<Role>, IRoleRepository
{
    public RoleRepository(TicketTrackingSystemDbContext context) : base(context)
    {
    }
}
