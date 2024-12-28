namespace TicketTrackingSystem.Application.Dto;
public class RoleWithPermissionDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public List<PermissionDto> Permissions { get; set; }
}
public class PermissionDto
{
    public Guid Id { get; set; }
    public Guid? ParentId { get; set; }
    public string Name { get; set; }
    public PermissionDto Parent { get; set; }
    public List<PermissionDto> Children { get; set; }  // Navigation property for child Permissions

}
public class CreateRolePermissionDto
{
    public Guid RoleId { get; set; }
    public Guid PermissionId { get; set; }
}

public class CreateRolePermissionsDto
{
    public Guid RoleId { get; set; }
    public List<Guid> PermissionIds { get; set; }
}