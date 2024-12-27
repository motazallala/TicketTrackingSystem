using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;
using TicketTrackingSystem.Application.Dto;
using TicketTrackingSystem.Application.HttpResponse;
using TicketTrackingSystem.Application.Interface;
using TicketTrackingSystem.Common.Model;

namespace TicketTrackingSystem.MVC.Controllers;
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

    [HttpPost("/user/call")]
    public async Task<IActionResult> CallService([FromBody] DynamicRequest request)
    {
        // Check necessary permissions
        var permissions = await CheckPermissionsAsync(
            PermissionName.ViewUser.ToString(),
            PermissionName.CreateUser.ToString(),
            PermissionName.EditUser.ToString(),
            PermissionName.DeleteUser.ToString()
        );

        var canView = permissions[PermissionName.ViewUser.ToString()];
        var canAdd = permissions[PermissionName.CreateUser.ToString()];
        var canEdit = permissions[PermissionName.EditUser.ToString()];
        var canDelete = permissions[PermissionName.DeleteUser.ToString()];

        var response = new BaseResponse();
        var parameters = request.Parameters;
        switch (request.Method.ToLower())
        {
            case "getalluserswithrolepaginatedasync":
                {
                    try
                    {
                        if (!canView)
                        {
                            response.IsSuccess = false;
                            response.SetError(new ErrorMessage
                            {
                                Code = HttpStatusCode.Forbidden,
                                Description = "You do not have permission to view users."
                            });
                            break;
                        }
                        if (parameters.Length < 1 || !(parameters[0] is JsonElement requestElement))
                        {
                            response.IsSuccess = false;
                            response.SetError(new ErrorMessage
                            {
                                Code = HttpStatusCode.BadRequest,
                                Description = "Invalid pagination request data."
                            });
                            break;
                        }
                        var dataTablesRequest = JsonSerializer.Deserialize<DataTablesRequest>(requestElement.GetRawText(), new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        });
                        if (dataTablesRequest == null)
                        {
                            response.IsSuccess = false;
                            response.SetError(new ErrorMessage
                            {
                                Code = HttpStatusCode.BadRequest,
                                Description = "Invalid DataTablesRequest format."
                            });
                            break;
                        }
                        var paginationResult = await _userService.GetAllUsersWithRolePaginatedAsync(dataTablesRequest);
                        if (!paginationResult.IsSuccess)
                        {
                            response.IsSuccess = false;
                            response.SetError(new ErrorMessage
                            {
                                Code = HttpStatusCode.InternalServerError,
                                Description = paginationResult.ErrorMessage
                            });
                            break;
                        }
                        response.IsSuccess = true;
                        response.Data = paginationResult.Value;
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
            case "getalluserswithrolewithconditionpaginatedasync":
                {
                    try
                    {
                        if (!canView)
                        {
                            response.IsSuccess = false;
                            response.SetError(new ErrorMessage
                            {
                                Code = HttpStatusCode.Forbidden,
                                Description = "You do not have permission to view users."
                            });
                            break;
                        }

                        // Validate the number of parameters
                        if (parameters.Length < 3 || !(parameters[0] is JsonElement requestElement) || !bool.TryParse(parameters[1].ToString(), out bool withRole) || string.IsNullOrEmpty(parameters[2]?.ToString()))
                        {
                            response.IsSuccess = false;
                            response.SetError(new ErrorMessage
                            {
                                Code = HttpStatusCode.BadRequest,
                                Description = "Invalid request parameters. Expected pagination request, withRole, and roleId."
                            });
                            break;
                        }

                        // Deserialize the DataTablesRequest
                        var dataTablesRequest = JsonSerializer.Deserialize<DataTablesRequest>(requestElement.GetRawText(), new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        });
                        if (dataTablesRequest == null)
                        {
                            response.IsSuccess = false;
                            response.SetError(new ErrorMessage
                            {
                                Code = HttpStatusCode.BadRequest,
                                Description = "Invalid DataTablesRequest format."
                            });
                            break;
                        }
                        var roleId = parameters[2].ToString();
                        // Call the service method with the extracted parameters
                        var paginationResult = await _userService.GetAllUsersWithRoleWithConditionPaginatedAsync(dataTablesRequest, withRole, roleId);
                        if (!paginationResult.IsSuccess)
                        {
                            response.IsSuccess = false;
                            response.SetError(new ErrorMessage
                            {
                                Code = HttpStatusCode.InternalServerError,
                                Description = paginationResult.ErrorMessage
                            });
                            break;
                        }

                        // Success response
                        response.IsSuccess = true;
                        response.Data = paginationResult.Value;
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
            case "createuserasync":
                {
                    try
                    {
                        if (!canAdd)
                        {
                            response.IsSuccess = false;
                            response.SetError(new ErrorMessage
                            {
                                Code = HttpStatusCode.Forbidden,
                                Description = "You do not have permission to create users."
                            });
                            break;
                        }
                        //get create users dto and check if the parameters are valid and if yes serilze user
                        if (parameters.Length < 1 || string.IsNullOrEmpty(request.Parameters[0]?.ToString()))
                        {
                            response.IsSuccess = false;
                            response.SetError(new ErrorMessage
                            {
                                Code = HttpStatusCode.BadRequest,
                                Description = "Invalid request data."
                            });
                            break;
                        }
                        var model = JsonSerializer.Deserialize<CreateUserDto>(parameters[0].ToString(), new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        });

                        if (model == null)
                        {
                            response.IsSuccess = false;
                            response.SetError(new ErrorMessage
                            {
                                Code = HttpStatusCode.BadRequest,
                                Description = "Invalid CreateUserDto format."
                            });
                            break;
                        }
                        var result = await _userService.CreateUserAsync(model);
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

            case "removerolefromuserasync":
                {
                    try
                    {
                        if (!canDelete)
                        {
                            response.IsSuccess = false;
                            response.SetError(new ErrorMessage
                            {
                                Code = HttpStatusCode.Forbidden,
                                Description = "You do not have permission to delete users."
                            });
                            break;
                        }
                        if (parameters.Length < 2 || string.IsNullOrEmpty(parameters[1]?.ToString()) || string.IsNullOrEmpty(parameters[1]?.ToString()))
                        {
                            response.IsSuccess = false;
                            response.SetError(new ErrorMessage
                            {
                                Code = HttpStatusCode.BadRequest,
                                Description = "Invalid request parameters. Expected user id and role id."
                            });
                            break;
                        }
                        var userId = parameters[0].ToString();
                        var roleId = parameters[1].ToString();
                        var result = await _userService.RemoveRoleFromUserAsync(userId, roleId);
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

            case "getusertypedropdown":
                {
                    response.IsSuccess = true;
                    response.Data = _userService.GetUserTypeDropdown();
                    break;
                }

            case "getallproductpagenation":
                {
                    break;
                }
            case "setroletouserasync":
                {
                    try
                    {
                        if (!canEdit)
                        {
                            response.IsSuccess = false;
                            response.SetError(new ErrorMessage
                            {
                                Code = HttpStatusCode.Forbidden,
                                Description = "You do not have permission to edit users."
                            });
                            break;
                        }
                        if (parameters.Length < 2 || string.IsNullOrEmpty(parameters[1]?.ToString()) || string.IsNullOrEmpty(parameters[1]?.ToString()))
                        {
                            response.IsSuccess = false;
                            response.SetError(new ErrorMessage
                            {
                                Code = HttpStatusCode.BadRequest,
                                Description = "Invalid request parameters. Expected user id and role id."
                            });
                            break;
                        }
                        var userId = parameters[0].ToString();
                        var roleId = parameters[1].ToString();
                        var result = await _userService.SetRoleToUserAsync(userId, roleId);
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

            case "deleteuserasync":
                {
                    if (!canDelete)
                    {
                        response.IsSuccess = false;
                        response.SetError(new ErrorMessage
                        {
                            Code = HttpStatusCode.Forbidden,
                            Description = "You do not have permission to delete users."
                        });
                    }
                    if (parameters.Length < 1 || string.IsNullOrEmpty(parameters[0]?.ToString()))
                    {
                        response.IsSuccess = false;
                        response.SetError(new ErrorMessage
                        {
                            Code = HttpStatusCode.BadRequest,
                            Description = "Invalid request data."
                        });
                        break;
                    }
                    var userId = parameters[0].ToString();
                    var result = await _userService.DeleteUserAsync(userId);
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

            case "updateuserasync":
                {
                    try
                    {
                        if (!canEdit)
                        {
                            response.IsSuccess = false;
                            response.SetError(new ErrorMessage
                            {
                                Code = HttpStatusCode.Forbidden,
                                Description = "You do not have permission to edit users."
                            });
                        }
                        if (parameters.Length < 1 || string.IsNullOrEmpty(parameters[0]?.ToString()))
                        {
                            response.IsSuccess = false;
                            response.SetError(new ErrorMessage
                            {
                                Code = HttpStatusCode.BadRequest,
                                Description = "Invalid request data."
                            });
                            break;
                        }
                        var model = JsonSerializer.Deserialize<UpdateUserDto>(parameters[0].ToString(), new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        });
                        if (model == null)
                        {
                            response.IsSuccess = false;
                            response.SetError(new ErrorMessage
                            {
                                Code = HttpStatusCode.BadRequest,
                                Description = "Invalid UpdataUserDto format."
                            });
                            break;
                        }
                        var result = await _userService.UpdateUserAsync(model);
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

            case "getuserbyidasync":
                {
                    try
                    {
                        if (!canView)
                        {
                            response.IsSuccess = false;
                            response.SetError(new ErrorMessage
                            {
                                Code = HttpStatusCode.Forbidden,
                                Description = "You do not have permission to view users."
                            });
                            break;
                        }
                        if (parameters.Length < 1 || string.IsNullOrEmpty(parameters[0]?.ToString()))
                        {
                            response.IsSuccess = false;
                            response.SetError(new ErrorMessage
                            {
                                Code = HttpStatusCode.BadRequest,
                                Description = "Invalid request data."
                            });
                            break;
                        }
                        var userId = parameters[0].ToString();
                        var result = await _userService.GetUserByIdAsync(Guid.Parse(userId));
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
