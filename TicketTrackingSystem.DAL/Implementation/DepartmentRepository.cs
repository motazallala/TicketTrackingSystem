using TicketTrackingSystem.Core.Interface;
using TicketTrackingSystem.Core.Model;

namespace TicketTrackingSystem.DAL.Implementation;
public class DepartmentRepository : Repository<Department>, IDepartmentRepository
{
    public DepartmentRepository(TicketTrackingSystemDbContext context) : base(context)
    {
    }
}
