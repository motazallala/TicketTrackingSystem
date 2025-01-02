using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;
using TicketTrackingSystem.Application.Dto;
using TicketTrackingSystem.Application.HttpResponse;
using TicketTrackingSystem.Application.Interface;
using TicketTrackingSystem.Common.Model;

namespace TicketTrackingSystem.MVC.Controllers;
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
    public async Task<IActionResult> Details(Guid id)
    {
        var result = await _roleService.GetRoleById(id);
        if (!result.IsSuccess)
        {
            return NotFound();
        }
        return View(result.Value);
    }

    [HttpPost("/role/call")]
    public async Task<IActionResult> CallService([FromBody] DynamicRequest request)
    {
        var permissions = await CheckPermissionsAsync(
            PermissionName.ViewRole.ToString(),
            PermissionName.CreateRole.ToString(),
            PermissionName.EditRole.ToString(),
            PermissionName.DeleteRole.ToString()
            );
        var canView = permissions[PermissionName.ViewRole.ToString()];
        var canAdd = permissions[PermissionName.CreateRole.ToString()];
        var canEdit = permissions[PermissionName.EditRole.ToString()];
        var canDelete = permissions[PermissionName.DeleteRole.ToString()];

        var response = new BaseResponse();
        var parameters = request.Parameters;
        switch (request.Method.ToLower())
        {
            case "getallrolespaginatedasync":
                {
                    if (!canView)
                    {
                        response.IsSuccess = false;
                        response.SetError(new ErrorMessage
                        {
                            Code = HttpStatusCode.Forbidden,
                            Description = "You do not have permission to view roles."
                        });
                        return Ok(response);
                    }
                    if (parameters.Length < 1 || !(parameters[0] is JsonElement requestElement))
                    {
                        response.IsSuccess = false;
                        response.SetError(new ErrorMessage
                        {
                            Code = HttpStatusCode.BadRequest,
                            Description = "Invalid request."
                        });
                        break;
                    }
                    try
                    {
                        var dataTableRequest = JsonSerializer.Deserialize<DataTablesRequest>(requestElement.GetRawText(), new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        });
                        if (dataTableRequest == null)
                        {
                            response.IsSuccess = false;
                            response.SetError(new ErrorMessage
                            {
                                Code = HttpStatusCode.BadRequest,
                                Description = "Invalid request."
                            });
                            break;
                        }
                        var result = await _roleService.GetAllRolesPaginatedAsync(dataTableRequest);
                        if (!result.IsSuccess)
                        {
                            response.IsSuccess = false;
                            response.SetError(new ErrorMessage
                            {
                                Code = HttpStatusCode.InternalServerError,
                                Description = result.ErrorMessage
                            });
                        }
                        response.IsSuccess = true;
                        response.Data = result.Value;
                        break;
                    }
                    catch (Exception ex)
                    {
                        response.IsSuccess = false;
                        response.SetError(new ErrorMessage
                        {
                            Code = HttpStatusCode.InternalServerError,
                            Description = ex.Message
                        });
                        break;
                    }
                }
            case "createroleasync":
                {
                    try
                    {
                        if (!canAdd)
                        {
                            response.IsSuccess = false;
                            response.SetError(new ErrorMessage
                            {
                                Code = HttpStatusCode.Forbidden,
                                Description = "You do not have permission to create roles."
                            });
                            break;
                        }
                        if (parameters.Length < 1 || string.IsNullOrEmpty(request.Parameters[0]?.ToString()))
                        {
                            response.IsSuccess = false;
                            response.SetError(new ErrorMessage
                            {
                                Code = HttpStatusCode.BadRequest,
                                Description = "Invalid request."
                            });
                            break;
                        }
                        var roleName = request.Parameters[0].ToString();
                        var result = await _roleService.CreateRoleAsync(roleName);
                        if (!result.IsSuccess)
                        {
                            response.IsSuccess = false;
                            response.SetError(new ErrorMessage
                            {
                                Code = HttpStatusCode.InternalServerError,
                                Description = result.ErrorMessage
                            });
                        }
                        response.IsSuccess = true;
                        response.Data = result.Value;
                        break;
                    }
                    catch (Exception ex)
                    {
                        response.IsSuccess = false;
                        response.SetError(new ErrorMessage
                        {
                            Code = HttpStatusCode.InternalServerError,
                            Description = ex.Message
                        });
                        break;
                    }
                }
            case "updateroleasync":
                {
                    try
                    {
                        if (!canEdit)
                        {
                            response.IsSuccess = false;
                            response.SetError(new ErrorMessage
                            {
                                Code = HttpStatusCode.Forbidden,
                                Description = "You do not have permission to edit roles."
                            });
                            break;
                        }
                        if (parameters.Length < 1 || string.IsNullOrEmpty(parameters[0]?.ToString()))
                        {
                            response.IsSuccess = false;
                            response.SetError(new ErrorMessage
                            {
                                Code = HttpStatusCode.BadRequest,
                                Description = "Invalid request."
                            });
                            break;
                        }
                        var roleDto = JsonSerializer.Deserialize<UpdateRoleDto>(parameters[0].ToString(), new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        });
                        if (roleDto == null)
                        {
                            response.IsSuccess = false;
                            response.SetError(new ErrorMessage
                            {
                                Code = HttpStatusCode.BadRequest,
                                Description = "Invalid request."
                            });
                            break;
                        }
                        var result = await _roleService.UpdateRoleAsync(roleDto);
                        if (!result.IsSuccess)
                        {
                            response.IsSuccess = false;
                            response.SetError(new ErrorMessage
                            {
                                Code = HttpStatusCode.InternalServerError,
                                Description = result.ErrorMessage
                            });
                        }
                        response.IsSuccess = true;
                        response.Data = result.Value;
                        break;
                    }
                    catch (Exception)
                    {
                        response.IsSuccess = false;
                        response.SetError(new ErrorMessage
                        {
                            Code = HttpStatusCode.InternalServerError,
                            Description = "An error occurred while updating the role."
                        });
                        break;
                    }
                }
            case "deleteroleasync":
                {
                    try
                    {
                        if (!canDelete)
                        {
                            response.IsSuccess = false;
                            response.SetError(new ErrorMessage
                            {
                                Code = HttpStatusCode.Forbidden,
                                Description = "You do not have permission to delete roles."
                            });
                            break;
                        }
                        if (parameters.Length < 1 || string.IsNullOrEmpty(parameters[0]?.ToString()))
                        {
                            response.IsSuccess = false;
                            response.SetError(new ErrorMessage
                            {
                                Code = HttpStatusCode.BadRequest,
                                Description = "Invalid request."
                            });
                            break;
                        }
                        var roleName = parameters[0].ToString();
                        var result = await _roleService.DeleteRoleAsync(roleName);
                        if (!result.IsSuccess)
                        {
                            response.IsSuccess = false;
                            response.SetError(new ErrorMessage
                            {
                                Code = HttpStatusCode.InternalServerError,
                                Description = result.ErrorMessage
                            });
                            break;
                        }
                        response.IsSuccess = true;
                        response.Data = result.Value;
                        break;
                    }
                    catch (Exception ex)
                    {
                        response.IsSuccess = false;
                        response.SetError(new ErrorMessage
                        {
                            Code = HttpStatusCode.InternalServerError,
                            Description = ex.Message
                        });
                        break;
                    }
                }

            case "getallrolesashtmlasync":
                {
                    if (!canView)
                    {
                        response.IsSuccess = false;
                        response.SetError(new ErrorMessage
                        {
                            Code = HttpStatusCode.Forbidden,
                            Description = "You do not have permission to view roles."
                        });
                        break;
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
                        break;
                    }
                    response.IsSuccess = true;
                    response.Data = result.Value;
                    break;
                }

            default:
                {
                    response.IsSuccess = false;
                    response.SetError(new ErrorMessage
                    {
                        Code = HttpStatusCode.NotFound,
                        Description = "Method not found."
                    });
                    break;
                }
        }
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
