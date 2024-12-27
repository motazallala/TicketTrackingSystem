using TicketTrackingSystem.Core.Interface;
using TicketTrackingSystem.Core.Model;

namespace TicketTrackingSystem.DAL.Implementation;
public class PermissionRepository : Repository<Permission>, IPermissionRepository
{
    public PermissionRepository(TicketTrackingSystemDbContext context) : base(context)
    {
    }
}
