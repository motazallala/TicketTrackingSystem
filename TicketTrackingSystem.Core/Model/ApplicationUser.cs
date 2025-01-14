using Microsoft.AspNetCore.Identity;
using TicketTrackingSystem.Core.Model.Enum;

namespace TicketTrackingSystem.Core.Model;
public class ApplicationUser : IdentityUser<Guid>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string FullName => $"{FirstName} {LastName}";
    public UserType UserType { get; set; }
    public Guid? DepartmentId { get; set; }
    public virtual Department Department { get; set; }
    public virtual ICollection<Ticket> Tickets { get; set; }
    public virtual ICollection<ProjectMember> ProjectMembers { get; set; }
    public virtual ICollection<UserRole> Roles { get; set; }
    public virtual ICollection<TicketHistory> TicketHistories { get; set; }
    public virtual ICollection<TicketMessage> TicketMessages { get; set; }

}
