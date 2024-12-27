using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using TicketTrackingSystem.Application.Interface;
using TicketTrackingSystem.Core.Model;

namespace TicketTrackingSystem.Application.Services;
public class SignInService : ISignInService
{
    private readonly SignInManager<ApplicationUser> _signInManager;
    public SignInService(SignInManager<ApplicationUser> signInManager)
    {
        _signInManager = signInManager;
    }
    public async Task SignOutAsync()
    {
        await _signInManager.SignOutAsync();
    }

    public bool IsSignedIn(ClaimsPrincipal user)
    {
        return _signInManager.IsSignedIn(user);
    }
}
