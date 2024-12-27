using TicketTrackingSystem.Application.Dto;
using TicketTrackingSystem.Application.Model;

namespace TicketTrackingSystem.Application.Interface;
public interface IAuthService
{
    /// <summary>
    /// This Method Make the registration  
    /// </summary>
    /// <param name="model">The user info</param>
    /// <returns>Auth Dto that has the user info and the jwt token</returns>
    Task<Result<AuthDto>> RegisterAsync(SingupDto model);
    /// <summary>
    /// Assigns a role to a user.
    /// </summary>
    /// <param name="model">An object containing the user and role information.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a Result object with a string indicating the outcome of the operation.</returns>
    Task<Result<string>> AddUserRoleAsync(UserRoleDto model);
    Task<Result<AuthDto>> LoginWithJwtAsync(LoginDto model);
}
