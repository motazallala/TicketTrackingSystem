using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;
using TicketTrackingSystem.Application.HttpResponse;
using TicketTrackingSystem.Application.Interface;
using TicketTrackingSystem.Common.Model;

namespace TicketTrackingSystem.MVC.Controllers;
public class TicketHistoryController : Controller
{
    private readonly IPermissionService _permissionService;
    private readonly IUserService _userService;
    private readonly ITicketHistoryService _ticketHistoryService;
    public TicketHistoryController(IUserService userService, IPermissionService permissionService, ITicketHistoryService ticketHistoryService)
    {
        _userService = userService;
        _permissionService = permissionService;
        _ticketHistoryService = ticketHistoryService;
    }

    public async Task<IActionResult> TicketReport()
    {
        // Check necessary permissions
        var permissions = await CheckPermissionsAsync(
            PermissionName.ViewTicketHistory.ToString()
        );
        var canView = permissions[PermissionName.ViewTicketHistory.ToString()];
        //if (!canView)
        //{
        //    return Forbid();
        //}
        return View();
    }

    [HttpPost("/ticketHistory/call")]
    public async Task<IActionResult> CallService([FromBody] DynamicRequest request)
    {
        var permissions = await CheckPermissionsAsync(
            PermissionName.ViewTicket.ToString(),
            PermissionName.CreateTicket.ToString(),
            PermissionName.EditTicket.ToString(),
            PermissionName.DeleteTicket.ToString()
        );

        var canView = permissions[PermissionName.ViewTicket.ToString()];
        var canAdd = permissions[PermissionName.CreateTicket.ToString()];
        var canEdit = permissions[PermissionName.EditTicket.ToString()];
        var canDelete = permissions[PermissionName.DeleteTicket.ToString()];
        var response = new BaseResponse();
        var parameters = request.Parameters;
        switch (request.Method.ToLower())
        {
            case "getalltickethistoryforreportasync":
                {
                    if (parameters.Length < 3 || !(parameters[0] is JsonElement requestElement))
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
                        string stageFilter = parameters[1]?.ToString();
                        string deliveryStatusFilter = parameters[2]?.ToString();
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
                        var result = await _ticketHistoryService.GetAllTicketHistoryForReportAsync(dataTableRequest, stageFilter, deliveryStatusFilter);
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
            case "getdeliverystatusdropdown":
                {
                    response.IsSuccess = true;
                    response.Data = _ticketHistoryService.GetDeliveryStatusDropdown();
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
