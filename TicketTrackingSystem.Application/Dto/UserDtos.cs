using System.ComponentModel.DataAnnotations;

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
    [Required]
    [MaxLength(100, ErrorMessage = "The FirstName Is To Long!")]
    public string FirstName { get; set; }
    [Required]
    [MaxLength(100, ErrorMessage = "The LastName Is To Long!")]
    public string LastName { get; set; }
    [Required]
    [MaxLength(100, ErrorMessage = "The UserName Is To Long!")]
    public string UserName { get; set; }
    [Required]
    [MaxLength(256, ErrorMessage = "The Email Is To Long!")]
    public string Email { get; set; }

    [MaxLength(13, ErrorMessage = "The PhoneNumber Is To Long!")]
    public string? PhoneNumber { get; set; }
    [Required]
    public string Password { get; set; }
    public string UserType { get; set; }
    public string? DepartmentId { get; set; }
}
public class UpdateUserDto
{
    public Guid Id { get; set; }
    [Required]
    [MaxLength(100, ErrorMessage = "The FirstName Is To Long!")]
    public string FirstName { get; set; }
    [Required]
    [MaxLength(100, ErrorMessage = "The LastName Is To Long!")]
    public string LastName { get; set; }
    [Required]
    [MaxLength(256, ErrorMessage = "The Email Is To Long!")]
    public string UserName { get; set; }
    [Required]
    [MaxLength(256, ErrorMessage = "The Email Is To Long!")]
    public string Email { get; set; }
    [MaxLength(13, ErrorMessage = "The PhoneNumber Is To Long!")]
    public string? PhoneNumber { get; set; }
    public string Password { get; set; }
    public string UserType { get; set; }
    public string? DepartmentId { get; set; }
}