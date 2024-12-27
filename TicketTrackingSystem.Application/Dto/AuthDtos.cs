using System.ComponentModel.DataAnnotations;

namespace TicketTrackingSystem.Application.Dto;
public class AuthDto
{
    public bool IsAuthenticated { get; set; }
    public Guid UserId { get; set; }
    public string? Username { get; set; }
    public string? Email { get; set; }
    public List<string>? Roles { get; set; }
    public string? JwtToken { get; set; }
    public DateTime JwtExpiresOn { get; set; }

    //[JsonIgnore]
    //public string? RefreshToken { get; set; }

    //public DateTime RefreshTokenExpiration { get; set; }
}
public class RefreshTokenDto
{
    public string Token { get; set; }
    public DateTime ExpiresOn { get; set; }
    public DateTime CreatedOn { get; set; }
}
public class SingupDto
{
    [StringLength(100)]
    public string FirstName { get; set; }

    [StringLength(100)]
    public string LastName { get; set; }

    [StringLength(50)]
    public string Username { get; set; }

    [StringLength(128)]
    [EmailAddress]
    public string Email { get; set; }

    [StringLength(256)]
    public string Password { get; set; }
}
public class UserRoleDto
{
    public string UserId { get; set; }
    public string Role { get; set; }
}
public class LoginDto
{
    [EmailAddress]
    public string Email { get; set; }
    public string Password { get; set; }
}
