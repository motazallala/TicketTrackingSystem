using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Net;
using TicketTrackingSystem.Application.Dto;
using TicketTrackingSystem.Application.HttpResponse;
using TicketTrackingSystem.Application.Interface;
using TicketTrackingSystem.Common.Model;

namespace TicketTrackingSystem.MVC.Controllers;

[Route("ticket")]
public class TicketController : Controller
{
    private readonly IPermissionService _permissionService;
    private readonly IUserService _userService;
    private readonly ITicketService _ticketService;

    public TicketController(ITicketService ticketService, IUserService userService, IPermissionService permissionService)
    {
        _ticketService = ticketService;
        _userService = userService;
        _permissionService = permissionService;
    }


    public IActionResult Index()
    {
        return View();
    }

    [HttpPost("getallticketpaginatedasync")]
    public async Task<IActionResult> GetAllTicketPaginatedAsync([FromBody] GetAllTicketPaginatedRequest request)
    {
        var response = new BaseResponse();
        var permissions = await CheckPermissionsAsync(PermissionName.ViewTicket.ToString());
        if (!permissions[PermissionName.ViewTicket.ToString()])
        {
            response.IsSuccess = false;
            response.SetError(new ErrorMessage { Code = HttpStatusCode.Forbidden, Description = "You do not have permission to view tickets" });
            return Ok(response);
        }

        if (!ModelState.IsValid)
        {
            response.IsSuccess = false;
            response.SetErrorFromModelState(ModelState);
            return Ok(response);
        }

        try
        {
            var user = await _userService.GetUserByClaim(User);
            var result = await _ticketService.GetAllTicketForUserPaginatedAsync(request.DataTablesRequest, request.ProjectId, user.Id);
            return HandleServiceResult(response, result);
        }
        catch (Exception ex)
        {
            return HandleException(ex, response);
        }
    }

    [HttpPost("assigntickettouserasync")]
    public async Task<IActionResult> AssignTicketToUserAsync([FromBody] AssignTicketToUserRequest request)
    {
        var response = new BaseResponse();
        var permissions = await CheckPermissionsAsync(PermissionName.EditTicket.ToString());
        if (!permissions[PermissionName.EditTicket.ToString()])
        {
            response.IsSuccess = false;
            response.SetError(new ErrorMessage { Code = HttpStatusCode.Forbidden, Description = "You do not have permission to edit tickets" });
            return Ok(response);
        }

        if (!ModelState.IsValid)
        {
            response.IsSuccess = false;
            response.SetErrorFromModelState(ModelState);
            return Ok(response);
        }
        if (request.EstimationTime < DateTime.Now)
        {
            response.IsSuccess = false;
            response.SetError(new ErrorMessage { Code = HttpStatusCode.BadRequest, Description = "Estimation time must be a future date." });
            return Ok(response);
        }
        try
        {
            var user = await _userService.GetUserByClaim(User);
            var result = await _ticketService.AssignTicketToUserAsync(request.TicketId, user.Id, request.EstimationTime);
            return HandleServiceResult(response, result);
        }
        catch (Exception ex)
        {
            return HandleException(ex, response);
        }
    }

    [HttpPost("removeticketfromuserasync")]
    public async Task<IActionResult> RemoveTicketFromUserAsync([FromBody] RemoveTicketFromUserRequest request)
    {
        var response = new BaseResponse();
        var permissions = await CheckPermissionsAsync(PermissionName.EditTicket.ToString());
        if (!permissions[PermissionName.EditTicket.ToString()])
        {
            response.IsSuccess = false;
            response.SetError(new ErrorMessage { Code = HttpStatusCode.Forbidden, Description = "You do not have permission to edit tickets" });
            return Ok(response);
        }

        if (!ModelState.IsValid)
        {
            response.IsSuccess = false;
            response.SetErrorFromModelState(ModelState);
            return Ok(response);
        }

        try
        {
            var user = await _userService.GetUserByClaim(User);
            var result = await _ticketService.RemoveTicketFromUserAsync(request.TicketId, user.Id);
            return HandleServiceResult(response, result);
        }
        catch (Exception ex)
        {
            return HandleException(ex, response);
        }
    }

