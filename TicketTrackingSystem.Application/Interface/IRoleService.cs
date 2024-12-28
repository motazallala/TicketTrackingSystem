using TicketTrackingSystem.Application.Dto;
using TicketTrackingSystem.Application.Model;
using TicketTrackingSystem.Common.Model;

namespace TicketTrackingSystem.Application.Interface;
public interface IRoleService
{
    Task<Result<DataTablesResponse<RoleDto>>> GetAllRolesPaginatedAsync(DataTablesRequest request);
    Task<Result<string>> CreateRoleAsync(string roleName);
    Task<Result<string>> DeleteRoleAsync(string roleName);
    Task<Result<string>> SoftDeleteRoleAsync(string roleName);
    Task<Result<string>> UpdateRoleAsync(UpdateRoleDto updateRoleDto);
    Task<Result<RoleDto>> GetRoleById(Guid roleId);
    Task<Result<string>> GetAllRolesAsHtmlAsync();
}
