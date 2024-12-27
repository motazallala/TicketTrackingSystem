using TicketTrackingSystem.Core.Interface;
using TicketTrackingSystem.Core.Model;

namespace TicketTrackingSystem.DAL.Implementation;
public class RolePermissionRepository : Repository<RolePermission>, IRolePermissionRepository
{
    public RolePermissionRepository(TicketTrackingSystemDbContext context) : base(context)
    {
    }
}
