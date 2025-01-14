using System.ComponentModel;

namespace TicketTrackingSystem.Core.Model.Enum;

public enum TicketStatus
{
    Pending,
    [Description("In Progress")]
    InProgress,
    Completed,
    Rejected,
    Returned,
    Assigned,
}