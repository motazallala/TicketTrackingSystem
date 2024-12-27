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
    public string Name { get; set; }
    public string Description { get; set; }
}

public class UpdateProjectDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
}