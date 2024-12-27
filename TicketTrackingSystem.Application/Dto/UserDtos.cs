namespace TicketTrackingSystem.Application.Dto;
public class UserDto
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public Guid? DepartmentId { get; set; }
    public string? DepartmentName { get; set; }
    public string UserTypeNumber { get; set; }
    public string UserType { get; set; }
    public string FullName { get; set; }
    public IEnumerable<RoleDto> Roles { get; set; }
}
public class CreateUserDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string? PhoneNumber { get; set; }
    public string Password { get; set; }
    public string UserType { get; set; }
    public string? DepartmentId { get; set; }
}
public class UpdateUserDto
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string? PhoneNumber { get; set; }
    public string Password { get; set; }
    public string UserType { get; set; }
    public string? DepartmentId { get; set; }
}