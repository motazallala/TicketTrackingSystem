using TicketTrackingSystem.Core.Model.Enum;
using TicketTrackingSystem.Core.Model.GenericProp;

namespace TicketTrackingSystem.Core.Model;
public class TicketMessage : BaseEntity<Guid>
{
    public string Content { get; set; }
    public Guid TicketId { get; set; }
    public virtual Ticket Ticket { get; set; }

    public Guid UserId { get; set; }
    public virtual ApplicationUser User { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public Stage StageAtTimeOfMessage { get; set; }

    public bool IsVisibleToClient { get; set; } = false;
    public bool IsSeen { get; set; } = false;
}
