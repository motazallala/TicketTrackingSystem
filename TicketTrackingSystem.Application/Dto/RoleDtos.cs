using System.ComponentModel.DataAnnotations;

namespace TicketTrackingSystem.Application.Dto;
public class RoleDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}
public class UpdateRoleDto
{
    [Required]
    public Guid Id { get; set; }
    [Required]
    public string Name { get; set; }
}
public class CreateRoleDto
{
    [Required]
    public string Name { get; set; }
}