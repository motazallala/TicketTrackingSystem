using System.ComponentModel.DataAnnotations;

namespace TicketTrackingSystem.Application.Dto;
public class ProjectDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime CreatedAt { get; set; }
}
public class CreateProjectDto
{
    [Required]
    [MaxLength(100, ErrorMessage = "The Name Is To Long!")]
    public string Name { get; set; }
    [Required]
    [MaxLength(500, ErrorMessage = "The Description Is To Long!")]
    public string Description { get; set; }
}

public class UpdateProjectDto
{
    public Guid Id { get; set; }
    [Required]
    [MaxLength(100, ErrorMessage = "The Name Is To Long!")]
    public string Name { get; set; }
    [Required]
    [MaxLength(500, ErrorMessage = "The Description Is To Long!")]
    public string Description { get; set; }
}

public class DeleteProjectRequest
{
    [Required]
    public Guid ProjectId { get; set; }
}

public class SetUserForProjectRequest
{
    [Required]
    public Guid UserId { get; set; }

    [Required]
    public Guid ProjectId { get; set; }

    [Required]
    public int Stage { get; set; }
}

public class RemoveUserFromProjectRequest
{
    [Required]
    public Guid UserId { get; set; }

    [Required]
    public Guid ProjectId { get; set; }
}