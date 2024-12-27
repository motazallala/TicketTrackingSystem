using Microsoft.AspNetCore.Identity;
using TicketTrackingSystem.Application.Interface;
using TicketTrackingSystem.Core.Model;
using TicketTrackingSystem.DAL.Interface;

namespace TicketTrackingSystem.Application.Services;
public class PermissionService : IPermissionService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<Role> _roleManager;
    private readonly IUnitOfWork _unitOfWork;

    public PermissionService(
        UserManager<ApplicationUser> userManager,
        RoleManager<Role> roleManager,
        IUnitOfWork unitOfWork)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _unitOfWork = unitOfWork;
    }

    /// <summary>
    /// Checks if a user has a specific permission on a page or action.
    /// </summary>
    /// <param name="userId">User's ID</param>
    /// <param name="permissionName">The name of the permission (e.g., "View", "Edit")</param>
    /// <returns>True if the user has permission, otherwise false</returns>
    public async Task<bool> HasPermissionAsync(Guid userId, string permissionName)
    {
        // Get the user and their roles
        var user = await _userManager.FindByIdAsync(userId.ToString());
        if (user == null) return false;

        // Get the user's roles
        var roles = await _userManager.GetRolesAsync(user);
        if (roles == null || !roles.Any()) return false;

        // Check all roles for permissions
        foreach (var roleName in roles)
        {
            var role = await _roleManager.FindByNameAsync(roleName);
            if (role != null)
            {
                // Get all RolePermissions for this role
                var rolePermissions = await _unitOfWork.RolesPermissions.GetAllAsync(rp => rp.RoleId == role.Id, ["Permission"]);

                // Check if the required permission or its parent exists
                var hasPermission = rolePermissions.Any(rp =>
                    rp.Permission.Name.Equals(permissionName, StringComparison.OrdinalIgnoreCase) ||
                    (rp.Permission.Children != null && rp.Permission.Children.Any(c => c.Name.Equals(permissionName, StringComparison.OrdinalIgnoreCase))));

                if (hasPermission)
                {
                    return true;
                }
            }
        }

        // If no roles have the permission, return false
        return false;
    }


    public async Task<Dictionary<string, bool>> HasPermissionAsync(Guid userId, params string[] permissionNames)
    {
        // Initialize the dictionary to store results
        var permissionResults = permissionNames.ToDictionary(permissionName => permissionName, _ => false);

        // Get the user and their roles
        var user = await _userManager.FindByIdAsync(userId.ToString());
        if (user == null) return permissionResults;

        // Get the user's roles
        var roles = await _userManager.GetRolesAsync(user);
        if (roles == null || !roles.Any()) return permissionResults;

        // Check all roles for permissions
        foreach (var roleName in roles)
        {
            var role = await _roleManager.FindByNameAsync(roleName);
            if (role != null)
            {
                // Get all RolePermissions for this role
                var rolePermissions = await _unitOfWork.RolesPermissions.GetAllAsync(rp => rp.RoleId == role.Id, ["Permission"]);

                // Iterate over the requested permissions and check each one
                foreach (var permissionName in permissionNames)
                {
                    // Check if the required permission or its parent exists
                    var hasPermission = rolePermissions.Any(rp =>
                        rp.Permission.Name.Equals(permissionName, StringComparison.OrdinalIgnoreCase) ||
                        (rp.Permission.Children != null && rp.Permission.Children.Any(c => c.Name.Equals(permissionName, StringComparison.OrdinalIgnoreCase))));

                    // Update the result for this permission
                    if (hasPermission)
                    {
                        permissionResults[permissionName] = true;
                    }
                }
            }
        }

        return permissionResults;
    }
}
