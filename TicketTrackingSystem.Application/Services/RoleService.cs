using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TicketTrackingSystem.Application.Dto;
using TicketTrackingSystem.Application.Interface;
using TicketTrackingSystem.Application.Model;
using TicketTrackingSystem.Common.ExtensionMethod;
using TicketTrackingSystem.Common.Model;
using TicketTrackingSystem.Core.Model;


namespace TicketTrackingSystem.Application.Services;
public class RoleService : IRoleService
{
    private readonly RoleManager<Role> _roleManager;
    private readonly IMapper _mapper;
    public RoleService(RoleManager<Role> roleManager, IMapper mapper)
    {
        _roleManager = roleManager;
        _mapper = mapper;
    }

    public async Task<Result<DataTablesResponse<RoleDto>>> GetAllRolesPaginatedAsync(DataTablesRequest request)
    {
        try
        {
            var query = _roleManager.Roles.AsNoTracking();
            if (!string.IsNullOrEmpty(request.Search?.Value))
            {
                var searchValue = request.Search.Value.ToLower();
                query = query.Where(p => p.Name.ToLower().Contains(searchValue));
            }
            // Apply ordering
            if (request.Order != null && request.Order.Any())
            {
                var order = request.Order.First();
                var columnName = request.Columns[order.Column].Data;
                var direction = order.Dir;

                // Dynamically apply ordering
                query = direction == "asc"
                    ? query.OrderByDynamic(columnName, true)
                    : query.OrderByDynamic(columnName, false);
            }
            // Get the total count before pagination
            var recordsTotal = await query.CountAsync();
            // Apply pagination
            var paginatedData = await query
                .Skip(request.Start)
                .Take(request.Length)
                .ToListAsync();
            // Now project the roles separately in the mapping RoleDto
            var roleDtos = _mapper.Map<IEnumerable<RoleDto>>(paginatedData);
            var response = new DataTablesResponse<RoleDto>
            {
                Draw = request.Draw,
                RecordsTotal = recordsTotal,
                RecordsFiltered = recordsTotal,
                Data = roleDtos
            };
            return Result<DataTablesResponse<RoleDto>>.Success(response);
        }
        catch (Exception ex)
        {

            return Result<DataTablesResponse<RoleDto>>.Failure(ex.Message);
        }
    }
    public async Task<Result<string>> CreateRoleAsync(string roleName)
    {
        if (await _roleManager.FindByNameAsync(roleName) != null)
        {
            return Result<string>.Failure("Role already exists.");
        }
        var role = new Role
        {
            Name = roleName
        };
        var result = await _roleManager.CreateAsync(role);
        if (!result.Succeeded)
        {
            return Result<string>.Failure(string.Join(", ", result.Errors.Select(e => e.Description)));
        }
        return Result<string>.Success("Role Added");
    }
    public async Task<Result<string>> DeleteRoleAsync(string roleName)
    {
        var role = await _roleManager.FindByNameAsync(roleName);
        if (role == null)
        {
            return Result<string>.Failure("Role not found.");
        }

        var nonDeletableRoleIds = new List<Guid>
    {
        Guid.Parse("5e4d3c2b-a123-4f57-88ef-1ab23cdb3e57"), // Admin role ID
        Guid.Parse("E143EF8A-95C2-4359-B1B6-7FDE456B771F"), // Business Analyses role ID
        Guid.Parse("DEB2A077-7A07-49F4-BDDA-3C7F95061D72"), // Development Department role ID
        Guid.Parse("a1236e5d-42f3-4987-8cbf-6a2bca9f01a4")  // User role ID
    };

        if (nonDeletableRoleIds.Contains(role.Id))
        {
            return Result<string>.Failure($"Cannot delete the {role.Name} role.");
        }

        var result = await _roleManager.DeleteAsync(role);
        if (!result.Succeeded)
        {
            return Result<string>.Failure(string.Join(", ", result.Errors.Select(e => e.Description)));
        }

        return Result<string>.Success();
    }
    public async Task<Result<string>> SoftDeleteRoleAsync(string roleName)
    {
        var role = await _roleManager.FindByNameAsync(roleName);
        if (role == null)
        {
            return Result<string>.Failure("Role not found.");
        }
        role.isDeleted = true;
        var result = await _roleManager.UpdateAsync(role);
        if (!result.Succeeded)
        {
            return Result<string>.Failure(string.Join(", ", result.Errors.Select(e => e.Description)));
        }
        return Result<string>.Success();
    }
    public async Task<Result<string>> UpdateRoleAsync(UpdateRoleDto updateRoleDto)
    {
        try
        {
            var role = await _roleManager.FindByIdAsync(updateRoleDto.Id.ToString());
            if (role == null)
            {
                return Result<string>.Failure("Role not found.");
            }
            role.Name = updateRoleDto.Name;
            var result = await _roleManager.UpdateAsync(role);
            if (!result.Succeeded)
            {
                return Result<string>.Failure(string.Join(", ", result.Errors.Select(e => e.Description)));
            }
            return Result<string>.Success("Role Updated");
        }
        catch (Exception ex)
        {
            return Result<string>.Failure(ex.Message);
        }
    }
    public async Task<Result<RoleDto>> GetRoleById(Guid roleId)
    {
        try
        {
            var role = await _roleManager.FindByIdAsync(roleId.ToString());
            if (role == null)
            {
                return Result<RoleDto>.Failure("Role not found.");
            }
            var roleDto = _mapper.Map<RoleDto>(role);
            return Result<RoleDto>.Success(roleDto);

        }
        catch (Exception ex)
        {

            return Result<RoleDto>.Failure(ex.Message);
        }
    }

    public async Task<Result<string>> GetAllRolesAsHtmlAsync()
    {

        try
        {
            var role = await _roleManager.Roles.ToListAsync();

            // Build HTML options for the dropdown
            var roleDropdown = string.Join(Environment.NewLine,
                role.Select(d => $"<option value='{d.Id}'>{d.Name}</option>"));

            return Result<string>.Success(roleDropdown);

        }
        catch (Exception ex)
        {

            return Result<string>.Failure(ex.Message);
        }
    }
}
