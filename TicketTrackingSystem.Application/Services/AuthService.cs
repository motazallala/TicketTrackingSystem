using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using TicketTrackingSystem.Application.Dto;
using TicketTrackingSystem.Application.Interface;
using TicketTrackingSystem.Application.Model;
using TicketTrackingSystem.Common.Model;
using TicketTrackingSystem.Core.Model;
using TicketTrackingSystem.DAL.Interface;

namespace TicketTrackingSystem.Application.Services;
public class AuthService : IAuthService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly JwtSettings _jwtSettings;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<Role> _roleManager;
    public AuthService(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager, IOptions<JwtSettings> jwtSettings, RoleManager<Role> roleManager)
    {
        _unitOfWork = unitOfWork;
        _userManager = userManager;
        _jwtSettings = jwtSettings.Value;
        _roleManager = roleManager;
    }


    public async Task<Result<AuthDto>> RegisterAsync(SingupDto model)
    {
        if (await _userManager.FindByEmailAsync(model.Email) is not null)
            return Result<AuthDto>.Failure("Email is already registered!");

        if (await _userManager.FindByNameAsync(model.Username) is not null)
            return Result<AuthDto>.Failure("Username is already registered!");
        //Save the user in the database
        var user = new ApplicationUser
        {
            UserName = model.Username,
            Email = model.Email,
            FirstName = model.FirstName,
            LastName = model.LastName
        };
        var result = await _userManager.CreateAsync(user, model.Password);

        if (!result.Succeeded)
        {
            var errors = string.Empty;

            foreach (var error in result.Errors)
            {
                errors += $"* {error.Description}{Environment.NewLine}";
            }

            return Result<AuthDto>.Failure(errors);
        }

        await _userManager.AddToRoleAsync(user, "User");
        // Generate Refresh Token
        //var refreshToken = GenerateRefreshToken();
        //user.UserTokens?.Add(new UserToken
        //{
        //    Token = refreshToken.Token,
        //    ExpiresOn = refreshToken.ExpiresOn,
        //    CreatedOn = refreshToken.CreatedOn
        //});
        //Save the token in the data base
        //await _userManager.UpdateAsync(user);

        // Generate JWT Token
        var jwtSecurityToken = await CreateJwtToken(user);


        var x = new AuthDto
        {
            Email = user.Email,
            JwtExpiresOn = jwtSecurityToken.ValidTo,
            IsAuthenticated = true,
            Roles = new List<string> { "User" },
            JwtToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
            Username = user.UserName,
            UserId = user.Id,
        };

        return Result<AuthDto>.Success(x);
    }

    public async Task<Result<string>> AddUserRoleAsync(UserRoleDto model)
    {
        var user = await _userManager.FindByIdAsync(model.UserId);

        if (user is null || !await _roleManager.RoleExistsAsync(model.Role))
            return Result<string>.Failure("Invalid user ID or Role");

        if (await _userManager.IsInRoleAsync(user, model.Role))
            return Result<string>.Failure("User already assigned to this role");

        var result = await _userManager.AddToRoleAsync(user, model.Role);

        return result.Succeeded ? Result<string>.Success() : Result<string>.Failure("Something went wrong");
    }


    public async Task<Result<AuthDto>> LoginWithJwtAsync(LoginDto model)
    {
        var AuthDto = new AuthDto();

        var user = await _userManager.FindByEmailAsync(model.Email);

        if (user is null || !await _userManager.CheckPasswordAsync(user, model.Password))
        {
            return Result<AuthDto>.Failure("Email or Password is incorrect!");
        }

        // Generate JWT Token
        var jwtSecurityToken = await CreateJwtToken(user);
        var rolesList = await _userManager.GetRolesAsync(user);

        AuthDto.IsAuthenticated = true;
        AuthDto.JwtToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        AuthDto.Email = user.Email;
        AuthDto.UserId = user.Id;
        AuthDto.Username = user.UserName;
        AuthDto.JwtExpiresOn = jwtSecurityToken.ValidTo;
        AuthDto.Roles = rolesList.ToList();

        return Result<AuthDto>.Success(AuthDto);
    }

    #region Private Method
    private RefreshTokenDto GenerateRefreshToken()
    {
        var randomNumber = new byte[32];

        using var generator = new RNGCryptoServiceProvider();

        generator.GetBytes(randomNumber);

        return new RefreshTokenDto
        {
            Token = Convert.ToBase64String(randomNumber),
            ExpiresOn = DateTime.UtcNow.AddDays(10),
            CreatedOn = DateTime.UtcNow
        };
    }

    private async Task<JwtSecurityToken> CreateJwtToken(ApplicationUser user)

    {
        var userClaims = await _userManager.GetClaimsAsync(user);
        var roles = await _userManager.GetRolesAsync(user);
        var roleClaims = new List<Claim>();

        foreach (var role in roles)
            roleClaims.Add(new Claim("roles", role));

        var claims = new[]
        {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid", user.Id.ToString()),
                new Claim("userType", ((int)user.UserType).ToString())
        }
        .Union(userClaims)
        .Union(roleClaims);

        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));
        var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

        var jwtSecurityToken = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_jwtSettings.TokenExpirationMinutes),
            signingCredentials: signingCredentials);

        return jwtSecurityToken;
    }

    #endregion


}
