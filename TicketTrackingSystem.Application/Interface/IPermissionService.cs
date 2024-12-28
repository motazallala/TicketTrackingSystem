using TicketTrackingSystem.Application.Dto;
using TicketTrackingSystem.Application.Model;
using TicketTrackingSystem.Common.Model;

namespace TicketTrackingSystem.Application.Interface;
public interface IPermissionService
{
    Task<bool> HasPermissionAsync(Guid userId, string permissionName);
    Task<Dictionary<string, bool>> HasPermissionAsync(Guid userId, params string[] permissionNames);
    Task<Result<DataTablesResponse<RoleWithPermissionDto>>> GetAllRolesWithPermissionPaginatedAsync(DataTablesRequest request);
    Task<Result<CreateRolePermissionDto>> AddRoleToPermissionAsync(CreateRolePermissionDto createPermission);
    Task<Result<string>> GetAllPermissionsAsHtmlAsync();
    Task<Result<CreateRolePermissionsDto>> AddRoleToPermissionsAsync(CreateRolePermissionsDto createPermissions);
    Task<Result<CreateRolePermissionDto>> RemoveRoleFromPermissionAsync(CreateRolePermissionDto removePermission);
}
