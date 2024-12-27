using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using TicketTrackingSystem.Application.Interface;
using TicketTrackingSystem.Application.Mappers;
using TicketTrackingSystem.Application.Services;
using TicketTrackingSystem.DAL.Implementation;
using TicketTrackingSystem.DAL.Interface;

namespace TicketTrackingSystem.Application.ExtensionMethod;
public static class ServiceExtensions
{
    public static IServiceCollection AddCustomServices(this IServiceCollection Services)
    {
        Services.AddScoped<IAuthService, AuthService>();
        Services.AddScoped<ISignInService, SignInService>();
        Services.AddScoped<IUserService, UserService>();
        Services.AddScoped<IPermissionService, PermissionService>();
        Services.AddScoped<IDepartmentService, DepartmentService>();
        Services.AddScoped<IRoleService, RoleService>();
        Services.AddScoped<IProjectService, ProjectService>();
        Services.AddTransient<IUnitOfWork, UnitOfWork>();
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new MappingProfile());
        });
        var mapper = config.CreateMapper();
        Services.AddSingleton(mapper);
        return Services;
    }
}
