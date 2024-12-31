using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;
using TicketTrackingSystem.Application.Dto;
using TicketTrackingSystem.Application.HttpResponse;
using TicketTrackingSystem.Application.Interface;
using TicketTrackingSystem.Common.Model;

namespace TicketTrackingSystem.MVC.Controllers;
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

    [HttpPost("/permission/call")]
    public async Task<IActionResult> CallService([FromBody] DynamicRequest request)
    {
        var permissions = await CheckPermissionsAsync(
            PermissionName.ViewPermission.ToString(),
            PermissionName.CreatePermission.ToString(),
            PermissionName.EditPermission.ToString(),
            PermissionName.DeletePermission.ToString()
            );
        var canView = permissions[PermissionName.ViewPermission.ToString()];
        var canAdd = permissions[PermissionName.CreatePermission.ToString()];
        var canEdit = permissions[PermissionName.EditPermission.ToString()];
        var canDelete = permissions[PermissionName.DeletePermission.ToString()];

        var response = new BaseResponse();
        var parameters = request.Parameters;
        switch (request.Method.ToLower())
        {
            case "getallroleswithpermissionpaginatedasync":
                {
                    try
                    {
                        if (!canView)
                        {
                            response.IsSuccess = false;
                            response.SetError(new ErrorMessage
                            {
                                Code = HttpStatusCode.Forbidden,
                                Description = "You do not have permission to view this page."
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
                        var model = JsonSerializer.Deserialize<DataTablesRequest>(parameters[0].ToString(), new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        });
                        if (model == null)
                        {
                            response.IsSuccess = false;
                            response.SetError(new ErrorMessage
                            {
                                Code = HttpStatusCode.BadRequest,
                                Description = "Invalid request."
                            });
                            break;
                        }
                        var result = await _permissionService.GetAllRolesWithPermissionPaginatedAsync(model);
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

            case "addroletopermissionasync":
                {
                    try
                    {
                        if (!canAdd)
                        {
                            response.IsSuccess = false;
                            response.SetError(new ErrorMessage
                            {
                                Code = HttpStatusCode.Forbidden,
                                Description = "You do not have permission to add a role to a permission."
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
                        var model = JsonSerializer.Deserialize<CreateRolePermissionsDto>(parameters[0].ToString(), new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        });

                        if (model == null || model.RoleId == Guid.Empty || !model.PermissionIds.Any())
                        {
                            response.IsSuccess = false;
                            response.SetError(new ErrorMessage
                            {
                                Code = HttpStatusCode.BadRequest,
                                Description = "Invalid request."
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

            case "removerolefrompermissionasync":
                {
                    try
                    {
                        if (!canAdd)
                        {
                            response.IsSuccess = false;
                            response.SetError(new ErrorMessage
                            {
                                Code = HttpStatusCode.Forbidden,
                                Description = "You do not have permission to add a role to a permission."
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
                        var model = JsonSerializer.Deserialize<CreateRolePermissionDto>(parameters[0].ToString(), new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        });

                        if (model == null || model.RoleId == Guid.Empty || model.PermissionId == Guid.Empty)
                        {
                            response.IsSuccess = false;
                            response.SetError(new ErrorMessage
                            {
                                Code = HttpStatusCode.BadRequest,
                                Description = "Invalid request."
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



            case "getallpermissionsashtmlasync":
                {
                    try
                    {
                        if (!canView)
                        {
                            response.IsSuccess = false;
                            response.SetError(new ErrorMessage
                            {
                                Code = HttpStatusCode.Forbidden,
                                Description = "You do not have permission to view this page."
                            });
                            break;
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
