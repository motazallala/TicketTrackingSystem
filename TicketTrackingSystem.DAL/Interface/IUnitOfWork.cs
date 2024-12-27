using TicketTrackingSystem.Core.Interface;

namespace TicketTrackingSystem.DAL.Interface;
public interface IUnitOfWork : IDisposable
{
    IDepartmentRepository Departments { get; }
    IRolePermissionRepository RolesPermissions { get; }
    IProjectMemberRepository ProjectMembers { get; }
    IProjectRepository Projects { get; }
    IPermissionRepository Permissions { get; }
    IRoleRepository Roles { get; }
    ITicketRepository Tickets { get; }
    IUserRepository Users { get; }

    int Complete();
    Task<int> CompleteAsync();
}
