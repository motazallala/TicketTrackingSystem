using TicketTrackingSystem.Core.Model.GenericProp;

namespace TicketTrackingSystem.Core.Model;
public class Project : BaseEntity<Guid>
{
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public virtual ICollection<ProjectMember> Members { get; set; }
    public virtual ICollection<Ticket> Tickets { get; set; }
}
