namespace TicketTrackingSystem.Application.Dto;
public class TicketDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime CreatedAt { get; set; }
    public string Status { get; set; }

}
public class CreateTicketDto
{
    public Guid CreatorId { get; set; }
    public Guid ProjectId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
}