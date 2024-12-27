using System.Security.Claims;
using TicketTrackingSystem.Application.Dto;
using TicketTrackingSystem.Application.Model;
using TicketTrackingSystem.Common.Model;
using TicketTrackingSystem.Core.Model;

namespace TicketTrackingSystem.Application.Interface;
public interface IUserService
{
    Task<Result<DataTablesResponse<UserDto>>> GetAllUsersWithRolePaginatedAsync(DataTablesRequest request);
    Task<Result<string>> CreateUserAsync(CreateUserDto model);
    Task<Result<UserDto>> GetUserByIdAsync(Guid id);
    Task<ApplicationUser> GetUserByClaim(ClaimsPrincipal user);
    Task<Result<DataTablesResponse<UserDto>>> GetAllUsersWithRoleWithConditionPaginatedAsync(DataTablesRequest request, bool withRole, string roleId);
    Task<Result<string>> SetRoleToUserAsync(string userId, string roleId);
    Task<Result<string>> RemoveRoleFromUserAsync(string userId, string roleId);
    string GetUserTypeDropdown();
    Task<Result<string>> DeleteUserAsync(string userId);
    Task<Result<string>> UpdateUserAsync(UpdateUserDto userDto);
}
