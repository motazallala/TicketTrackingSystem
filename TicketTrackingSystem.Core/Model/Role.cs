using Microsoft.AspNetCore.Identity;

namespace TicketTrackingSystem.Core.Model;
public class Role : IdentityRole<Guid>
{
    public bool isDeleted { get; set; } = false;
    public virtual ICollection<RolePermission> RolePermissions { get; set; }
    public virtual ICollection<UserRole> Users { get; set; }
}
