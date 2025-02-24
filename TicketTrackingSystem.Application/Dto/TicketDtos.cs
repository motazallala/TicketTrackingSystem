using System.ComponentModel.DataAnnotations;
using TicketTrackingSystem.Core.Model.Enum;

namespace TicketTrackingSystem.Application.Dto;
public class TicketDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime CreatedAt { get; set; }
    public string CreatorName { get; set; }
    public string Status { get; set; }

}
public class AllTicketDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime CreatedAt { get; set; }
    public Guid ProjectId { get; set; }
    public Guid UserId { get; set; }
    public Guid? AssignedToId { get; set; }
    public TicketStatus Status { get; set; }
    public Stage Stage { get; set; }
    public string CreatorName { get; set; }
}
public class CreateTicketDto
{
    public Guid CreatorId { get; set; }
    public Guid ProjectId { get; set; }
    [Required]
    [MaxLength(100, ErrorMessage = "The Title Is To Long!")]
    public string Title { get; set; }
    [Required]
    [MaxLength(500, ErrorMessage = "The Description Is To Long!")]
    public string Description { get; set; }
}