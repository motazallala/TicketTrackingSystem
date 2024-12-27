using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;
using TicketTrackingSystem.Application.Dto;
using TicketTrackingSystem.Application.HttpResponse;
using TicketTrackingSystem.Application.Interface;
using TicketTrackingSystem.Common.Model;

namespace TicketTrackingSystem.MVC.Controllers;
public class ProjectController : Controller
{
    private readonly IProjectService _projectService;
    private readonly IPermissionService _permissionService;
    private readonly IUserService _userService;
    public ProjectController(IProjectService projectService, IPermissionService permissionService, IUserService userService)
    {
        _projectService = projectService;
        _permissionService = permissionService;
        _userService = userService;
    }
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost("/project/call")]
    public async Task<IActionResult> CallService([FromBody] DynamicRequest request)
    {
        // Check necessary permissions
        var permissions = await CheckPermissionsAsync(
            PermissionName.ViewProject.ToString(),
            PermissionName.CreateProject.ToString(),
            PermissionName.EditProject.ToString(),
            PermissionName.DeleteProject.ToString()
        );


        var canView = !permissions[PermissionName.ViewProject.ToString()];
        var canAdd = !permissions[PermissionName.CreateProject.ToString()];
        var canEdit = !permissions[PermissionName.EditProject.ToString()];
        var canDelete = !permissions[PermissionName.DeleteProject.ToString()];

        var response = new BaseResponse();
        var parameters = request.Parameters;
        switch (request.Method.ToLower())
        {
            case "getallprojectpaginatedasync":
                {
                    try
                    {
                        if (!canView)
                        {
                            response.IsSuccess = false;
                            response.SetError(new ErrorMessage
                            {
                                Code = HttpStatusCode.Forbidden,
                                Description = "You do not have permission to view projects."
                            });
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
                        var requestModel = JsonSerializer.Deserialize<DataTablesRequest>(requestElement.GetRawText(), new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        });
                        var result = await _projectService.GetAllProjectPaginatedAsync(requestModel);
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

            case "createprojectasync":
                {
                    try
                    {
                        if (!canAdd)
                        {
                            response.IsSuccess = false;
                            response.SetError(new ErrorMessage
                            {
                                Code = HttpStatusCode.Forbidden,
                                Description = "You do not have permission to add projects."
                            });
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
                        var requestModel = JsonSerializer.Deserialize<CreateProjectDto>(parameters[0].ToString(), new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        });
                        if (requestModel is null || string.IsNullOrEmpty(requestModel.Name) || string.IsNullOrEmpty(requestModel.Description))
                        {
                            response.IsSuccess = false;
                            response.SetError(new ErrorMessage
                            {
                                Code = HttpStatusCode.BadRequest,
                                Description = "Invalid request."
                            });
                            break;
                        }
                        var result = await _projectService.CreateProjectAsync(requestModel);
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

            case "updateprojectasync":
                {
                    try
                    {
                        if (!canEdit)
                        {
                            response.IsSuccess = false;
                            response.SetError(new ErrorMessage
                            {
                                Code = HttpStatusCode.Forbidden,
                                Description = "You do not have permission to edit projects."
                            });
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
                        var requestModel = JsonSerializer.Deserialize<UpdateProjectDto>(parameters[0].ToString(), new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        });
                        if (requestModel is null || requestModel.Id == Guid.Empty || string.IsNullOrEmpty(requestModel.Name) || string.IsNullOrEmpty(requestModel.Description))
                        {
                            response.IsSuccess = false;
                            response.SetError(new ErrorMessage
                            {
                                Code = HttpStatusCode.BadRequest,
                                Description = "Invalid request."
                            });
                            break;
                        }
                        var result = await _projectService.UpdateProjectAsync(requestModel);
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

            case "deleteprojectasync":
                {
                    try
                    {
                        if (!canDelete)
                        {
                            response.IsSuccess = false;
                            response.SetError(new ErrorMessage
                            {
                                Code = HttpStatusCode.Forbidden,
                                Description = "You do not have permission to delete projects."
                            });
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
                        var projectId = Guid.Parse(parameters[0].ToString());
                        var result = await _projectService.DeleteProjectAsync(projectId);
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
                        Code = HttpStatusCode.MethodNotAllowed,
                        Description = "Invalid method."
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
