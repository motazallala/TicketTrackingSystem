using TicketTrackingSystem.Core.Interface;
using TicketTrackingSystem.Core.Model;

namespace TicketTrackingSystem.DAL.Implementation;
public class ProjectMemberRepository : Repository<ProjectMember>, IProjectMemberRepository
{
    public ProjectMemberRepository(TicketTrackingSystemDbContext context) : base(context)
    {
    }
}
