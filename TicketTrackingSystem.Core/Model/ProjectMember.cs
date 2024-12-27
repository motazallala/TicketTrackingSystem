using TicketTrackingSystem.Core.Model.Enum;

namespace TicketTrackingSystem.Core.Model;
public class ProjectMember
{
    public Guid ProjectId { get; set; }
    public Project Project { get; set; }
    public Guid UserId { get; set; }
    public ApplicationUser User { get; set; }
    public DateTime? JoinDate { get; set; } = DateTime.Now;
    public Stage Stage { get; set; } = Stage.Stage1;
    public UserType UserType { get; set; } = UserType.Client;
}