    [HttpPost("getallticketformemberpaginatedasync")]
    public async Task<IActionResult> GetAllTicketForMemberPaginatedAsync([FromBody] GetAllTicketForMemberPaginatedRequest request)
    {
        var response = new BaseResponse();
        var permissions = await CheckPermissionsAsync(PermissionName.ViewTicket.ToString());
        if (!permissions[PermissionName.ViewTicket.ToString()])
        {
            response.IsSuccess = false;
            response.SetError(new ErrorMessage { Code = HttpStatusCode.Forbidden, Description = "You do not have permission to view tickets" });
            return Ok(response);
        }

        if (!ModelState.IsValid)
        {
            response.IsSuccess = false;
            response.SetErrorFromModelState(ModelState);
            return Ok(response);
        }

        try
        {
            var user = await _userService.GetUserByClaim(User);
            var result = await _ticketService.GetAllTicketForMemberPaginatedAsync(request.DataTablesRequest, request.ProjectId, user.Id, request.Reserved);
            return HandleServiceResult(response, result);
        }
        catch (Exception ex)
        {
            return HandleException(ex, response);
        }
    }

    [HttpGet("getticketbyidasync/{ticketId}")]
    public async Task<IActionResult> GetTicketByIdAsync(Guid ticketId)
    {
        var response = new BaseResponse();
        var permissions = await CheckPermissionsAsync(PermissionName.ViewTicket.ToString());
        if (!permissions[PermissionName.ViewTicket.ToString()])
        {
            response.IsSuccess = false;
            response.SetError(new ErrorMessage { Code = HttpStatusCode.Forbidden, Description = "You do not have permission to view tickets" });
            return Ok(response);
        }

        try
        {
            var result = await _ticketService.GetTicketByIdAsync(ticketId);
            return HandleServiceResult(response, result);
        }
        catch (Exception ex)
        {
            return HandleException(ex, response);
        }
    }

    [HttpPost("updateticketwithautostageasync")]
    public async Task<IActionResult> UpdateTicketWithAutoStageAsync([FromBody] UpdateTicketWithAutoStageRequest request)
    {
        var response = new BaseResponse();
        var permissions = await CheckPermissionsAsync(PermissionName.EditTicket.ToString());
        if (!permissions[PermissionName.EditTicket.ToString()])
        {
            response.IsSuccess = false;
            response.SetError(new ErrorMessage { Code = HttpStatusCode.Forbidden, Description = "You do not have permission to edit tickets" });
            return Ok(response);
        }

        if (!ModelState.IsValid)
        {
            response.IsSuccess = false;
            response.SetErrorFromModelState(ModelState);
            return Ok(response);
        }

        if (request.Status != "accept" && request.Status != "reject" && request.Status != "returned")
        {
            response.IsSuccess = false;
            response.SetError(new ErrorMessage { Code = HttpStatusCode.BadRequest, Description = "Invalid status value" });
            return Ok(response);
        }

        try
        {
            var user = await _userService.GetUserByClaim(User);
            var result = await _ticketService.UpdateTicketWithAutoStageAsync(request.TicketId, request.Status, request.IsFinished, user.Id, request.Message);
            return HandleServiceResult(response, result);
        }
        catch (Exception ex)
        {
            return HandleException(ex, response);
        }
    }

    [HttpPost("addticketasync")]
    public async Task<IActionResult> AddTicketAsync([FromBody] CreateTicketDto ticketDto)
    {
        var response = new BaseResponse();
        var permissions = await CheckPermissionsAsync(PermissionName.CreateTicket.ToString());
        if (!permissions[PermissionName.CreateTicket.ToString()])
        {
            response.IsSuccess = false;
            response.SetError(new ErrorMessage { Code = HttpStatusCode.Forbidden, Description = "You do not have permission to create tickets" });
            return Ok(response);
        }

        if (!ModelState.IsValid)
        {
            response.IsSuccess = false;
            response.SetErrorFromModelState(ModelState);
            return Ok(response);
        }

        try
        {
            var user = await _userService.GetUserByClaim(User);
            ticketDto.CreatorId = user.Id;
            var result = await _ticketService.AddTicketAsync(ticketDto);
            return HandleServiceResult(response, result);
        }
        catch (Exception ex)
        {
            return HandleException(ex, response);
        }
    }

    [HttpGet("checkestimatedcompletiondateasync/{ticketId}")]
    public async Task<IActionResult> CheckEstimatedCompletionDateAsync(Guid ticketId)
    {
        var response = new BaseResponse();
        var permissions = await CheckPermissionsAsync(PermissionName.ViewTicket.ToString());
        if (!permissions[PermissionName.ViewTicket.ToString()])
        {
            response.IsSuccess = false;
            response.SetError(new ErrorMessage { Code = HttpStatusCode.Forbidden, Description = "You do not have permission to view tickets" });
            return Ok(response);
        }

        try
        {
            var result = await _ticketService.CheckEstimatedCompletionDateAsync(ticketId);
            return HandleServiceResult(response, result);
        }
        catch (Exception ex)
        {
            return HandleException(ex, response);
        }
    }

