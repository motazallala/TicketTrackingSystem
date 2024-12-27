using System.Security.Claims;

namespace TicketTrackingSystem.Application.Interface;
public interface ISignInService
{
    Task SignOutAsync();
    bool IsSignedIn(ClaimsPrincipal user);
}
