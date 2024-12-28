using AutoMapper;
using TicketTrackingSystem.Application.Dto;
using TicketTrackingSystem.Core.Model;

namespace TicketTrackingSystem.Application.Mappers;
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<ApplicationUser, UserDto>()
            .ForMember(dest => dest.Roles, opt => opt.MapFrom(src => src.Roles.Select(x => x.Role)))
            .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.Department.Name))
            .ForMember(dest => dest.UserTypeNumber, opt => opt.MapFrom(src => (int)src.UserType))
            .ForMember(dest => dest.UserType, opt => opt.MapFrom(src => src.UserType.ToString()));


        CreateMap<Role, RoleDto>();

        CreateMap<Department, DepartmentDto>().ReverseMap();
        CreateMap<Department, CreateDepartmentDto>().ReverseMap();
        CreateMap<Department, UpdateDepartmentDto>().ReverseMap();
        CreateMap<Project, ProjectDto>().ReverseMap();
        CreateMap<RolePermission, RoleWithPermissionDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Role.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Role.Name))
            .ForMember(dest => dest.Permissions, opt => opt.MapFrom(src => src.Role.RolePermissions.Select(rp => rp.Permission)))
            .ReverseMap();

        CreateMap<Permission, PermissionDto>()
            .ForMember(dest => dest.Children, opt => opt.MapFrom(src => src.Children))
            .ForMember(dest => dest.Parent, opt => opt.Condition(src => src.Parent != null))  // Avoid recursion when Parent is null
            .ReverseMap();
    }
}
