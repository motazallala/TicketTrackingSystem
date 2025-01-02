using System.Security.Claims;

namespace TicketTrackingSystem.Application.Interface;
public interface ISignInService
{
    /// <summary>
    /// Performs the sign-out operation for the current user.
    /// </summary>
    /// <returns>
    /// A <see cref="Task"/> representing the asynchronous sign-out operation.
    /// </returns>
    Task SignOutAsync();
    /// <summary>
    /// Checks if the specified user is currently signed in.
    /// </summary>
    /// <param name="user">The <see cref="ClaimsPrincipal"/> representing the user to check.</param>
    /// <returns>
    /// <see langword="true"/> if the user is signed in; otherwise, <see langword="false"/>.
    /// </returns>
    bool IsSignedIn(ClaimsPrincipal user);
}
