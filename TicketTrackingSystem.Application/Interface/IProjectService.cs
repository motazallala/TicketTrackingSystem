using TicketTrackingSystem.Application.Dto;
using TicketTrackingSystem.Application.Model;
using TicketTrackingSystem.Common.Model;

namespace TicketTrackingSystem.Application.Interface;
public interface IProjectService
{
    Task<Result<ProjectDto>> CreateProjectAsync(CreateProjectDto newProject);
    Task<Result<ProjectDto>> UpdateProjectAsync(UpdateProjectDto projectDto);
    Task<Result<string>> DeleteProjectAsync(Guid id);
    Task<Result<DataTablesResponse<ProjectDto>>> GetAllProjectPaginatedAsync(DataTablesRequest request);
}
