using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;
using TicketTrackingSystem.Application.Dto;
using TicketTrackingSystem.Application.HttpResponse;
using TicketTrackingSystem.Application.Interface;
using TicketTrackingSystem.Common.Model;

namespace TicketTrackingSystem.MVC.Controllers;
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

    [HttpPost("/ticket/call")]
    public async Task<IActionResult> CallService([FromBody] DynamicRequest request)
    {
        // Check necessary permissions
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

            case "getallticketpaginatedasync":
                {
                    try
                    {
                        if (!canView)
                        {
                            response.IsSuccess = false;
                            response.SetError(new ErrorMessage
                            {
                                Code = HttpStatusCode.Forbidden,
                                Description = "You do not have permission to view tickets"
                            });
                            break;
                        }

                        if (parameters.Length < 2 || string.IsNullOrEmpty(parameters[0]?.ToString()) || string.IsNullOrEmpty(parameters[1]?.ToString()))
                        {
                            response.IsSuccess = false;
                            response.SetError(new ErrorMessage
                            {
                                Code = HttpStatusCode.BadRequest,
                                Description = "Invalid parameters"
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
                                Description = "Invalid parameters"
                            });
                            break;
                        }

                        var projectId = Guid.Parse(parameters[1].ToString());
                        var user = await _userService.GetUserByClaim(User);

                        var result = await _ticketService.GetAllTicketForUserPaginatedAsync(model, projectId, user.Id);

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

            case "assigntickettouserasync":
                {
                    if (!canEdit)
                    {
                        response.IsSuccess = false;
                        response.SetError(new ErrorMessage
                        {
                            Code = HttpStatusCode.Forbidden,
                            Description = "You do not have permission to edit tickets"
                        });
                        break;
                    }
                    if (parameters.Length < 1 || string.IsNullOrEmpty(parameters[0]?.ToString()))
                    {
                        response.IsSuccess = false;
                        response.SetError(new ErrorMessage
                        {
                            Code = HttpStatusCode.BadRequest,
                            Description = "Invalid parameters"
                        });
                        break;
                    }
                    var ticketId = Guid.Parse(parameters[0].ToString());
                    var user = await _userService.GetUserByClaim(User);
                    var result = await _ticketService.AssignTicketToUserAsync(ticketId, user.Id);
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

            case "getallticketformemberpaginatedasync":
                {
                    if (!canView)
                    {
                        response.IsSuccess = false;
                        response.SetError(new ErrorMessage
                        {
                            Code = HttpStatusCode.Forbidden,
                            Description = "You do not have permission to view tickets"
                        });
                        break;
                    }

                    if (parameters.Length < 3 || string.IsNullOrEmpty(parameters[0]?.ToString()) || string.IsNullOrEmpty(parameters[1]?.ToString()) || !bool.TryParse(parameters[2]?.ToString(), out bool reserved))
                    {
                        response.IsSuccess = false;
                        response.SetError(new ErrorMessage
                        {
                            Code = HttpStatusCode.BadRequest,
                            Description = "Invalid parameters"
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
                            Description = "Invalid parameters"
                        });
                        break;
                    }

                    var projectId = Guid.Parse(parameters[1].ToString());
                    var user = await _userService.GetUserByClaim(User);

                    var result = await _ticketService.GetAllTicketForMemberPaginatedAsync(model, projectId, user.Id, reserved);

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

            case "getticketbyidasync":
                {
                    try
                    {
                        if (!canView)
                        {
                            response.IsSuccess = false;
                            response.SetError(new ErrorMessage
                            {
                                Code = HttpStatusCode.Forbidden,
                                Description = "You do not have permission to view tickets"
                            });
                            break;
                        }

                        if (parameters.Length < 1 || string.IsNullOrEmpty(parameters[0]?.ToString()))
                        {
                            response.IsSuccess = false;
                            response.SetError(new ErrorMessage
                            {
                                Code = HttpStatusCode.BadRequest,
                                Description = "Invalid parameters"
                            });
                            break;
                        }

                        var ticketId = Guid.Parse(parameters[0].ToString());
                        var result = await _ticketService.GetTicketByIdAsync(ticketId);

                        if (!result.IsSuccess)
                        {
                            response.IsSuccess = false;
                            response.SetError(new ErrorMessage
                            {
                                Code = HttpStatusCode.NotFound,
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

            case "updateticketwithautostageasync":
                {
                    try
                    {
                        if (!canEdit)
                        {
                            response.IsSuccess = false;
                            response.SetError(new ErrorMessage
                            {
                                Code = HttpStatusCode.Forbidden,
                                Description = "You do not have permission to edit tickets"
                            });
                            break;
                        }
                        if (parameters.Length < 4 || string.IsNullOrEmpty(parameters[0]?.ToString()) || string.IsNullOrEmpty(parameters[1]?.ToString()) || !(bool.TryParse(parameters[2]?.ToString(), out bool isFinished)) || string.IsNullOrEmpty(parameters[3]?.ToString()))
                        {
                            response.IsSuccess = false;
                            response.SetError(new ErrorMessage
                            {
                                Code = HttpStatusCode.BadRequest,
                                Description = "Invalid parameters"
                            });
                            break;
                        }
                        var ticketId = Guid.Parse(parameters[0].ToString());
                        var status = parameters[1]?.ToString();
                        if (status == null || (!status.Equals("accept") && !status.Equals("reject") && !status.Equals("returned")))
                        {
                            response.IsSuccess = false;
                            response.SetError(new ErrorMessage
                            {
                                Code = HttpStatusCode.BadRequest,
                                Description = "The request does not have a accept or reject or returned. \n\n You at lest need send a accept or null."
                            });
                            break;
                        }
                        var message = parameters[3].ToString();
                        var user = await _userService.GetUserByClaim(User);
                        var result = await _ticketService.UpdateTicketWithAutoStageAsync(ticketId, status, isFinished, user.Id, message);
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

            case "addticketasync":
                {
                    try
                    {
                        if (!canAdd)
                        {
                            response.IsSuccess = false;
                            response.SetError(new ErrorMessage
                            {
                                Code = HttpStatusCode.Forbidden,
                                Description = "You do not have permission to create tickets"
                            });
                            break;
                        }

                        if (parameters.Length < 1 || string.IsNullOrEmpty(parameters[0]?.ToString()))
                        {
                            response.IsSuccess = false;
                            response.SetError(new ErrorMessage
                            {
                                Code = HttpStatusCode.BadRequest,
                                Description = "Invalid parameters"
                            });
                            break;
                        }

                        var ticketDto = JsonSerializer.Deserialize<CreateTicketDto>(parameters[0].ToString(), new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        });
                        var user = await _userService.GetUserByClaim(User);
                        ticketDto.CreatorId = user.Id;

                        var result = await _ticketService.AddTicketAsync(ticketDto);

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

            case "getticketstatusdropdown":
                {
                    response.IsSuccess = true;
                    response.Data = _ticketService.GetTicketStatusDropdown();
                    break;
                }
            default:
                {
                    response.IsSuccess = false;
                    response.SetError(new ErrorMessage
                    {
                        Code = HttpStatusCode.BadRequest,
                        Description = "Invalid method"
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
