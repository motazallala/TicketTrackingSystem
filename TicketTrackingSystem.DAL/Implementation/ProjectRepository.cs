using TicketTrackingSystem.Core.Interface;
using TicketTrackingSystem.Core.Model;

namespace TicketTrackingSystem.DAL.Implementation;
public class ProjectRepository : Repository<Project>, IProjectRepository
{
    public ProjectRepository(TicketTrackingSystemDbContext context) : base(context)
    {
    }
}
