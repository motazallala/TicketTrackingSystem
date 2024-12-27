using Microsoft.AspNetCore.Identity;

namespace TicketTrackingSystem.Core.Model;
public class UserRole : IdentityUserRole<Guid>
{
    public virtual ApplicationUser User { get; set; }
    public virtual Role Role { get; set; }
}
