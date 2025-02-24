using Microsoft.AspNetCore.Mvc;
using System.Net;
using TicketTrackingSystem.Application.Dto;
using TicketTrackingSystem.Application.HttpResponse;
using TicketTrackingSystem.Application.Interface;
using TicketTrackingSystem.Common.Model;

namespace TicketTrackingSystem.MVC.Controllers;
[Route("role")]

public class RoleController : Controller
{
    private readonly IRoleService _roleService;
    private readonly IPermissionService _permissionService;
    private readonly IUserService _userService;
    public RoleController(IRoleService roleService, IPermissionService permissionService, IUserService userService)
    {
        _roleService = roleService;
        _permissionService = permissionService;
        _userService = userService;
    }
    public IActionResult Index()
    {
        return View();
    }
    [Route("Details/{id}")]
    public async Task<IActionResult> Details(Guid id)
    {
        var result = await _roleService.GetRoleById(id);
        if (!result.IsSuccess)
        {
            return NotFound();
        }
        return View(result.Value);
    }

    // GET ALL ROLES PAGINATED
    [HttpPost("getallrolespaginatedasync")]
    public async Task<IActionResult> GetAllRolesPaginatedAsync([FromBody] DataTablesRequest dataTablesRequest)
    {
        var response = new BaseResponse();

        if (!ModelState.IsValid)
        {
            response.IsSuccess = false;
            response.SetErrorFromModelState(ModelState);
            return Ok(response);
        }

        var permissions = await CheckPermissionsAsync(PermissionName.ViewRole.ToString());
        if (!permissions[PermissionName.ViewRole.ToString()])
        {
            response.IsSuccess = false;
            response.SetError(new ErrorMessage
            {
                Code = HttpStatusCode.Forbidden,
                Description = "You do not have permission to view roles."
            });
            return Ok(response);
        }

        var result = await _roleService.GetAllRolesPaginatedAsync(dataTablesRequest);
        if (!result.IsSuccess)
        {
            response.IsSuccess = false;
            response.SetError(new ErrorMessage
            {
                Code = HttpStatusCode.InternalServerError,
                Description = result.ErrorMessage
            });
            return Ok(response);
        }

        response.IsSuccess = true;
        response.Data = result.Value;
        return Ok(response);
    }

    // CREATE ROLE
    // Assumes you have a DTO like:
    // public class CreateRoleDto { public string RoleName { get; set; } }
    [HttpPost("createroleasync")]
    public async Task<IActionResult> CreateRoleAsync([FromBody] CreateRoleDto createRoleDto)
    {
        var response = new BaseResponse();

        if (!ModelState.IsValid)
        {
            response.IsSuccess = false;
            response.SetErrorFromModelStateAsDictionary(ModelState);
            return Ok(response);
        }

        var permissions = await CheckPermissionsAsync(PermissionName.CreateRole.ToString());
        if (!permissions[PermissionName.CreateRole.ToString()])
        {
            response.IsSuccess = false;
            response.SetError(new ErrorMessage
            {
                Code = HttpStatusCode.Forbidden,
                Description = "You do not have permission to create roles."
            });
            return Ok(response);
        }

        var result = await _roleService.CreateRoleAsync(createRoleDto.Name);
        if (!result.IsSuccess)
        {
            response.IsSuccess = false;
            response.SetError(new ErrorMessage
            {
                Code = HttpStatusCode.InternalServerError,
                Description = result.ErrorMessage
            });
            return Ok(response);
        }

        response.IsSuccess = true;
        response.Data = result.Value;
        return Ok(response);
    }

