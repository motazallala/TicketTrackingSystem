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
    Task<Result<ProjectDto>> GetProjectByIdAsync(Guid id);
    Task<Result<DataTablesResponse<ProjectDto>>> GetAllUserProjectsAsync(DataTablesRequest request, Guid clientId);
    Task<Result<string>> SetUserForProjectAsync(Guid userId, Guid projectId, int? stage);
    Task<Result<string>> RemoveUserFromProjectAsync(Guid userId, Guid projectId);
    string GetStageDropdown();
}
