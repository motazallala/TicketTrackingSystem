using TicketTrackingSystem.Application.Dto;
using TicketTrackingSystem.Application.Model;
using TicketTrackingSystem.Common.Model;

namespace TicketTrackingSystem.Application.Interface;
public interface IRoleService
{
    /// <summary>
    /// Retrieves a paginated list of roles based on the provided DataTables request.
    /// </summary>
    /// <param name="request">The DataTables request containing pagination, sorting, and search parameters.</param>
    /// <returns>
    /// A Result object containing the following:
    /// - Success: A DataTablesResponse object containing the paginated list of RoleDto objects.
    /// - Failure: An error message indicating the reason for the failure.
    /// </returns>
    Task<Result<DataTablesResponse<RoleDto>>> GetAllRolesPaginatedAsync(DataTablesRequest request);
    /// <summary>
    /// Creates a new role with the provided role name.
    /// </summary>
    /// <param name="roleName">The name of the role to be created.</param>
    /// <returns>
    /// A Result object containing the following:
    /// - Success: A string message indicating the successful creation of the role.
    /// - Failure: An error message indicating the reason for the failure.
    /// </returns>
    Task<Result<string>> CreateRoleAsync(string roleName);
    /// <summary>
    /// Deletes a role with the provided role name.
    /// </summary>
    /// <param name="roleName">The name of the role to be deleted.</param>
    /// <returns>
    /// A Result object containing the following:
    /// - Success: A string message indicating the successful deletion of the role.
    /// - Failure: An error message indicating the reason for the failure.
    /// </returns>
    Task<Result<string>> DeleteRoleAsync(string roleName);
    /// <summary>
    /// Deletes a role with the provided role name permanently from the system.
    /// </summary>
    /// <param name="roleName">The name of the role to be permanently deleted.</param>
    /// <returns>
    /// A Result object containing the following:
    /// - Success: A string message indicating the successful permanent deletion of the role.
    /// - Failure: An error message indicating the reason for the failure.
    /// </returns>
    Task<Result<string>> SoftDeleteRoleAsync(string roleName);
    /// <summary>
    /// Updates an existing role with the provided role details.
    /// </summary>
    /// <param name="updateRoleDto">
    /// An UpdateRoleDto object containing the details of the role to be updated.
    /// The Dto should include the role's unique identifier (RoleId) and any other relevant information.
    /// </param>
    /// <returns>
    /// A Result object containing the following:
    /// - Success: A string message indicating the successful update of the role.
    /// - Failure: An error message indicating the reason for the failure.
    /// </returns>
    Task<Result<string>> UpdateRoleAsync(UpdateRoleDto updateRoleDto);
    /// <summary>
    /// Retrieves a role by its unique identifier (RoleId).
    /// </summary>
    /// <param name="roleId">The unique identifier of the role to be retrieved.</param>
    /// <returns>
    /// A Result object containing the following:
    /// - Success: A RoleDto object representing the retrieved role.
    /// - Failure: An error message indicating the reason for the failure.
    /// </returns>
    Task<Result<RoleDto>> GetRoleById(Guid roleId);
    /// <summary>
    /// Retrieves all roles as an HTML formatted string.
    /// </summary>
    /// <returns>
    /// A Result object containing the following:
    /// - Success: A string representing the roles in HTML format.
    /// - Failure: An error message indicating the reason for the failure.
    /// </returns>
    Task<Result<string>> GetAllRolesAsHtmlAsync();
}
