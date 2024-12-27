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
    public TicketTrackingSystemDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Configure RolePermission relationship
        builder.Entity<Permission>(rp =>
        {
            rp.HasOne(rp => rp.Parent).WithOne().HasForeignKey<Permission>(x => x.ParentId);
        });
        builder.Entity<RolePermission>()
            .HasKey(rp => new { rp.RoleId, rp.PermissionId });

        builder.Entity<RolePermission>()
            .HasOne(rp => rp.Role)
            .WithMany(r => r.RolePermissions)
            .HasForeignKey(rp => rp.RoleId);

        builder.Entity<RolePermission>()
            .HasOne(rp => rp.Permission)
            .WithMany(r => r.RolePermissions)
            .HasForeignKey(rp => rp.PermissionId);
        builder.Entity<ApplicationUser>(rp =>
        {
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
        // Configure the self-referencing relationship for the Permission entity
        builder.Entity<Permission>()
            .HasOne(p => p.Parent)                // One Permission has one Parent
            .WithMany(p => p.Children)            // One Permission can have many Children
            .HasForeignKey(p => p.ParentId)       // Foreign key is ParentId
            .OnDelete(DeleteBehavior.Restrict);   // Prevent cascading deletes
        builder.SeedUsers();
        builder.SeedRoles();
        builder.SeedUserRoles();
        builder.SeedPermissions();
        builder.SeedRolePermissions();
    }
}
