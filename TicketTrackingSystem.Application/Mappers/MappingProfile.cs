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
    }
}
