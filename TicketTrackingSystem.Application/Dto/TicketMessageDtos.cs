namespace TicketTrackingSystem.Application.Dto;
public class TicketMessageDto
{
    public Guid Id { get; set; }
    public Guid TicketId { get; set; }
    public string Content { get; set; }
    public Guid UserId { get; set; }
    public DateTime CreatedAt { get; set; }
    public string StageAtTimeOfMessage { get; set; }
}