using TicketTrackingSystem.Core.Model.Enum;
using TicketTrackingSystem.Core.Model.GenericProp;

namespace TicketTrackingSystem.Core.Model;
public class TicketHistory : BaseEntity<Guid>
{
    public Guid UserId { get; set; }
    public virtual ApplicationUser User { get; set; }
    public Stage StageBeforeChange { get; set; }
    public Stage StageAfterChange { get; set; }
    public DateTime Date { get; set; } = DateTime.UtcNow;
    public Guid TicketId { get; set; }
    public virtual Ticket Ticket { get; set; }
}
