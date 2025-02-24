using System.Text.Json.Serialization;
using TicketTrackingSystem.Core.Model.Enum;

namespace TicketTrackingSystem.Application.Dto;
public class TicketMessageDto
{
    public Guid Id { get; set; }
    public Guid TicketId { get; set; }
    public string Content { get; set; }
    public Guid UserId { get; set; }
    public DateTime CreatedAt { get; set; }
    public string StageAtTimeOfMessage { get; set; }
    public string UserName { get; set; }
    public bool IsSeen { get; set; }
    [JsonIgnore]
    public bool IsVisibleToClient { get; set; }

}
public class TicketMessageAllDto
{
    public Guid Id { get; set; }
    public Guid TicketId { get; set; }
    public string Content { get; set; }
    public Guid UserId { get; set; }
    public DateTime CreatedAt { get; set; }
    public Stage StageAtTimeOfMessage { get; set; }
    public string UserName { get; set; }
    public bool IsSeen { get; set; }
    public bool IsVisibleToClient { get; set; }

}