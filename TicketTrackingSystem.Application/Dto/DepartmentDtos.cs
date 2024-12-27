namespace TicketTrackingSystem.Application.Dto;
public class CreateDepartmentDto
{
    public string Name { get; set; }
    public string Description { get; set; }
}

public class DepartmentDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

}
public class UpdateDepartmentDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
}