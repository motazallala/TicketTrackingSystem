using Microsoft.AspNetCore.Authorization;
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
    private readonly ITicketService _ticketService;
    private readonly IUserService _userService;
    public ProjectController(IProjectService projectService, IPermissionService permissionService, IUserService userService, ITicketService ticketService)
    {
        _projectService = projectService;
        _permissionService = permissionService;
        _userService = userService;
        _ticketService = ticketService;
    }
    public IActionResult Index()
    {
        return View();
    }
    public async Task<IActionResult> Details(Guid id)
    {
        var result = await _projectService.GetProjectByIdAsync(id);
        //check if the user in the project as a member
        if (!result.IsSuccess)
        {
            return NotFound();
        }
        return View(result.Value);
    }
    [Authorize]
    public async Task<IActionResult> Tickets(Guid id)
    {
        var result = await _projectService.GetProjectByIdAsync(id);
        if (!result.IsSuccess)
        {
            return NotFound();
        }
        var user = await _userService.GetUserByClaim(User);
        var stageName = await _userService.GetUserStageFromProjectMemberAsync(user.Id, id);
        if (!stageName.IsSuccess)
        {
            return NotFound();
        }
        ViewData["StageName"] = stageName.Value;
        return View(result.Value);
    }

    [HttpGet("project/ticket/{ticketId}/messages")]
    public async Task<IActionResult> Messages(Guid ticketId)
    {
        var result = await _ticketService.GetTicketByIdAsync(ticketId);
        // check if the ticket is have history with the logged in user
        if (!result.IsSuccess)
        {
            return NotFound();
        }
        return View(result.Value);
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


        var canView = permissions[PermissionName.ViewProject.ToString()];
        var canAdd = permissions[PermissionName.CreateProject.ToString()];
        var canEdit = permissions[PermissionName.EditProject.ToString()];
        var canDelete = permissions[PermissionName.DeleteProject.ToString()];

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
                            break;
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

            case "setuserforprojectasync":
                {
                    if (!canEdit)
                    {
                        response.IsSuccess = false;
                        response.SetError(new ErrorMessage
                        {
                            Code = HttpStatusCode.Forbidden,
                            Description = "You do not have permission to edit projects."
                        });
                        break;
                    }
                    if (parameters.Length < 3 || string.IsNullOrEmpty(parameters[0]?.ToString()) || string.IsNullOrEmpty(parameters[1]?.ToString()) || !(int.TryParse(parameters[2]?.ToString(), out int stage)))
                    {
                        response.IsSuccess = false;
                        response.SetError(new ErrorMessage
                        {
                            Code = HttpStatusCode.BadRequest,
                            Description = "Invalid request."
                        });
                        break;
                    }
                    var userId = Guid.Parse(parameters[0].ToString());
                    var projectId = Guid.Parse(parameters[1].ToString());
                    var result = await _projectService.SetUserForProjectAsync(userId, projectId, stage);
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

            case "removeuserfromprojectasync":
                {
                    if (!canEdit)
                    {
                        response.IsSuccess = false;
                        response.SetError(new ErrorMessage
                        {
                            Code = HttpStatusCode.Forbidden,
                            Description = "You do not have permission to edit projects."
                        });
                        break;
                    }
                    if (parameters.Length < 2 || string.IsNullOrEmpty(parameters[0]?.ToString()) || string.IsNullOrEmpty(parameters[1]?.ToString()))
                    {
                        response.IsSuccess = false;
                        response.SetError(new ErrorMessage
                        {
                            Code = HttpStatusCode.BadRequest,
                            Description = "Invalid request."
                        });
                        break;
                    }
                    var userId = Guid.Parse(parameters[0].ToString());
                    var projectId = Guid.Parse(parameters[1].ToString());
                    var result = await _projectService.RemoveUserFromProjectAsync(userId, projectId);
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
            case "getstagedropdown":
                {
                    response.IsSuccess = true;
                    response.Data = _projectService.GetStageDropdown();
                    break;
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

    [HttpPost("/projectforclient/call")]
    public async Task<IActionResult> CallProjectService([FromBody] DynamicRequest request)
    {
        // Check necessary permissions
        var permissions = await CheckPermissionsAsync(
            PermissionName.ViewProject.ToString(),
            PermissionName.CreateProject.ToString(),
            PermissionName.EditProject.ToString(),
            PermissionName.DeleteProject.ToString()
        );


        var canView = permissions[PermissionName.ViewProject.ToString()];
        var canAdd = permissions[PermissionName.CreateProject.ToString()];
        var canEdit = permissions[PermissionName.EditProject.ToString()];
        var canDelete = permissions[PermissionName.DeleteProject.ToString()];
        var response = new BaseResponse();
        var parameters = request.Parameters;
        switch (request.Method.ToLower())
        {
            case "getalluserprojectsasync":
                {
                    if (!canView)
                    {
                        response.IsSuccess = false;
                        response.SetError(new ErrorMessage
                        {
                            Code = HttpStatusCode.Forbidden,
                            Description = "You do not have permission to view projects."
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
                    if (!User.Identity.IsAuthenticated)
                    {
                        response.IsSuccess = false;
                        response.SetError(new ErrorMessage
                        {
                            Code = HttpStatusCode.Unauthorized,
                            Description = "You are not authenticated."
                        });
                        break;
                    }
                    var user = await _userService.GetUserByClaim(User);
                    if (user is null)
                    {
                        response.IsSuccess = false;
                        response.SetError(new ErrorMessage
                        {
                            Code = HttpStatusCode.Unauthorized,
                            Description = "You are not authenticated."
                        });
                        break;
                    }
                    var model = JsonSerializer.Deserialize<DataTablesRequest>(parameters[0].ToString(), new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true,
                    });
                    if (model is null)
                    {
                        response.IsSuccess = false;
                        response.SetError(new ErrorMessage
                        {
                            Code = HttpStatusCode.BadRequest,
                            Description = "Invalid request."
                        });

                        break;
                    }
                    var userId = user.Id;
                    var result = await _projectService.GetAllUserProjectsAsync(model, userId);
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
