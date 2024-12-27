using TicketTrackingSystem.Core.Model.GenericProp;

namespace TicketTrackingSystem.Core.Model;
public class Permission : BaseEntity<Guid>
{
    public string Name { get; set; }
    public Guid? ParentId { get; set; }  // Nullable foreign key
    public virtual Permission Parent { get; set; }  // Navigation property for parent Permission
    public virtual ICollection<Permission> Children { get; set; }  // Navigation property for child Permissions
    public virtual ICollection<RolePermission> RolePermissions { get; set; }
}
