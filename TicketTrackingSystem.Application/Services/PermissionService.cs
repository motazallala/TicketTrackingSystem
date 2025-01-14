using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TicketTrackingSystem.Application.Dto;
using TicketTrackingSystem.Application.Interface;
using TicketTrackingSystem.Application.Model;
using TicketTrackingSystem.Common.Model;
using TicketTrackingSystem.Core.Model;
using TicketTrackingSystem.DAL.Interface;

namespace TicketTrackingSystem.Application.Services;
public class PermissionService : IPermissionService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<Role> _roleManager;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public PermissionService(
        UserManager<ApplicationUser> userManager,
        RoleManager<Role> roleManager,
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
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
                //here i can add redis to cach the roles permissions
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


    // get All Roles with there permissions pagenation
    public async Task<Result<DataTablesResponse<RoleWithPermissionDto>>> GetAllRolesWithPermissionPaginatedAsync(DataTablesRequest request)
    {
        try
        {
            var query = _unitOfWork.RolesPermissions.GetAllAsQueryable().AsNoTracking();

            if (!string.IsNullOrEmpty(request.Search?.Value))
            {
                var searchValue = request.Search.Value.ToLower();
                query = query.Where(p => p.Role.Name.ToLower().Contains(searchValue));
            }

            // Get distinct role count before pagination
            var recordsTotal = await query
                .Select(x => x.RoleId)
                .Distinct()
                .CountAsync();


            // Apply pagination and grouping
            var paginatedData = await query
                .GroupBy(x => new { x.RoleId, x.Role.Name }) // Group by Role ID and Name
                .Select(g => new RoleWithPermissionDto
                {
                    Id = g.Key.RoleId,
                    Name = g.Key.Name,
                    Permissions = g.SelectMany(x => x.Role.RolePermissions)
                                   .Select(rp => new PermissionDto
                                   {
                                       Id = rp.PermissionId,
                                       Name = rp.Permission.Name,
                                       ParentId = rp.Permission.ParentId,
                                       Parent = null,
                                       Children = null
                                   })
                                   .Distinct() // Ensure permissions are not duplicated
                                   .OrderBy(p => p.Name)
                                   .ToList()
                })
                .Skip(request.Start)
                .Take(request.Length)
                .ToListAsync();

            // Prepare response
            var response = new DataTablesResponse<RoleWithPermissionDto>
            {
                Draw = request.Draw,
                RecordsTotal = recordsTotal, // Total count of distinct roles
                RecordsFiltered = recordsTotal, // This can be adjusted based on filtering
                Data = paginatedData
            };

            return Result<DataTablesResponse<RoleWithPermissionDto>>.Success(response);
        }
        catch (Exception ex)
        {
            return Result<DataTablesResponse<RoleWithPermissionDto>>.Failure(ex.Message);
        }
    }

    public async Task<Result<CreateRolePermissionDto>> AddRoleToPermissionAsync(CreateRolePermissionDto createPermission)
    {
        try
        {
            var role = await _roleManager.FindByIdAsync(createPermission.RoleId.ToString());
            if (role == null)
            {
                return Result<CreateRolePermissionDto>.Failure($"Role with ID {createPermission.RoleId} not found.");
            }
            var permission = await _unitOfWork.Permissions.GetByIdAsync(createPermission.PermissionId);
            if (permission == null)
            {
                return Result<CreateRolePermissionDto>.Failure($"Permission with ID {createPermission.PermissionId} not found.");
            }
            var rolePermissions = await _unitOfWork.RolesPermissions.GetByIdAsync(createPermission.RoleId, createPermission.PermissionId);
            if (rolePermissions != null)
            {
                return Result<CreateRolePermissionDto>.Failure($"Role with ID {createPermission.RoleId} already has permission with ID {createPermission.PermissionId}.");
            }
            var rolePermission = new RolePermission
            {
                RoleId = role.Id,
                PermissionId = permission.Id
            };
            await _unitOfWork.RolesPermissions.AddAsync(rolePermission);
            await _unitOfWork.CompleteAsync();
            return Result<CreateRolePermissionDto>.Success(createPermission);
        }
        catch (Exception ex)
        {
            return Result<CreateRolePermissionDto>.Failure(ex.Message);
        }
    }

    //add list of permissions to role with CreateRolePermissionsDto
    public async Task<Result<CreateRolePermissionsDto>> AddRoleToPermissionsAsync(CreateRolePermissionsDto createPermissions)
    {
        try
        {
            var role = await _roleManager.FindByIdAsync(createPermissions.RoleId.ToString());
            if (role == null)
            {
                return Result<CreateRolePermissionsDto>.Failure($"Role with ID {createPermissions.RoleId} not found.");
            }
            var permissions = await _unitOfWork.Permissions.GetAllAsync(p => createPermissions.PermissionIds.Contains(p.Id));
            if (permissions == null || !permissions.Any())
            {
                return Result<CreateRolePermissionsDto>.Failure("No permissions found.");
            }
            var rolePermissions = await _unitOfWork.RolesPermissions.GetAllAsync(rp => rp.RoleId == role.Id);
            var existingPermissionIds = rolePermissions.Select(rp => rp.PermissionId).ToList();
            var newPermissions = permissions.Where(p => !existingPermissionIds.Contains(p.Id)).ToList();
            if (!newPermissions.Any())
            {
                return Result<CreateRolePermissionsDto>.Failure($"Role with ID {createPermissions.RoleId} already has all the specified permissions.");
            }
            var rolePermissionsToAdd = newPermissions.Select(p => new RolePermission
            {
                RoleId = role.Id,
                PermissionId = p.Id
            }).ToList();
            await _unitOfWork.RolesPermissions.AddRangeAsync(rolePermissionsToAdd);
            await _unitOfWork.CompleteAsync();
            return Result<CreateRolePermissionsDto>.Success(createPermissions);
        }
        catch (Exception ex)
        {
            return Result<CreateRolePermissionsDto>.Failure(ex.Message);
        }
    }
    //remove permission from role
    public async Task<Result<CreateRolePermissionDto>> RemoveRoleFromPermissionAsync(CreateRolePermissionDto removePermission)
    {
        try
        {
            var role = await _roleManager.FindByIdAsync(removePermission.RoleId.ToString());
            if (role == null)
            {
                return Result<CreateRolePermissionDto>.Failure($"Role with ID {removePermission.RoleId} not found.");
            }
            var permission = await _unitOfWork.Permissions.GetByIdAsync(removePermission.PermissionId);
            if (permission == null)
            {
                return Result<CreateRolePermissionDto>.Failure($"Permission with ID {removePermission.PermissionId} not found.");
            }
            var rolePermissions = await _unitOfWork.RolesPermissions.GetByIdAsync(removePermission.RoleId, removePermission.PermissionId);
            if (rolePermissions == null)
            {
                return Result<CreateRolePermissionDto>.Failure($"Role with ID {removePermission.RoleId} does not have permission with ID {removePermission.PermissionId}.");
            }
            if (rolePermissions.RoleId == Guid.Parse("5e4d3c2b-a123-4f57-88ef-1ab23cdb3e57"))
            {
                return Result<CreateRolePermissionDto>.Failure($"Permission with ID {removePermission.PermissionId} is required for the Admin role.");
            }
            _unitOfWork.RolesPermissions.Remove(rolePermissions);
            await _unitOfWork.CompleteAsync();
            return Result<CreateRolePermissionDto>.Success(removePermission);
        }
        catch (Exception ex)
        {
            return Result<CreateRolePermissionDto>.Failure(ex.Message);
        }
    }

    public async Task<Result<string>> GetAllPermissionsAsHtmlAsync()
    {

        try
        {
            var permissions = await _unitOfWork.Permissions.GetAllAsync();

            // Build HTML options for the dropdown
            var permissionsDropdown = string.Join(Environment.NewLine,
                permissions.OrderBy(x => x.Name).Select(d => $"<option value='{d.Id}'>{d.Name}</option>"));

            return Result<string>.Success(permissionsDropdown);

        }
        catch (Exception ex)
        {

            return Result<string>.Failure(ex.Message);
        }
    }


}
