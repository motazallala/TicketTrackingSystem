using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TicketTrackingSystem.Application.Dto;
using TicketTrackingSystem.Application.Interface;
using TicketTrackingSystem.Application.Model;
using TicketTrackingSystem.Common.ExtensionMethod;
using TicketTrackingSystem.Common.Model;
using TicketTrackingSystem.Core.Model;
using TicketTrackingSystem.Core.Model.Enum;
using TicketTrackingSystem.DAL.Interface;

namespace TicketTrackingSystem.Application.Services;
public class UserService : IUserService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<Role> _roleManager;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UserService(UserManager<ApplicationUser> userManager, RoleManager<Role> roleManager, IMapper mapper, IUnitOfWork unitOfWork)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    public async Task<Result<string>> CreateUserAsync(CreateUserDto model)
    {
        try
        {
            if (await _userManager.FindByEmailAsync(model.Email) is not null)
            {
                return Result<string>.Failure("Email already exists.");
            }
            if (await _userManager.FindByNameAsync(model.UserName) is not null)
            {
                return Result<string>.Failure("Username already exists.");
            }
            //check if the department exist
            if (!string.IsNullOrEmpty(model.DepartmentId))
            {
                if (await _unitOfWork.Departments.GetByIdAsync(Guid.Parse(model.DepartmentId)) is null)
                {
                    return Result<string>.Failure("Department not exist.");
                }
            }
            else
            {
                model.DepartmentId = null;
            }
            var user = new ApplicationUser
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                UserName = model.UserName,
                Email = model.Email,
                UserType = (UserType)int.Parse(model.UserType), // Explicit conversion
                DepartmentId = model.DepartmentId is not null ? Guid.Parse(model.DepartmentId) : null,
                PhoneNumber = model.PhoneNumber
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                return Result<string>.Failure(string.Join(", ", result.Errors.Select(e => e.Description)));
            }

            return Result<string>.Success();
        }
        catch (Exception ex)
        {

            return Result<string>.Failure("Something went wrong : " + ex.Message);
        }
    }
    //get the list of user type as html string dropdown
    public string GetUserTypeDropdown()
    {
        var userType = Enum.GetValues(typeof(UserType)).Cast<UserType>().Select(v => new SelectListItem
        {
            Value = ((int)v).ToString(),
            Text = v.ToString()
        }).ToList();

        var htmlString = new System.Text.StringBuilder();
        foreach (var item in userType)
        {
            htmlString.Append($"<option value='{item.Value}'>{item.Text}</option>");
        }
        return htmlString.ToString();
    }

    public async Task<Result<DataTablesResponse<UserDto>>> GetAllUsersWithRolePaginatedAsync(DataTablesRequest request)
    {
        try
        {
            var query = _userManager.Users.AsQueryable().AsNoTracking();

            // Apply global search filter
            if (!string.IsNullOrEmpty(request.Search?.Value))
            {
                var searchValue = request.Search.Value.ToLower();

                // Enum search handling for UserType
                if (Enum.TryParse<UserType>(searchValue, true, out var userType))
                {
                    // Filter by UserType if it's a valid enum value
                    query = query.Where(p => p.UserType == userType);
                }
                else
                {
                    // Apply global string-based search for other fields
                    query = query.Where(p => p.FirstName.ToLower().Contains(searchValue)
                                            || p.LastName.ToLower().Contains(searchValue)
                                            || p.Email.ToLower().Contains(searchValue)
                                            || p.UserName.ToLower().Contains(searchValue)
                                            || p.Department.Name.ToLower().Contains(searchValue)
                                            || p.Roles.Any(x => x.Role.Name.ToLower().Contains(searchValue)));
                }
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
                .Include(d => d.Department)
                .Include(r => r.Roles) // Include the Roles collection
                .ThenInclude(ur => ur.Role)
                .ToListAsync();

            // Now project the roles separately in the mapping phase
            var userDtos = _mapper.Map<IEnumerable<UserDto>>(paginatedData);
            var response = new DataTablesResponse<UserDto>
            {
                Draw = request.Draw,
                RecordsTotal = recordsTotal,
                RecordsFiltered = recordsTotal,
                Data = userDtos
            };

            return Result<DataTablesResponse<UserDto>>.Success(response);
        }
        catch (Exception ex)
        {
            return Result<DataTablesResponse<UserDto>>.Failure(ex.Message);
        }
    }

    public async Task<Result<UserDto>> GetUserByIdAsync(Guid id)
    {
        try
        {
            var user = await _userManager.Users
        .Include(d => d.Department)
        .Include(r => r.Roles)
        .ThenInclude(ur => ur.Role)
        .FirstOrDefaultAsync(u => u.Id == id);
            if (user == null)
            {
                return Result<UserDto>.Failure("User not found.");
            }
            var userDto = _mapper.Map<UserDto>(user);
            return Result<UserDto>.Success(userDto);
        }
        catch (Exception ex)
        {

            return Result<UserDto>.Failure(ex.Message);
        }
    }
    //get user by claim
    public async Task<ApplicationUser> GetUserByClaim(ClaimsPrincipal user)
    {
        return await _userManager.GetUserAsync(user);
    }
    public async Task<Result<DataTablesResponse<UserDto>>> GetAllUsersWithRoleWithConditionPaginatedAsync(DataTablesRequest request, bool withRole, string roleId)
    {
        try
        {
            IQueryable<ApplicationUser> query;
            if (withRole)
            {
                query = _userManager.Users.AsQueryable().Where(u => u.Roles.Any(r => r.RoleId == Guid.Parse(roleId))).AsNoTracking();
            }
            else
            {
                query = _userManager.Users.AsQueryable()
                    .Where(u => !u.Roles.Any(r => r.RoleId == Guid.Parse(roleId)) || !u.Roles.Any())
                    .AsNoTracking();
            }

            // Apply global search filter
            if (!string.IsNullOrEmpty(request.Search?.Value))
            {
                var searchValue = request.Search.Value.ToLower();

                // Enum search handling for UserType
                if (Enum.TryParse<UserType>(searchValue, true, out var userType))
                {
                    // Filter by UserType if it's a valid enum value
                    query = query.Where(p => p.UserType == userType);
                }
                else
                {
                    // Apply global string-based search for other fields
                    query = query.Where(p => p.FirstName.ToLower().Contains(searchValue)
                                            || p.LastName.ToLower().Contains(searchValue)
                                            || p.Email.ToLower().Contains(searchValue)
                                            || p.UserName.ToLower().Contains(searchValue)
                                            || p.Department.Name.ToLower().Contains(searchValue)
                                            || p.Roles.Any(x => x.Role.Name.ToLower().Contains(searchValue)));
                }
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
                .Include(r => r.Roles) // Include the Roles collection
                .ThenInclude(ur => ur.Role)
                .ToListAsync();

            // Now project the roles separately in the mapping phase
            var userDtos = _mapper.Map<IEnumerable<UserDto>>(paginatedData);
            var response = new DataTablesResponse<UserDto>
            {
                Draw = request.Draw,
                RecordsTotal = recordsTotal,
                RecordsFiltered = recordsTotal,
                Data = userDtos
            };

            return Result<DataTablesResponse<UserDto>>.Success(response);
        }
        catch (Exception ex)
        {
            return Result<DataTablesResponse<UserDto>>.Failure(ex.Message);
        }
    }

    public async Task<Result<string>> SetRoleToUserAsync(string userId, string roleId)
    {
        try
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user is null)
            {
                return Result<string>.Failure("User not exist");
            }
            var role = await _roleManager.FindByIdAsync(roleId);
            if (role is null)
            {
                return Result<string>.Failure("Role not exist");
            }
            var result = await _userManager.AddToRoleAsync(user, role.Name);
            if (!result.Succeeded)
            {
                return Result<string>.Failure(string.Join("; ", result.Errors.Select(e => e.Description)));
            }
            return Result<string>.Success("The Role Has Been Added successful");
        }
        catch (Exception ex)
        {

            return Result<string>.Failure(ex.Message);
        }

    }
    //remove role from user
    public async Task<Result<string>> RemoveRoleFromUserAsync(string userId, string roleId)
    {
        try
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user is null)
            {
                return Result<string>.Failure("User not exist");
            }
            var role = await _roleManager.FindByIdAsync(roleId);
            if (role is null)
            {
                return Result<string>.Failure("Role not exist");
            }
            var result = await _userManager.RemoveFromRoleAsync(user, role.Name);
            if (!result.Succeeded)
            {
                return Result<string>.Failure(string.Join("; ", result.Errors.Select(e => e.Description)));
            }
            return Result<string>.Success("The Role Has Been Removed successful");
        }
        catch (Exception ex)
        {
            return Result<string>.Failure(ex.Message);
        }
    }

    public async Task<Result<string>> DeleteUserAsync(string userId)
    {
        try
        {
            // Find the user
            var user = await _userManager.FindByIdAsync(userId);
            if (user is null)
            {
                return Result<string>.Failure("There is no user with this Id");
            }

            // Prevent deletion of a specific admin
            if (user.Id == Guid.Parse("d4d6e58f-8f94-4e8c-93c7-d048e24e2639"))
            {
                return Result<string>.Failure($"{user.UserName} is an Admin. You cannot delete it!");
            }

            // Check for dependent TicketHistory records
            var hasTicketHistory = await _unitOfWork.TicketHistory.CheckItemExistenceAsync(th => th.AssignedToId == user.Id);
            if (hasTicketHistory)
            {
                return Result<string>.Failure("This user has associated ticket history. Delete or reassign those records first.");
            }

            // Check for dependent TicketHistory records
            var hasTicketMessage = await _unitOfWork.TicketMessage.CheckItemExistenceAsync(th => th.UserId == user.Id);
            if (hasTicketMessage)
            {
                return Result<string>.Failure("This user has associated ticket message. Delete or reassign those records first.");
            }

            // Attempt to delete the user
            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded)
            {
                return Result<string>.Failure(string.Join("; ", result.Errors.Select(e => e.Description)));
            }

            return Result<string>.Success("User deleted successfully");
        }
        catch (Exception ex)
        {

            return Result<string>.Failure($"An error occurred: {ex.Message}");
        }
    }


    public async Task<Result<string>> DeleteUserCascadeAsync(string userId)
    {
        try
        {
            // Find the user
            var user = await _userManager.FindByIdAsync(userId);
            if (user is null)
            {
                return Result<string>.Failure("There is no user with this Id");
            }

            // Prevent deletion of a specific admin
            if (user.Id == Guid.Parse("d4d6e58f-8f94-4e8c-93c7-d048e24e2639"))
            {
                return Result<string>.Failure($"{user.UserName} is an Admin. You cannot delete it!");
            }

            _unitOfWork.TicketHistory.DeleteAllHistoryForUser(user.Id);
            _unitOfWork.TicketMessage.DeleteAllMessageForUser(user.Id);
            await _unitOfWork.CompleteAsync();
            // Attempt to delete the user
            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded)
            {
                return Result<string>.Failure(string.Join("; ", result.Errors.Select(e => e.Description)));
            }

            return Result<string>.Success("User deleted successfully");
        }
        catch (Exception ex)
        {

            return Result<string>.Failure($"An error occurred: {ex.Message}");
        }
    }

    //update user
    public async Task<Result<string>> UpdateUserAsync(UpdateUserDto userDto)
    {
        try
        {
            //check if the department exist
            if (!string.IsNullOrEmpty(userDto.DepartmentId))
            {
                if (await _unitOfWork.Departments.GetByIdAsync(Guid.Parse(userDto.DepartmentId)) is null)
                {
                    return Result<string>.Failure("Department not exist.");
                }
            }
            else
            {
                userDto.DepartmentId = null;
            }
            var user = await _userManager.FindByIdAsync(userDto.Id.ToString());
            if (user is null)
            {
                return Result<string>.Failure("User not found.");
            }
            //check if the email exist
            if (await _userManager.Users.AnyAsync(u => u.Email == userDto.Email && u.Id != user.Id))
            {
                return Result<string>.Failure("Email already exists.");
            }
            //check if the username exist
            if (await _userManager.Users.AnyAsync(u => u.UserName == userDto.UserName && u.Id != user.Id))
            {
                return Result<string>.Failure("Username already exists.");
            }
            user.FirstName = userDto.FirstName;
            user.LastName = userDto.LastName;
            user.Email = userDto.Email;
            user.PhoneNumber = userDto.PhoneNumber;
            user.UserType = (UserType)int.Parse(userDto.UserType);
            user.DepartmentId = userDto.DepartmentId is not null ? Guid.Parse(userDto.DepartmentId) : null;
            var result = await _userManager.UpdateAsync(user);
            //check if the password is not null or empty then update the password
            if (!string.IsNullOrEmpty(userDto.Password))
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                result = await _userManager.ResetPasswordAsync(user, token, userDto.Password);
                if (!result.Succeeded)
                {
                    return Result<string>.Failure(string.Join(", ", result.Errors.Select(e => e.Description)));
                }
            }
            if (!result.Succeeded)
            {
                return Result<string>.Failure(string.Join(", ", result.Errors.Select(e => e.Description)));
            }
            return Result<string>.Success();
        }
        catch (Exception ex)
        {

            return Result<string>.Failure(ex.Message);
        }
    }

    public async Task<Result<DataTablesResponse<UserDto>>> GetProjectMembersAsync(DataTablesRequest request, bool isMember, Guid projectId)
    {
        try
        {
            var query = _unitOfWork.Users.GetAllAsQueryable();

            if (isMember)
            {
                query = query.Where(m => m.ProjectMembers.Any(p => p.ProjectId == projectId)).AsNoTracking();
            }
            else
            {
                query = query.Where(u => !u.ProjectMembers.Any(p => p.ProjectId == projectId || !u.ProjectMembers.Any())).AsNoTracking();
            }

            if (!string.IsNullOrEmpty(request.Search?.Value))
            {
                var searchValue = request.Search.Value.ToLower();
                query = query.Where(p => p.UserName.ToLower().Contains(searchValue) || p.Email.ToLower().Contains(searchValue) ||
                                    p.FirstName.ToLower().Contains(searchValue) || p.LastName.ToLower().Contains(searchValue) ||
                                    p.PhoneNumber.ToLower().Contains(searchValue));
            }
            //// Apply ordering
            //if (request.Order != null && request.Order.Any())
            //{
            //    var order = request.Order.First();
            //    var columnName = request.Columns[order.Column].Data;
            //    var direction = order.Dir;
            //    // Dynamically apply ordering
            //    query = direction == "asc"
            //        ? query.OrderByDynamic(columnName, true)
            //        : query.OrderByDynamic(columnName, false);
            //}

            // Get the total count before pagination
            var recordsTotal = await query.CountAsync();

            var paginatedData = await query
                .Include(r => r.Roles)
                .ThenInclude(ur => ur.Role)
                .Skip(request.Start)
                .Take(request.Length)
                .ToListAsync();
            var projectDtos = _mapper.Map<IEnumerable<UserDto>>(paginatedData);
            var response = new DataTablesResponse<UserDto>
            {
                Draw = request.Draw,
                RecordsTotal = recordsTotal,
                RecordsFiltered = recordsTotal,
                Data = projectDtos
            };
            return Result<DataTablesResponse<UserDto>>.Success(response);
        }
        catch (Exception ex)
        {

            return Result<DataTablesResponse<UserDto>>.Failure(ex.Message);
        }


    }

    //get the user stage from project member
    public async Task<Result<string>> GetUserStageFromProjectMemberAsync(Guid userId, Guid projectId)
    {
        var projectMember = await _unitOfWork.ProjectMembers.GetSingleOrDefaultAsync(u => u.UserId == userId && u.ProjectId == projectId, false);
        if (projectMember is null)
        {
            return Result<string>.Failure("User is not a member of this project");
        }
        if (projectMember.Stage.Equals(Stage.NoStage))
        {
            return Result<string>.Success("All Tickets");

        }
        else if (projectMember.Stage.Equals(Stage.Stage1))
        {
            return Result<string>.Success("Stage 1 Tickets");
        }
        else if (projectMember.Stage.Equals(Stage.Stage2))
        {
            return Result<string>.Success("Stage 2 Tickets");
        }
        return Result<string>.Failure("There is no Stage ");
    }
}
