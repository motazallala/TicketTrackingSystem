using TicketTrackingSystem.Core.Model.Enum;
using TicketTrackingSystem.Core.Model.GenericProp;

namespace TicketTrackingSystem.Core.Model;
public class Ticket : BaseEntity<Guid>
{
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; }
    public TicketStatus Status { get; set; } = TicketStatus.New;
    public Stage Stage { get; set; } = Stage.Stage1;
    public Guid ProjectId { get; set; }
    public virtual Project Project { get; set; }
    public Guid CreatorId { get; set; }
    public virtual ApplicationUser Creator { get; set; }
    public string? Message { get; set; }


}