    // UPDATE ROLE
    [HttpPut("updateroleasync")]
    public async Task<IActionResult> UpdateRoleAsync([FromBody] UpdateRoleDto updateRoleDto)
    {
        var response = new BaseResponse();

        if (!ModelState.IsValid)
        {
            response.IsSuccess = false;
            response.SetErrorFromModelStateAsDictionary(ModelState);
            return Ok(response);
        }

        var permissions = await CheckPermissionsAsync(PermissionName.EditRole.ToString());
        if (!permissions[PermissionName.EditRole.ToString()])
        {
            response.IsSuccess = false;
            response.SetError(new ErrorMessage
            {
                Code = HttpStatusCode.Forbidden,
                Description = "You do not have permission to edit roles."
            });
            return Ok(response);
        }

        var result = await _roleService.UpdateRoleAsync(updateRoleDto);
        if (!result.IsSuccess)
        {
            response.IsSuccess = false;
            response.SetError(new ErrorMessage
            {
                Code = HttpStatusCode.InternalServerError,
                Description = result.ErrorMessage
            });
            return Ok(response);
        }

        response.IsSuccess = true;
        response.Data = result.Value;
        return Ok(response);
    }

    // DELETE ROLE
    [HttpDelete("deleteroleasync/{roleName}")]
    public async Task<IActionResult> DeleteRoleAsync(string roleName)
    {
        var response = new BaseResponse();

        var permissions = await CheckPermissionsAsync(PermissionName.DeleteRole.ToString());
        if (!permissions[PermissionName.DeleteRole.ToString()])
        {
            response.IsSuccess = false;
            response.SetError(new ErrorMessage
            {
                Code = HttpStatusCode.Forbidden,
                Description = "You do not have permission to delete roles."
            });
            return Ok(response);
        }

        var result = await _roleService.DeleteRoleAsync(roleName);
        if (!result.IsSuccess)
        {
            response.IsSuccess = false;
            response.SetError(new ErrorMessage
            {
                Code = HttpStatusCode.InternalServerError,
                Description = result.ErrorMessage
            });
            return Ok(response);
        }

        response.IsSuccess = true;
        response.Data = result.Value;
        return Ok(response);
    }
    // DELETE ROLE
    [HttpDelete("deleterolecascadeasync/{roleName}")]
    public async Task<IActionResult> DeleteRoleCascadeAsync(string roleName)
    {
        var response = new BaseResponse();

        var permissions = await CheckPermissionsAsync(PermissionName.DeleteRole.ToString());
        if (!permissions[PermissionName.DeleteRole.ToString()])
        {
            response.IsSuccess = false;
            response.SetError(new ErrorMessage
            {
                Code = HttpStatusCode.Forbidden,
                Description = "You do not have permission to delete roles."
            });
            return Ok(response);
        }

        var result = await _roleService.DeleteRoleCascadeAsync(roleName);
        if (!result.IsSuccess)
        {
            response.IsSuccess = false;
            response.SetError(new ErrorMessage
            {
                Code = HttpStatusCode.InternalServerError,
                Description = result.ErrorMessage
            });
            return Ok(response);
        }

        response.IsSuccess = true;
        response.Data = result.Value;
        return Ok(response);
    }
    // GET ALL ROLES AS HTML
    [HttpGet("getallrolesashtmlasync")]
    public async Task<IActionResult> GetAllRolesAsHtmlAsync()
    {
        var response = new BaseResponse();

        var permissions = await CheckPermissionsAsync(PermissionName.ViewRole.ToString());
        if (!permissions[PermissionName.ViewRole.ToString()])
        {
            response.IsSuccess = false;
            response.SetError(new ErrorMessage
            {
                Code = HttpStatusCode.Forbidden,
                Description = "You do not have permission to view roles."
            });
            return Ok(response);
        }

        var result = await _roleService.GetAllRolesAsHtmlAsync();
        if (!result.IsSuccess)
        {
            response.IsSuccess = false;
            response.SetError(new ErrorMessage
            {
                Code = HttpStatusCode.InternalServerError,
                Description = result.ErrorMessage
            });
            return Ok(response);
        }

        response.IsSuccess = true;
        response.Data = result.Value;
        return Ok(response);
    }
    private async Task<Dictionary<string, bool>> CheckPermissionsAsync(params string[] permissionNames)
    {
        if (User.Identity.IsAuthenticated)
        {
            var user = await _userService.GetUserByClaim(User);
            var userId = user.Id;
            return await _permissionService.HasPermissionAsync(userId, permissionNames);
        }
        return permissionNames.ToDictionary(p => p, p => false);
    }
}

