namespace TicketTrackingSystem.Application.Dto;
public class RoleDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}
public class UpdateRoleDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}