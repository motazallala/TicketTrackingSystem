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


    [HttpPost("/dashboard/call")]
    public async Task<IActionResult> CallService([FromBody] DynamicRequest request)
    {
        // Check necessary permissions
        var permissions = await CheckPermissionsAsync(
            PermissionName.ViewDepartment.ToString(),
            PermissionName.CreateDepartment.ToString(),
            PermissionName.EditDepartment.ToString(),
            PermissionName.DeleteDepartment.ToString()
        );

        var canView = permissions[PermissionName.ViewDepartment.ToString()];
        var canAdd = permissions[PermissionName.CreateDepartment.ToString()];
        var canEdit = permissions[PermissionName.EditDepartment.ToString()];
        var canDelete = permissions[PermissionName.DeleteDepartment.ToString()];

        var response = new BaseResponse();
        var parameters = request.Parameters;

        switch (request.Method.ToLower())
        {
            case "assigntickettouserasync":
                {
                    var user = await _userService.GetUserByClaim(User);
                    var result = await _boardService.GetTotalMemberTicketAsync(user.Id);
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
            case "gettotalclientticketasync":
                {
                    var user = await _userService.GetUserByClaim(User);
                    var result = await _boardService.GetTotalClientTicketAsync(user.Id);
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
            case "gettotalusersasync":
                {
                    try
                    {
                        var result = await _boardService.GetTotalUsersAsync();
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
                    catch (Exception)
                    {
                        response.IsSuccess = false;
                        response.SetError(new ErrorMessage
                        {
                            Code = HttpStatusCode.InternalServerError,
                            Description = "An error occurred getting the data!"
                        });
                        break;
                    }
                }
            case "gettotalticketsasync":
                {
                    try
                    {
                        var result = await _boardService.GetTotalTicketsAsync();
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
                    catch (Exception)
                    {
                        response.IsSuccess = false;
                        response.SetError(new ErrorMessage
                        {
                            Code = HttpStatusCode.InternalServerError,
                            Description = "An error occurred getting the data!"
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