    [HttpPost("reassignticketasync")]
    public async Task<IActionResult> ReAssignTicketAsync([FromBody] ReAssignTicketRequest request)
    {
        var response = new BaseResponse();
        var permissions = await CheckPermissionsAsync(PermissionName.EditTicket.ToString());
        if (!permissions[PermissionName.EditTicket.ToString()])
        {
            response.IsSuccess = false;
            response.SetError(new ErrorMessage { Code = HttpStatusCode.Forbidden, Description = "You do not have permission to edit tickets" });
            return Ok(response);
        }

        if (!ModelState.IsValid)
        {
            response.IsSuccess = false;
            response.SetErrorFromModelState(ModelState);
            return Ok(response);
        }

        try
        {
            var result = await _ticketService.ReAssignTicketAsync(request.TicketId, request.UserId);
            return HandleServiceResult(response, result);
        }
        catch (Exception ex)
        {
            return HandleException(ex, response);
        }
    }

    [HttpPost("setestimatedcompletiondateforreassignticketasync")]
    public async Task<IActionResult> SetEstimatedCompletionDateForReassignTicketAsync([FromBody] SetEstimatedCompletionDateRequest request)
    {
        var response = new BaseResponse();
        var permissions = await CheckPermissionsAsync(PermissionName.EditTicket.ToString());
        if (!permissions[PermissionName.EditTicket.ToString()])
        {
            response.IsSuccess = false;
            response.SetError(new ErrorMessage { Code = HttpStatusCode.Forbidden, Description = "You do not have permission to edit tickets" });
            return Ok(response);
        }

        if (!ModelState.IsValid)
        {
            response.IsSuccess = false;
            response.SetErrorFromModelState(ModelState);
            return Ok(response);
        }
        if (request.EstimationTime < DateTime.Now)
        {
            response.IsSuccess = false;
            response.SetError(new ErrorMessage { Code = HttpStatusCode.BadRequest, Description = "Estimation time must be a future date." });
            return Ok(response);
        }
        try
        {
            var user = await _userService.GetUserByClaim(User);
            var result = await _ticketService.SetEstimatedCompletionDateForReassignTicketAsync(request.TicketId, user.Id, request.EstimationTime);
            return HandleServiceResult(response, result);
        }
        catch (Exception ex)
        {
            return HandleException(ex, response);
        }
    }

    [HttpGet("getallfreemembersdropdownasync")]
    public async Task<IActionResult> GetAllFreeMembersDropdownAsync([FromQuery] Guid projectId)
    {
        var response = new BaseResponse();
        var permissions = await CheckPermissionsAsync(PermissionName.ViewTicket.ToString());
        if (!permissions[PermissionName.ViewTicket.ToString()])
        {
            response.IsSuccess = false;
            response.SetError(new ErrorMessage { Code = HttpStatusCode.Forbidden, Description = "You do not have permission to view tickets" });
            return Ok(response);
        }

        try
        {
            var user = await _userService.GetUserByClaim(User);
            var result = await _ticketService.GetAllFreeMembersDropdownAsync(projectId, user.Id);
            response.IsSuccess = true;
            response.Data = result;
            return Ok(response);
        }
        catch (Exception ex)
        {
            return HandleException(ex, response);
        }
    }

    [HttpGet("getticketstatusdropdown")]
    public IActionResult GetTicketStatusDropdown()
    {
        var response = new BaseResponse
        {
            IsSuccess = true,
            Data = _ticketService.GetTicketStatusDropdown()
        };
        return Ok(response);
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

    private IActionResult HandleException(Exception ex, BaseResponse response)
    {
        response.IsSuccess = false;
        response.SetError(new ErrorMessage { Code = HttpStatusCode.InternalServerError, Description = ex.Message });
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

public class GetAllTicketPaginatedRequest
{
    public DataTablesRequest DataTablesRequest { get; set; }
    public Guid ProjectId { get; set; }
}

public class AssignTicketToUserRequest
{
    public Guid TicketId { get; set; }
    public DateTime EstimationTime { get; set; }
}

public class RemoveTicketFromUserRequest
{
    public Guid TicketId { get; set; }
}

public class GetAllTicketForMemberPaginatedRequest
{
    public DataTablesRequest DataTablesRequest { get; set; }
    public Guid ProjectId { get; set; }
    public bool Reserved { get; set; }
}

public class UpdateTicketWithAutoStageRequest
{
    public Guid TicketId { get; set; }
    public string Status { get; set; }
    public bool IsFinished { get; set; }
    [Required]
    [MaxLength(500, ErrorMessage = "The Message Is To Long!")]
    public string Message { get; set; }
}

public class ReAssignTicketRequest
{
    public Guid TicketId { get; set; }
    public Guid UserId { get; set; }
}

public class SetEstimatedCompletionDateRequest
{
    public Guid TicketId { get; set; }
    public DateTime EstimationTime { get; set; }
}