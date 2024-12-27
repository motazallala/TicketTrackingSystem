using System.ComponentModel;

namespace TicketTrackingSystem.Core.Model.Enum;

public enum TicketStatus
{
    New,
    [Description("In Progress")]
    InProgress,
    Closed,
}