using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TicketTrackingSystem.Application.Dto;
using TicketTrackingSystem.Application.Interface;

namespace TicketTrackingSystem.MVC.Controllers;
public class AccountController : Controller
{
    private readonly IAuthService _authService;
    private readonly ISignInService _signInService;
    public AccountController(IAuthService authService, ISignInService signInService)
    {
        _authService = authService;
        _signInService = signInService;
    }
    public IActionResult Login(string returnUrl = null)
    {
        ViewData["ReturnUrl"] = returnUrl;
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> LogIn(LoginDto model, string returnUrl = null)
    {
        if (!ModelState.IsValid)
        {
            return View(ModelState);
        }

        var result = await _authService.LoginWithJwtAsync(model);

        if (!result.IsSuccess)
        {
            ModelState.AddModelError("Error", result.ErrorMessage);
            return View();
        }

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, result.Value.UserId.ToString()),
            new Claim(ClaimTypes.Name, result.Value.Username),
            new Claim(ClaimTypes.Email, result.Value.Email)
        };

        foreach (var role in result.Value.Roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        var claimsIdentity = new ClaimsIdentity(claims, "Identity.Application");

        await HttpContext.SignInAsync("Identity.Application", new ClaimsPrincipal(claimsIdentity));

        if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
        {
            return Redirect(returnUrl);
        }

        return RedirectToAction("Index", "Home");
    }

    [HttpGet]
    //AccessDenied view for unauthorized users
    public IActionResult AccessDenied()
    {
        return View();
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync();
        await _signInService.SignOutAsync();
        return RedirectToAction("Login", "Account");
    }
    #region private method
    private void SetRefreshTokenInCookie(string refreshToken, DateTime expires)
    {
        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Expires = expires.ToLocalTime(),
            Secure = true,
            IsEssential = true,
            SameSite = SameSiteMode.None
        };

        Response.Cookies.Append("refreshToken", refreshToken, cookieOptions);
    }

    private void SetJwtTokenInCookie(string jwtToken, DateTime expires)
    {
        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Expires = expires.ToLocalTime(),
            Secure = true,
            IsEssential = true,
            SameSite = SameSiteMode.None
        };

        Response.Cookies.Append("accessToken", jwtToken, cookieOptions);
    }
    //set jwt in session
    private void SetJwtInSession(string jwt)
    {
        HttpContext.Session.SetString("jwtToken", jwt);
    }


    #endregion
}
