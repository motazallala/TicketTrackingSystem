using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using TicketTrackingSystem.Application.Dto;
using TicketTrackingSystem.Application.HttpResponse;
using TicketTrackingSystem.Application.Interface;
using TicketTrackingSystem.Common.Model;

namespace TicketTrackingSystem.MVC.Controllers;
[Authorize]
[Route("project")]
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
    [Route("Details/{id}")]
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
    [Route("Tickets/{id}")]
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

    [HttpGet("ticket/{ticketId}/messages")]
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

    [HttpPost("getallprojectpaginatedasync")]
    public async Task<IActionResult> GetAllProjectPaginatedAsync(
            [FromBody] DataTablesRequest request)
    {
        var response = new BaseResponse();
        if (!ModelState.IsValid)
        {
            response.IsSuccess = false;
            response.SetErrorFromModelState(ModelState);
            return Ok(response);
        }
        var permissions = await CheckPermissionsAsync(PermissionName.ViewProject.ToString());

        if (!permissions[PermissionName.ViewProject.ToString()])
        {
            return ForbidResponse(response, "view projects");
        }

        try
        {
            var result = await _projectService.GetAllProjectPaginatedAsync(request);
            return HandleServiceResult(response, result);
        }
        catch (Exception ex)
        {
            return HandleException(response, ex);
        }
    }

    [HttpPost("getalluserprojectsasync")]
    public async Task<IActionResult> GetAllUserProjectsAsync(
    [FromBody] DataTablesRequest request)
    {
        var response = new BaseResponse();

        // Permission check
        var permissions = await CheckPermissionsAsync(PermissionName.ViewProject.ToString());
        if (!permissions[PermissionName.ViewProject.ToString()])
        {
            return ForbidResponse(response, "view projects");
        }



        // Get current user
        var user = await _userService.GetUserByClaim(User);
        if (user == null)
        {
            return UnauthorizedResponse(response);
        }

        try
        {
            var result = await _projectService.GetAllUserProjectsAsync(
                request,
                user.Id);

            return HandleServiceResult(response, result);
        }
        catch (Exception ex)
        {
            return HandleException(response, ex);
        }
    }


    [HttpPost("createprojectasync")]
    public async Task<IActionResult> CreateProjectAsync(
        [FromBody] CreateProjectDto request)
    {
        var response = new BaseResponse();
        if (!ModelState.IsValid)
        {
            response.IsSuccess = false;
            response.SetErrorFromModelStateAsDictionary(ModelState);
            return Ok(response);
        }
        var permissions = await CheckPermissionsAsync(PermissionName.CreateProject.ToString());

        if (!permissions[PermissionName.CreateProject.ToString()])
        {
            return ForbidResponse(response, "create projects");
        }

        try
        {
            var result = await _projectService.CreateProjectAsync(request);
            return HandleServiceResult(response, result);
        }
        catch (Exception ex)
        {
            return HandleException(response, ex);
        }
    }

    [HttpPost("updateprojectasync")]
    public async Task<IActionResult> UpdateProjectAsync(
        [FromBody] UpdateProjectDto request)
    {
        var response = new BaseResponse();
        if (!ModelState.IsValid)
        {
            response.IsSuccess = false;
            response.SetErrorFromModelStateAsDictionary(ModelState);
            return Ok(response);
        }
        var permissions = await CheckPermissionsAsync(PermissionName.EditProject.ToString());

        if (!permissions[PermissionName.EditProject.ToString()])
        {
            return ForbidResponse(response, "edit projects");
        }

        try
        {
            var result = await _projectService.UpdateProjectAsync(request);
            return HandleServiceResult(response, result);
        }
        catch (Exception ex)
        {
            return HandleException(response, ex);
        }
    }

    [HttpPost("deleteprojectasync")]
    public async Task<IActionResult> DeleteProjectAsync(
        [FromBody] DeleteProjectRequest request)
    {
        var response = new BaseResponse();
        if (!ModelState.IsValid)
        {
            response.IsSuccess = false;
            response.SetErrorFromModelState(ModelState);
            return Ok(response);
        }
        var permissions = await CheckPermissionsAsync(PermissionName.DeleteProject.ToString());

        if (!permissions[PermissionName.DeleteProject.ToString()])
        {
            return ForbidResponse(response, "delete projects");
        }

        try
        {
            var result = await _projectService.DeleteProjectAsync(request.ProjectId);
            return HandleServiceResult(response, result);
        }
        catch (Exception ex)
        {
            return HandleException(response, ex);
        }
    }
    [HttpPost("deleteprojectcascadeasync")]
    public async Task<IActionResult> DeleteProjectCascadeAsync(
    [FromBody] DeleteProjectRequest request)
    {
        var response = new BaseResponse();
        if (!ModelState.IsValid)
        {
            response.IsSuccess = false;
            response.SetErrorFromModelState(ModelState);
            return Ok(response);
        }
        var permissions = await CheckPermissionsAsync(PermissionName.DeleteProject.ToString());

        if (!permissions[PermissionName.DeleteProject.ToString()])
        {
            return ForbidResponse(response, "delete projects");
        }

        try
        {
            var result = await _projectService.DeleteProjectCascadeAsync(request.ProjectId);
            return HandleServiceResult(response, result);
        }
        catch (Exception ex)
        {
            return HandleException(response, ex);
        }
    }
    [HttpPost("setuserforprojectasync")]
    public async Task<IActionResult> SetUserForProjectAsync(
        [FromBody] SetUserForProjectRequest request)
    {
        var response = new BaseResponse();
        if (!ModelState.IsValid)
        {
            response.IsSuccess = false;
            response.SetErrorFromModelState(ModelState);
            return Ok(response);
        }
        var permissions = await CheckPermissionsAsync(PermissionName.EditProject.ToString());

        if (!permissions[PermissionName.EditProject.ToString()])
        {
            return ForbidResponse(response, "edit projects");
        }

        try
        {
            var result = await _projectService.SetUserForProjectAsync(
                request.UserId,
                request.ProjectId,
                request.Stage);
            return HandleServiceResult(response, result);
        }
        catch (Exception ex)
        {
            return HandleException(response, ex);
        }
    }

    [HttpPost("removeuserfromprojectasync")]
    public async Task<IActionResult> RemoveUserFromProjectAsync(
        [FromBody] RemoveUserFromProjectRequest request)
    {
        var response = new BaseResponse();
        if (!ModelState.IsValid)
        {
            response.IsSuccess = false;
            response.SetErrorFromModelState(ModelState);
            return Ok(response);
        }
        var permissions = await CheckPermissionsAsync(PermissionName.EditProject.ToString());

        if (!permissions[PermissionName.EditProject.ToString()])
        {
            return ForbidResponse(response, "edit projects");
        }

        try
        {
            var result = await _projectService.RemoveUserFromProjectAsync(
                request.UserId,
                request.ProjectId);
            return HandleServiceResult(response, result);
        }
        catch (Exception ex)
        {
            return HandleException(response, ex);
        }
    }

    [HttpGet("getstagedropdown")]
    public IActionResult GetStageDropdown()
    {
        var response = new BaseResponse
        {
            IsSuccess = true,
            Data = _projectService.GetStageDropdown()
        };
        return Ok(response);
    }

    // Common handler methods
    private IActionResult UnauthorizedResponse(BaseResponse response)
    {
        response.IsSuccess = false;
        response.SetError(new ErrorMessage
        {
            Code = HttpStatusCode.Unauthorized,
            Description = "You are not authenticated."
        });
        return Ok(response);
    }
    private IActionResult HandleServiceResult(BaseResponse response, dynamic result)
    {
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

    private IActionResult HandleException(BaseResponse response, Exception ex)
    {
        response.IsSuccess = false;
        response.SetError(new ErrorMessage
        {
            Code = HttpStatusCode.InternalServerError,
            Description = ex.Message
        });
        return Ok(response);
    }

    private IActionResult ForbidResponse(BaseResponse response, string action)
    {
        response.IsSuccess = false;
        response.SetError(new ErrorMessage
        {
            Code = HttpStatusCode.Forbidden,
            Description = $"You do not have permission to {action}."
        });
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


