using TicketTrackingSystem.Core.Interface;
using TicketTrackingSystem.DAL.Interface;

namespace TicketTrackingSystem.DAL.Implementation;
public class UnitOfWork : IUnitOfWork
{
    public IUserRepository Users { get; private set; }

    public IDepartmentRepository Departments { get; private set; }

    public IProjectRepository Projects { get; private set; }

    public ITicketRepository Tickets { get; private set; }

    public IProjectMemberRepository ProjectMembers { get; private set; }

    public IRolePermissionRepository RolesPermissions { get; private set; }

    public IPermissionRepository Permissions { get; private set; }

    public IRoleRepository Roles { get; private set; }

    public ITicketHistoryRepository TicketHistory { get; private set; }

    public ITicketMessageRepository TicketMessage { get; private set; }

    private readonly TicketTrackingSystemDbContext _context;
    public UnitOfWork(TicketTrackingSystemDbContext context)
    {
        _context = context;
        Users = new UserRepository(_context);
        Departments = new DepartmentRepository(_context);
        Projects = new ProjectRepository(_context);
        Tickets = new TicketRepository(_context);
        ProjectMembers = new ProjectMemberRepository(_context);
        RolesPermissions = new RolePermissionRepository(_context);
        Permissions = new PermissionRepository(_context);
        Roles = new RoleRepository(_context);
        TicketHistory = new TicketHistoryRepository(_context);
        TicketMessage = new TicketMessageRepository(_context);
    }

    public int Complete()
    {
        return _context.SaveChanges();
    }

    public async Task<int> CompleteAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
