using TicketTrackingSystem.Core.Model.Enum;
using TicketTrackingSystem.Core.Model.GenericProp;

namespace TicketTrackingSystem.Core.Model;
public class TicketHistory : BaseEntity<Guid>
{

    public Guid? AssignedToId { get; set; }  // User who the ticket was assigned to (for Assignment type)
    public virtual ApplicationUser AssignedTo { get; set; }
    public DeliveryStatus? DeliveryStatus { get; set; }
    public bool IsOverdue => EstimatedCompletionDate.HasValue &&
                            EstimatedCompletionDate.Value < DateTime.Now;
    public Stage? StageAfterChange { get; set; }
    public DateTime? EstimatedCompletionDate { get; set; }
    public DateTime Date { get; set; } = DateTime.UtcNow;
    public Guid TicketId { get; set; }
    public virtual Ticket Ticket { get; set; }
}
