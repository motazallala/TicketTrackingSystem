using TicketTrackingSystem.Core.Model.GenericProp;

namespace TicketTrackingSystem.Core.Model;
public class Department : BaseEntity<Guid>
{
    public string Name { get; set; }
    public string Description { get; set; }
    public virtual ICollection<ApplicationUser> Employees { get; set; }

}
