using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using TicketTrackingSystem.Application.HttpResponse;
using TicketTrackingSystem.Application.Interface;
using TicketTrackingSystem.Common.Model;

namespace TicketTrackingSystem.MVC.Controllers;
[Authorize]
[Route("ticketmessage")]
public class TicketMessageController : Controller
{
    private readonly ITicketMessageService _ticketMessageService;
    private readonly IPermissionService _permissionService;
    private readonly IUserService _userService;

    public TicketMessageController(
        IPermissionService permissionService,
        IUserService userService,
        ITicketMessageService ticketMessageService)
    {
        _permissionService = permissionService;
        _userService = userService;
        _ticketMessageService = ticketMessageService;
    }

    [HttpPost("getallticketmessagespaginatedasync")]
    public async Task<IActionResult> GetAllTicketMessagesPaginatedAsync(
        [FromBody] TicketMessagePaginatedRequest request)
    {
        var response = new BaseResponse();

        if (!ModelState.IsValid)
        {
            response.IsSuccess = false;
            response.SetErrorFromModelState(ModelState);
            return Ok(response);
        }

        var permissions = await CheckPermissionsAsync(PermissionName.ViewTicket.ToString());
        if (!permissions[PermissionName.ViewTicket.ToString()])
        {
            return ForbidResponse(response, "view tickets");
        }

        try
        {
            var user = await _userService.GetUserByClaim(User);
            var result = await _ticketMessageService.GetAllTicketMessagesPaginatedAsync(
                request.DataTablesRequest,
                request.TicketId,
                user.Id);

            return HandleServiceResult(response, result);
        }
        catch (Exception ex)
        {
            return HandleException(response, ex);
        }
    }

    [HttpPost("getallnotseenmessageforticketasync")]
    public async Task<IActionResult> GetAllNotSeenMessageForTicketAsync(
        [FromBody] TicketMessageNotSeenRequest request)
    {
        var response = new BaseResponse();

        if (!ModelState.IsValid)
        {
            response.IsSuccess = false;
            response.SetErrorFromModelState(ModelState);
            return Ok(response);
        }

        var permissions = await CheckPermissionsAsync(PermissionName.ViewTicket.ToString());
        if (!permissions[PermissionName.ViewTicket.ToString()])
        {
            return ForbidResponse(response, "view tickets");
        }

        try
        {
            var user = await _userService.GetUserByClaim(User);
            var result = await _ticketMessageService.GetAllNotSeenMessageForTicketAsync(
                request.TicketId,
                user.Id);

            return HandleServiceResult(response, result);
        }
        catch (Exception ex)
        {
            return HandleException(response, ex);
        }
    }

    [HttpPost("makemessageseenasync")]
    public async Task<IActionResult> MakeMessageSeenAsync(
        [FromBody] MakeMessageSeenRequest request)
    {
        var response = new BaseResponse();

        if (!ModelState.IsValid)
        {
            response.IsSuccess = false;
            response.SetErrorFromModelState(ModelState);
            return Ok(response);
        }

        var permissions = await CheckPermissionsAsync(PermissionName.ViewTicket.ToString());
        if (!permissions[PermissionName.ViewTicket.ToString()])
        {
            return ForbidResponse(response, "View Ticket");
        }

        try
        {
            var user = await _userService.GetUserByClaim(User);
            var result = await _ticketMessageService.MakeMessageSeenAsync(
                request.MessageId,
                user.Id);

            return HandleServiceResult(response, result);
        }
        catch (Exception ex)
        {
            return HandleException(response, ex);
        }
    }

    private IActionResult HandleServiceResult(BaseResponse response, dynamic result)
    {
        if (!result.IsSuccess)
        {
            response.IsSuccess = false;
            response.SetError(new ErrorMessage
            {
                Code = HttpStatusCode.BadRequest,
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

    private IActionResult ForbidResponse(BaseResponse response, string permission)
    {
        response.IsSuccess = false;
        response.SetError(new ErrorMessage
        {
            Code = HttpStatusCode.Forbidden,
            Description = $"You do not have permission to {permission}."
        });
        return Ok(response);
    }

    private async Task<Dictionary<string, bool>> CheckPermissionsAsync(params string[] permissionNames)
    {
        if (User.Identity.IsAuthenticated)
        {
            var user = await _userService.GetUserByClaim(User);
            return await _permissionService.HasPermissionAsync(user.Id, permissionNames);
        }
        return permissionNames.ToDictionary(p => p, p => false);
    }
}

// DTOs
public class TicketMessagePaginatedRequest
{
    public DataTablesRequest DataTablesRequest { get; set; }
    public Guid TicketId { get; set; }
}

public class TicketMessageNotSeenRequest
{
    public Guid TicketId { get; set; }
}

public class MakeMessageSeenRequest
{
    public Guid MessageId { get; set; }
}