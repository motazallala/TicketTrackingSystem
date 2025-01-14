using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;
using TicketTrackingSystem.Application.HttpResponse;
using TicketTrackingSystem.Application.Interface;
using TicketTrackingSystem.Common.Model;

namespace TicketTrackingSystem.MVC.Controllers;
public class TicketMessageController : Controller
{

    private readonly ITicketMessageService _ticketMessageService;
    private readonly IPermissionService _permissionService;
    private readonly IUserService _userService;
    public TicketMessageController(IPermissionService permissionService, IUserService userService, ITicketService ticketService, ITicketMessageService ticketMessageService)
    {
        _permissionService = permissionService;
        _userService = userService;
        _ticketMessageService = ticketMessageService;
    }

    [HttpPost("/ticketMessage/call")]
    public async Task<IActionResult> CallService([FromBody] DynamicRequest request)
    {
        var response = new BaseResponse();
        var parameters = request.Parameters;
        switch (request.Method.ToLower())
        {
            case "getallticketmessagespaginatedasync":
                {
                    if (parameters.Length < 2 || string.IsNullOrEmpty(parameters[0]?.ToString()) || string.IsNullOrEmpty(parameters[1]?.ToString()))
                    {
                        response.IsSuccess = false;
                        response.SetError(new ErrorMessage
                        {
                            Code = HttpStatusCode.BadRequest,
                            Description = "Invalid parameters."
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
                            Description = "Invalid parameters."
                        });
                        break;
                    }
                    var ticketId = Guid.Parse(parameters[1].ToString());
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
                    var result = await _ticketMessageService.GetAllTicketMessagesPaginatedAsync(model, ticketId, user.Id);
                    if (!result.IsSuccess)
                    {
                        response.IsSuccess = false;
                        response.SetError(new ErrorMessage
                        {
                            Code = HttpStatusCode.BadRequest,
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
