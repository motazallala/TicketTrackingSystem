using Microsoft.AspNetCore.Mvc;
using System.Net;
using TicketTrackingSystem.Application.HttpResponse;
using TicketTrackingSystem.Application.Interface;
using TicketTrackingSystem.Common.Model;

namespace TicketTrackingSystem.MVC.Controllers;

[Route("tickethistory")]
public class TicketHistoryController : Controller
{
    private readonly IPermissionService _permissionService;
    private readonly IUserService _userService;
    private readonly ITicketHistoryService _ticketHistoryService;

    public TicketHistoryController(
        IUserService userService,
        IPermissionService permissionService,
        ITicketHistoryService ticketHistoryService)
    {
        _userService = userService;
        _permissionService = permissionService;
        _ticketHistoryService = ticketHistoryService;
    }
    [Route("TicketReport")]
    public async Task<IActionResult> TicketReport()
    {
        var permissions = await CheckPermissionsAsync(PermissionName.ViewTicketHistory.ToString());
        if (!permissions[PermissionName.ViewTicketHistory.ToString()])
        {
            return Forbid();
        }
        return View();
    }

    [HttpPost("getalltickethistoryforreportasync")]
    public async Task<IActionResult> GetAllTicketHistoryForReportAsync(
        [FromBody] TicketHistoryReportRequest request)
    {
        var response = new BaseResponse();

        if (!ModelState.IsValid)
        {
            response.IsSuccess = false;
            response.SetErrorFromModelState(ModelState);
            return Ok(response);
        }

        var permissions = await CheckPermissionsAsync(PermissionName.ViewTicketHistory.ToString());
        if (!permissions[PermissionName.ViewTicketHistory.ToString()])
        {
            response.IsSuccess = false;
            response.SetError(new ErrorMessage
            {
                Code = HttpStatusCode.Forbidden,
                Description = "You do not have permission to view ticket history."
            });
            return Ok(response);
        }

        try
        {
            var result = await _ticketHistoryService.GetAllTicketHistoryForReportAsync(
                request.DataTablesRequest,
                request.StageFilter,
                request.DeliveryStatusFilter);

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
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.SetError(new ErrorMessage
            {
                Code = HttpStatusCode.InternalServerError,
                Description = ex.Message
            });
            return Ok(response);
        }
    }

    [HttpGet("deliverystatusdropdown")]
    public IActionResult GetDeliveryStatusDropdown()
    {
        var response = new BaseResponse
        {
            IsSuccess = true,
            Data = _ticketHistoryService.GetDeliveryStatusDropdown()
        };
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

// DTO for ticket history report request
public class TicketHistoryReportRequest
{
    public DataTablesRequest DataTablesRequest { get; set; }
    public string? StageFilter { get; set; }
    public string? DeliveryStatusFilter { get; set; }
}