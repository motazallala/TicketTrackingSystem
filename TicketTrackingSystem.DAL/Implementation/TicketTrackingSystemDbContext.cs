using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TicketTrackingSystem.Core.Model;
using TicketTrackingSystem.Core.Seeds;

namespace TicketTrackingSystem.DAL.Implementation;
public class TicketTrackingSystemDbContext : IdentityDbContext<ApplicationUser, Role, Guid, IdentityUserClaim<Guid>, UserRole, IdentityUserLogin<Guid>, IdentityRoleClaim<Guid>, IdentityUserToken<Guid>>
{

    public DbSet<Permission> Permission { get; set; }
    public DbSet<RolePermission> RolePermission { get; set; }
    public DbSet<Department> Department { get; set; }
    public DbSet<Project> Project { get; set; }
    public DbSet<Ticket> Ticket { get; set; }
    public DbSet<ProjectMember> ProjectMember { get; set; }
    public DbSet<Role> Role { get; set; }
    public DbSet<ApplicationUser> User { get; set; }
    public DbSet<UserRole> UserRole { get; set; }
    public DbSet<TicketHistory> TicketHistory { get; set; }
    public DbSet<TicketMessage> TicketMessage { get; set; }
    public TicketTrackingSystemDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<RolePermission>(rp =>
        {
            rp.HasKey(pk => new { pk.RoleId, pk.PermissionId });
            rp.HasOne(x => x.Role).WithMany(x => x.RolePermissions).HasForeignKey(rp => rp.RoleId);
            rp.HasOne(x => x.Permission).WithMany(x => x.RolePermissions).HasForeignKey(rp => rp.PermissionId);
        });

        builder.Entity<Department>(rp =>
        {
            //make the name and the description has a maximum length of 100
            rp.Property(x => x.Name).HasMaxLength(100);
            rp.Property(x => x.Description).HasMaxLength(500);
        });
        builder.Entity<Project>(rp =>
        {
            //make the name and the description has a maximum length of 100
            rp.Property(x => x.Name).HasMaxLength(100);
            rp.Property(x => x.Description).HasMaxLength(500);
        });
        builder.Entity<ApplicationUser>(rp =>
        {
            rp.Property(x => x.FirstName).HasMaxLength(100);
            rp.Property(x => x.LastName).HasMaxLength(100);
            rp.HasOne(x => x.Department).WithMany(x => x.Employees).HasForeignKey(rp => rp.DepartmentId);

        });
        builder.Entity<ProjectMember>(pm =>
        {
            pm.HasKey(pk => new { pk.ProjectId, pk.UserId });
            pm.HasOne(x => x.Project).WithMany(x => x.Members).HasForeignKey(pm => pm.ProjectId);
            pm.HasOne(x => x.User).WithMany(x => x.ProjectMembers).HasForeignKey(pm => pm.UserId);
        });
        builder.Entity<Ticket>(t =>
        {
            t.Property(x => x.Title).HasMaxLength(100);
            t.Property(x => x.Description).HasMaxLength(500);
            t.HasOne(x => x.Project).WithMany(x => x.Tickets).HasForeignKey(t => t.ProjectId);
            t.HasOne(p => p.Creator).WithMany(p => p.Tickets).HasForeignKey(fk => fk.CreatorId);
        });
        builder.Entity<UserRole>(entity =>
        {
            // Configure composite primary key
            entity.HasKey(ur => new { ur.UserId, ur.RoleId });

            // Configure relationships
            entity.HasOne(ur => ur.User)
                .WithMany(u => u.Roles)
                .HasForeignKey(ur => ur.UserId)
                .IsRequired();

            entity.HasOne(ur => ur.Role)
                .WithMany(r => r.Users)
                .HasForeignKey(ur => ur.RoleId)
                .IsRequired();

        });

        builder.Entity<TicketHistory>(th =>
        {
            th.HasOne(x => x.Ticket).WithMany(x => x.TicketHistories).HasForeignKey(th => th.TicketId).OnDelete(DeleteBehavior.Cascade);
            th.HasOne(x => x.User).WithMany(x => x.TicketHistories).HasForeignKey(th => th.UserId).OnDelete(DeleteBehavior.Restrict);
        });
        builder.Entity<TicketMessage>(th =>
        {
            th.Property(x => x.Content).HasMaxLength(500);
            th.HasOne(x => x.Ticket).WithMany(x => x.TicketMessages).HasForeignKey(th => th.TicketId).OnDelete(DeleteBehavior.Cascade);
            th.HasOne(x => x.User).WithMany(x => x.TicketMessages).HasForeignKey(th => th.UserId).OnDelete(DeleteBehavior.Restrict);
        });

        builder.Entity<Permission>(p =>
        {
            p.Property(x => x.Name).HasMaxLength(100);
            p.HasOne(x => x.Parent).WithMany(x => x.Children).HasForeignKey(p => p.ParentId).OnDelete(DeleteBehavior.Restrict);
        });
        builder.SeedUsers();
        builder.SeedRoles();
        builder.SeedUserRoles();
        builder.SeedPermissions();
        builder.SeedRolePermissions();
    }
}
