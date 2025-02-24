using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net;
using TicketTrackingSystem.Application.HttpResponse;
using TicketTrackingSystem.Application.Interface;
using TicketTrackingSystem.Common.Model;
using TicketTrackingSystem.MVC.Models;

namespace TicketTrackingSystem.MVC.Controllers;
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IPermissionService _permissionService;
    private readonly IUserService _userService;
    private readonly IDashboardService _boardService;

    public HomeController(ILogger<HomeController> logger, IPermissionService permissionService, IUserService userService, IDashboardService boardService)
    {
        _logger = logger;
        _permissionService = permissionService;
        _userService = userService;
        _boardService = boardService;
    }
    public IActionResult Index()
    {
        return View();
    }
    public IActionResult Privacy()
    {
        return View();
    }
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }


    [HttpGet("home/gettotalmemberticketasync")]
    public async Task<IActionResult> GetTotalMemberTicketAsync()
    {
        return await HandleDashboardOperation(PermissionName.EditTicket, async () =>
        {
            var user = await _userService.GetUserByClaim(User);
            return await _boardService.GetTotalMemberTicketAsync(user.Id);
        });
    }

    [HttpGet("home/gettotalclientticketasync")]
    public async Task<IActionResult> GetTotalClientTicketAsync()
    {
        return await HandleDashboardOperation(null, async () =>
        {
            var user = await _userService.GetUserByClaim(User);
            return await _boardService.GetTotalClientTicketAsync(user.Id);
        });
    }

    [HttpGet("home/gettotalusersasync")]
    public async Task<IActionResult> GetTotalUsersAsync()
    {
        return await HandleDashboardOperation(PermissionName.ViewDepartment, async () =>
            await _boardService.GetTotalUsersAsync());
    }

    [HttpGet("home/gettotalticketsasync")]
    public async Task<IActionResult> GetTotalTicketsAsync()
    {
        return await HandleDashboardOperation(PermissionName.ViewDepartment, async () =>
            await _boardService.GetTotalTicketsAsync());
    }

    #region Common Handlers
    private async Task<IActionResult> HandleDashboardOperation(
        PermissionName? requiredPermission,
        Func<Task<dynamic>> operation)
    {
        var response = new BaseResponse();
        var permissionName = requiredPermission.ToString();

        if (!string.IsNullOrEmpty(permissionName))
        {
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
            return HandleException(ex, response);
        }
    }
    private IActionResult HandleException(Exception ex, BaseResponse response)
    {
        response.IsSuccess = false;
        response.SetError(new ErrorMessage { Code = HttpStatusCode.InternalServerError, Description = ex.Message });
        return Ok(response);
    }
    private async Task<Guid> GetCurrentUserId()
    {
        var user = await _userService.GetUserByClaim(User);
        return user?.Id ?? Guid.Empty;
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
    #endregion

}
