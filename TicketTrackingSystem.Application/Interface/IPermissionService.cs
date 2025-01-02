using TicketTrackingSystem.Application.Dto;
using TicketTrackingSystem.Application.Model;
using TicketTrackingSystem.Common.Model;

namespace TicketTrackingSystem.Application.Interface;
public interface IPermissionService
{
    /// <summary>
    /// Checks if a user has a specific permission.
    /// </summary>
    /// <param name="userId">The unique identifier of the user.</param>
    /// <param name="permissionName">The name of the permission to check.</param>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// The task result is a boolean value indicating whether the user has the specified permission.
    /// </returns>
    Task<bool> HasPermissionAsync(Guid userId, string permissionName);
    /// <summary>
    /// Determines whether a user has a specific permission.
    /// </summary>
    /// <param name="userId">The unique identifier of the user.</param>
    /// <param name="permissionName">The name of the permission to check.</param>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// The task result contains a boolean value indicating whether the user has the specified permission.
    /// </returns>
    Task<Dictionary<string, bool>> HasPermissionAsync(Guid userId, params string[] permissionNames);
    /// <summary>
    /// Retrieves all roles with their associated permissions in a paginated format.
    /// </summary>
    /// <param name="request">The request object containing pagination and filtering information.</param>
    /// <returns>
    /// A task that represents the asynchronous operation. 
    /// The task result contains a <see cref="Result{T}"/> object with a <see cref="DataTablesResponse{RoleWithPermissionDto}"/> 
    /// that includes the paginated list of roles with permissions.
    /// </returns>
    Task<Result<DataTablesResponse<RoleWithPermissionDto>>> GetAllRolesWithPermissionPaginatedAsync(DataTablesRequest request);
    /// <summary>
    /// Adds a new role to a specific permission.
    /// </summary>
    /// <param name="createPermission">
    /// An instance of <see cref="CreateRolePermissionDto"/> containing the role and permission details.
    /// The role and permission must exist in the system.
    /// </param>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// The task result contains a <see cref="Result{T}"/> object.
    /// If the operation is successful, the result will contain the added <see cref="CreateRolePermissionDto"/>.
    /// If the operation fails, the result will contain an error message.
    /// </returns>
    Task<Result<CreateRolePermissionDto>> AddRoleToPermissionAsync(CreateRolePermissionDto createPermission);
    /// <summary>
    /// Retrieves all permissions in HTML format.
    /// </summary>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// The task result contains a <see cref="Result{T}"/> object.
    /// If the operation is successful, the result will contain a string representing all permissions in HTML format.
    /// If the operation fails, the result will contain an error message.
    /// </returns>
    Task<Result<string>> GetAllPermissionsAsHtmlAsync();
    /// <summary>
    /// Adds a new role to multiple permissions.
    /// </summary>
    /// <param name="createPermissions">
    /// An instance of <see cref="CreateRolePermissionsDto"/> containing the role and a collection of permission details.
    /// The role and permissions must exist in the system.
    /// </param>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// The task result contains a <see cref="Result{T}"/> object.
    /// If the operation is successful, the result will contain the added <see cref="CreateRolePermissionsDto"/>.
    /// If the operation fails, the result will contain an error message.
    /// </returns>
    Task<Result<CreateRolePermissionsDto>> AddRoleToPermissionsAsync(CreateRolePermissionsDto createPermissions);
    /// <summary>
    /// Removes a role from a specific permission.
    /// </summary>
    /// <param name="removePermission">
    /// An instance of <see cref="CreateRolePermissionDto"/> containing the role and permission details.
    /// The role and permission must exist in the system.
    /// </param>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// The task result contains a <see cref="Result{T}"/> object.
    /// If the operation is successful, the result will contain the removed <see cref="CreateRolePermissionDto"/>.
    /// If the operation fails, the result will contain an error message.
    /// </returns>
    Task<Result<CreateRolePermissionDto>> RemoveRoleFromPermissionAsync(CreateRolePermissionDto removePermission);
}
