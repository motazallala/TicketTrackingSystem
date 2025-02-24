using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Net;
using TicketTrackingSystem.Application.Dto;
using TicketTrackingSystem.Application.HttpResponse;
using TicketTrackingSystem.Application.Interface;
using TicketTrackingSystem.Common.Model;

namespace TicketTrackingSystem.MVC.Controllers;

[Route("user")]
public class UserController : Controller
{
    private readonly IUserService _userService;
    private readonly IPermissionService _permissionService;

    public UserController(IUserService userService, IPermissionService permissionService)
    {
        _userService = userService;
        _permissionService = permissionService;
    }
    public IActionResult Index()
    {
        return View();
    }
    [HttpPost("getalluserswithrolepaginatedasync")]
    public async Task<IActionResult> GetAllUsersWithRolePaginatedAsync(
        [FromBody] DataTablesRequest request)
    {
        return await HandleUserOperation(request, PermissionName.ViewUser, async () =>
            await _userService.GetAllUsersWithRolePaginatedAsync(request));
    }

    [HttpPost("getalluserswithrolewithconditionpaginatedasync")]
    public async Task<IActionResult> GetAllUsersWithRoleWithConditionPaginatedAsync(
        [FromBody] UserListRequest request)
    {
        return await HandleUserOperation(request, PermissionName.ViewUser, async () =>
            await _userService.GetAllUsersWithRoleWithConditionPaginatedAsync(
                request.DataTablesRequest,
                request.WithRole,
                request.RoleId));
    }

    [HttpPost("createuserasync")]
    public async Task<IActionResult> CreateUserAsync(
        [FromBody] CreateUserDto request)
    {
        return await HandleUserOperation(request, PermissionName.CreateUser, async () =>
            await _userService.CreateUserAsync(request));
    }

    [HttpPost("removerolefromuserasync")]
    public async Task<IActionResult> RemoveRoleFromUserAsync(
        [FromBody] UserRoleRequest request)
    {
        return await HandleUserOperation(request, PermissionName.DeleteUser, async () =>
            await _userService.RemoveRoleFromUserAsync(request.UserId, request.RoleId));
    }

    [HttpGet("getusertypedropdown")]
    public IActionResult GetUserTypeDropdown()
    {
        return Ok(new BaseResponse
        {
            IsSuccess = true,
            Data = _userService.GetUserTypeDropdown()
        });
    }

    [HttpPost("getprojectmembersasync")]
    public async Task<IActionResult> GetProjectMembersAsync(
        [FromBody] ProjectMembersRequest request)
    {
        return await HandleUserOperation(request, PermissionName.ViewUser, async () =>
            await _userService.GetProjectMembersAsync(
                request.DataTablesRequest,
                request.IsMember,
                request.ProjectId));
    }

    [HttpPost("setroletouserasync")]
    public async Task<IActionResult> SetRoleToUserAsync(
        [FromBody] UserRoleRequest request)
    {
        return await HandleUserOperation(request, PermissionName.EditUser, async () =>
            await _userService.SetRoleToUserAsync(request.UserId, request.RoleId));
    }

    [HttpPost("deleteuserasync")]
    public async Task<IActionResult> DeleteUserAsync(
        [FromBody] UserIdRequest request)
    {
        return await HandleUserOperation(request, PermissionName.DeleteUser, async () =>
            await _userService.DeleteUserAsync(request.UserId));
    }

    [HttpPost("deleteusercascadeasync")]
    public async Task<IActionResult> DeleteUserCascadeAsync(
        [FromBody] UserIdRequest request)
    {
        return await HandleUserOperation(request, PermissionName.DeleteUser, async () =>
            await _userService.DeleteUserCascadeAsync(request.UserId));
    }

    [HttpPost("updateuserasync")]
    public async Task<IActionResult> UpdateUserAsync(
        [FromBody] UpdateUserDto request)
    {
        return await HandleUserOperation(request, PermissionName.EditUser, async () =>
            await _userService.UpdateUserAsync(request));
    }

    [HttpPost("getuserbyidasync")]
    public async Task<IActionResult> GetUserByIdAsync(
        [FromBody] UserIdRequest request)
    {
        return await HandleUserOperation(request, PermissionName.ViewUser, async () =>
            await _userService.GetUserByIdAsync(Guid.Parse(request.UserId)));
    }

    #region Common Handlers
    private async Task<IActionResult> HandleUserOperation<T>(
        T request,
        PermissionName requiredPermission,
        Func<Task<dynamic>> operation)
    {
        var response = new BaseResponse();
        var permissionName = requiredPermission.ToString();

        // Check permissions
        var permissions = await CheckPermissionsAsync(permissionName);
        if (!permissions.ContainsKey(permissionName) || !permissions[permissionName])
        {
            response.SetError(new ErrorMessage
            {
                Code = HttpStatusCode.Forbidden,
                Description = $"No permission to {requiredPermission.ToString().ToLower()}"
            });
            return Ok(response);
        }

        // Validate request
        if (!ModelState.IsValid)
        {
            response.SetErrorFromModelState(ModelState);
            return Ok(response);
        }

        try
        {
            var result = await operation();
            return HandleServiceResult(response, result);
        }
        catch (Exception ex)
        {
            return HandleException(response, ex);
        }
    }

    private IActionResult HandleServiceResult(BaseResponse response, dynamic result)
    {
        if (result.IsSuccess)
        {
            response.IsSuccess = true;
            response.Data = result.Value;
            return Ok(response);
        }

        response.SetError(new ErrorMessage
        {
            Code = HttpStatusCode.BadRequest,
            Description = result.ErrorMessage
        });
        return Ok(response);
    }

    private IActionResult HandleException(BaseResponse response, Exception ex)
    {
        response.SetError(new ErrorMessage
        {
            Code = HttpStatusCode.InternalServerError,
            Description = ex.Message,
        });
        return Ok(response);
    }

    private async Task<Dictionary<string, bool>> CheckPermissionsAsync(params string[] permissionNames)
    {
        if (User.Identity.IsAuthenticated)
        {
            var user = await _userService.GetUserByClaim(User);
            return await _permissionService.HasPermissionAsync(user.Id, permissionNames);
        }
        return permissionNames.ToDictionary(p => p, p => false);
    }
    #endregion
}

#region DTOs
public class UserListRequest
{
    public DataTablesRequest DataTablesRequest { get; set; }
    public bool WithRole { get; set; }
    public string RoleId { get; set; }
}

public class UserRoleRequest
{
    [Required]
    public string UserId { get; set; }

    [Required]
    public string RoleId { get; set; }
}

public class ProjectMembersRequest
{
    public DataTablesRequest DataTablesRequest { get; set; }
    public bool IsMember { get; set; }
    public Guid ProjectId { get; set; }
}

public class UserIdRequest
{
    [Required]
    public string UserId { get; set; }
}
#endregion