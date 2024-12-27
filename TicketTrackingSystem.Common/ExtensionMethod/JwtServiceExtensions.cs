using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TicketTrackingSystem.Common.Model;

namespace TicketTrackingSystem.Common.ExtensionMethod;
public static class JwtServiceExtensions
{
    public static IServiceCollection AddJwtSettings(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));

        services.AddAuthentication(options =>
        {

            options.DefaultAuthenticateScheme = "JwtBearer"; // Default for web views    options.DefaultChallengeScheme = "Cookies";

            options.DefaultScheme = "JwtBearer";

        })
    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, o =>
    {
        o.RequireHttpsMetadata = false;
        o.SaveToken = false;
        o.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidIssuer = configuration["JwtSettings:Issuer"],
            ValidAudience = configuration["JwtSettings:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:SecretKey"])),
            ClockSkew = TimeSpan.Zero
        };

        o.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                if (context.Request.Cookies.ContainsKey("accessToken"))
                {
                    context.Token = context.Request.Cookies["accessToken"];
                }
                return Task.CompletedTask;
            }
        };
    });

        return services;
    }
}