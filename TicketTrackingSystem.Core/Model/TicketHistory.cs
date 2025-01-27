using TicketTrackingSystem.Core.Model.Enum;
using TicketTrackingSystem.Core.Model.GenericProp;

namespace TicketTrackingSystem.Core.Model;
public class TicketHistory : BaseEntity<Guid>
{
    public Guid UserId { get; set; }
    public virtual ApplicationUser User { get; set; }
    public Guid? AssignedToId { get; set; }  // User who the ticket was assigned to (for Assignment type)
    public virtual ApplicationUser AssignedTo { get; set; }
    public HistoryType HistoryType { get; set; }
    public DeliveryStatus? DeliveryStatus { get; set; }
    public bool IsOverdue => EstimatedCompletionDate.HasValue &&
                            EstimatedCompletionDate.Value < DateTime.Now &&
                            HistoryType == HistoryType.Assignment;
    public Stage? StageBeforeChange { get; set; }
    public Stage? StageAfterChange { get; set; }
    public DateTime? EstimatedCompletionDate { get; set; }
    public DateTime Date { get; set; } = DateTime.UtcNow;
    public Guid TicketId { get; set; }
    public virtual Ticket Ticket { get; set; }

    public Guid? ParentId { get; set; }
    public virtual TicketHistory Parent { get; set; }
    public virtual ICollection<TicketHistory> Children { get; set; }
    public ActionName? ActionName { get; set; }
}
