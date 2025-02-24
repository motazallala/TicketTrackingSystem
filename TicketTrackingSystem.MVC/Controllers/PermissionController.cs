using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using TicketTrackingSystem.Application.Dto;
using TicketTrackingSystem.Application.HttpResponse;
using TicketTrackingSystem.Application.Interface;
using TicketTrackingSystem.Common.Model;

namespace TicketTrackingSystem.MVC.Controllers
{
    [Authorize]
    [Route("permission")]
    public class PermissionController : Controller
    {
        private readonly IPermissionService _permissionService;
        private readonly IUserService _userService;

        public PermissionController(IPermissionService permissionService, IUserService userService)
        {
            _permissionService = permissionService;
            _userService = userService;
        }

        public IActionResult Index()
        {
            return View();
        }

        // GET ALL ROLES WITH PERMISSION PAGINATED
        [HttpPost("getallroleswithpermissionpaginatedasync")]
        public async Task<IActionResult> GetAllRolesWithPermissionPaginatedAsync([FromBody] DataTablesRequest dataTablesRequest)
        {
            var response = new BaseResponse();

            if (!ModelState.IsValid)
            {
                response.IsSuccess = false;
                response.SetErrorFromModelState(ModelState);
                return Ok(response);
            }

            var permissions = await CheckPermissionsAsync(PermissionName.ViewPermission.ToString());
            if (!permissions[PermissionName.ViewPermission.ToString()])
            {
                response.IsSuccess = false;
                response.SetError(new ErrorMessage
                {
                    Code = HttpStatusCode.Forbidden,
                    Description = "You do not have permission to view this page."
                });
                return Ok(response);
            }

            var result = await _permissionService.GetAllRolesWithPermissionPaginatedAsync(dataTablesRequest);
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

        // ADD ROLE TO PERMISSIONS
        [HttpPost("addroletopermissionasync")]
        public async Task<IActionResult> AddRoleToPermissionAsync([FromBody] CreateRolePermissionsDto model)
        {
            var response = new BaseResponse();

            if (!ModelState.IsValid)
            {
                response.IsSuccess = false;
                response.SetErrorFromModelState(ModelState);
                return Ok(response);
            }

            var permissions = await CheckPermissionsAsync(PermissionName.CreatePermission.ToString());
            if (!permissions[PermissionName.CreatePermission.ToString()])
            {
                response.IsSuccess = false;
                response.SetError(new ErrorMessage
                {
                    Code = HttpStatusCode.Forbidden,
                    Description = "You do not have permission to add a role to a permission."
                });
                return Ok(response);
            }


            var result = await _permissionService.AddRoleToPermissionsAsync(model);
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

        // REMOVE ROLE FROM PERMISSION
        [HttpDelete("removerolefrompermissionasync")]
        public async Task<IActionResult> RemoveRoleFromPermissionAsync([FromBody] CreateRolePermissionDto model)
        {
            var response = new BaseResponse();

            if (!ModelState.IsValid)
            {
                response.IsSuccess = false;
                response.SetErrorFromModelState(ModelState);
                return Ok(response);
            }

            // You might use a separate permission check here if needed.
            var permissions = await CheckPermissionsAsync(PermissionName.CreatePermission.ToString());
            if (!permissions[PermissionName.CreatePermission.ToString()])
            {
                response.IsSuccess = false;
                response.SetError(new ErrorMessage
                {
                    Code = HttpStatusCode.Forbidden,
                    Description = "You do not have permission to remove a role from a permission."
                });
                return Ok(response);
            }

            var result = await _permissionService.RemoveRoleFromPermissionAsync(model);
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

        // GET ALL PERMISSIONS AS HTML
        [HttpGet("getallpermissionsashtmlasync")]
        public async Task<IActionResult> GetAllPermissionsAsHtmlAsync()
        {
            var response = new BaseResponse();

            var permissions = await CheckPermissionsAsync(PermissionName.ViewPermission.ToString());
            if (!permissions[PermissionName.ViewPermission.ToString()])
            {
                response.IsSuccess = false;
                response.SetError(new ErrorMessage
                {
                    Code = HttpStatusCode.Forbidden,
                    Description = "You do not have permission to view this page."
                });
                return Ok(response);
            }

            var result = await _permissionService.GetAllPermissionsAsHtmlAsync();
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

        // Shared method to check permissions.
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
}
