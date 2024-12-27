using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;
using TicketTrackingSystem.Application.Dto;
using TicketTrackingSystem.Application.HttpResponse;
using TicketTrackingSystem.Application.Interface;
using TicketTrackingSystem.Common.Model;

namespace TicketTrackingSystem.MVC.Controllers;
public class DepartmentController : Controller
{
    private readonly IPermissionService _permissionService;
    private readonly IUserService _userService;
    private readonly IDepartmentService _departmentService;

    public DepartmentController(IPermissionService permissionService, IUserService userService, IDepartmentService departmentService)
    {
        _permissionService = permissionService;
        _userService = userService;
        _departmentService = departmentService;
    }
    public IActionResult Index()
    {
        return View();
    }


    [HttpPost("/department/call")]
    public async Task<IActionResult> CallService([FromBody] DynamicRequest request)
    {
        // Check necessary permissions
        var permissions = await CheckPermissionsAsync(
            PermissionName.ViewDepartment.ToString(),
            PermissionName.CreateDepartment.ToString(),
            PermissionName.EditDepartment.ToString(),
            PermissionName.DeleteDepartment.ToString()
        );

        var canView = !permissions[PermissionName.ViewDepartment.ToString()];
        var canAdd = !permissions[PermissionName.CreateDepartment.ToString()];
        var canEdit = !permissions[PermissionName.EditDepartment.ToString()];
        var canDelete = !permissions[PermissionName.DeleteDepartment.ToString()];

        var response = new BaseResponse();
        var parameters = request.Parameters;

        switch (request.Method.ToLower())
        {
            case "getalldepartmentspaginatedasync":
                {
                    if (!canView)
                    {
                        response.IsSuccess = false;
                        response.SetError(new ErrorMessage
                        {
                            Code = HttpStatusCode.Forbidden,
                            Description = "You do not have permission to view departments."
                        });
                        break;
                    }
                    if (parameters.Length < 1 || !(parameters[0] is JsonElement requestElement))
                    {
                        response.IsSuccess = false;
                        response.SetError(new ErrorMessage
                        {
                            Code = HttpStatusCode.BadRequest,
                            Description = "Invalid request parameters."
                        });
                        break;
                    }
                    var dataTablesRequest = JsonSerializer.Deserialize<DataTablesRequest>(requestElement.GetRawText(), new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
                    if (dataTablesRequest == null)
                    {
                        response.IsSuccess = false;
                        response.SetError(new ErrorMessage
                        {
                            Code = HttpStatusCode.BadRequest,
                            Description = "Invalid pagination request data."
                        });
                        break;
                    }
                    var paginationResult = await _departmentService.GetAllDepartmentsPaginatedAsync(dataTablesRequest);
                    if (!paginationResult.IsSuccess)
                    {
                        response.IsSuccess = false;
                        response.SetError(new ErrorMessage
                        {
                            Code = HttpStatusCode.InternalServerError,
                            Description = paginationResult.ErrorMessage
                        });
                        break;
                    }
                    response.IsSuccess = true;
                    response.Data = paginationResult.Value;
                    break;
                }
            case "createdepartmentasync":
                {
                    if (!canAdd)
                    {
                        response.IsSuccess = false;
                        response.SetError(new ErrorMessage
                        {
                            Code = HttpStatusCode.Forbidden,
                            Description = "You do not have permission to create departments."
                        });
                        break;
                    }
                    if (request.Parameters.Length < 1 || string.IsNullOrEmpty(request.Parameters[0]?.ToString()))
                    {
                        response.IsSuccess = false;
                        response.SetError(new ErrorMessage
                        {
                            Code = HttpStatusCode.BadRequest,
                            Description = "Invalid request parameters."
                        });
                        break;
                    }

                    try
                    {
                        var department = JsonSerializer.Deserialize<CreateDepartmentDto>(request.Parameters[0].ToString(), new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        });
                        if (department is null || string.IsNullOrEmpty(department.Name) || string.IsNullOrEmpty(department.Description))
                        {
                            response.IsSuccess = false;
                            response.SetError(new ErrorMessage
                            {
                                Code = HttpStatusCode.BadRequest,
                                Description = "Invalid department data."
                            });
                            break;
                        }
                        var createResult = await _departmentService.CreateDepartmentAsync(department);
                        if (!createResult.IsSuccess)
                        {
                            response.IsSuccess = false;
                            response.SetError(new ErrorMessage
                            {
                                Code = HttpStatusCode.InternalServerError,
                                Description = createResult.ErrorMessage
                            });
                            break;
                        }
                        response.IsSuccess = true;
                        response.Data = createResult.Value;
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

            case "deletedepartmentasync":
                {
                    if (!canDelete)
                    {
                        response.IsSuccess = false;
                        response.SetError(new ErrorMessage
                        {
                            Code = HttpStatusCode.Forbidden,
                            Description = "You do not have permission to delete departments."
                        });
                        break;
                    }
                    if (parameters.Length < 1 && string.IsNullOrEmpty(request.Parameters[0]?.ToString()))
                    {
                        response.IsSuccess = false;
                        response.SetError(new ErrorMessage
                        {
                            Code = HttpStatusCode.NotFound,
                            Description = "Invalid Department ID."
                        });
                        break;
                    }
                    var result = await _departmentService.DeleteDepartmentAsync(request.Parameters[0].ToString());
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
                    response.Data = "the delete is complete!";
                    break;
                }

            case "updatedepartmentasync":
                {
                    if (!canEdit)
                    {
                        response.IsSuccess = false;
                        response.SetError(new ErrorMessage
                        {
                            Code = HttpStatusCode.Forbidden,
                            Description = "You do not have permission to edit departments."
                        });
                        break;
                    }
                    if (parameters.Length < 1 || string.IsNullOrEmpty(request.Parameters[0]?.ToString()))
                    {
                        response.IsSuccess = false;
                        response.SetError(new ErrorMessage
                        {
                            Code = HttpStatusCode.NotFound,
                            Description = "Invalid request parameters."
                        });
                        break;
                    }
                    try
                    {
                        var updataDepartmentDto = JsonSerializer.Deserialize<UpdateDepartmentDto>(parameters[0].ToString(), new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        });
                        if (updataDepartmentDto == null || updataDepartmentDto.Id == Guid.Empty || string.IsNullOrEmpty(updataDepartmentDto.Name) || string.IsNullOrEmpty(updataDepartmentDto.Description))
                        {
                            response.IsSuccess = false;
                            response.SetError(new ErrorMessage
                            {
                                Code = HttpStatusCode.BadRequest,
                                Description = "Invalid department data."
                            });
                            break;
                        }
                        var updateResult = await _departmentService.UpdateDepartmentAsync(updataDepartmentDto);
                        if (!updateResult.IsSuccess)
                        {
                            response.IsSuccess = false;
                            response.SetError(new ErrorMessage
                            {
                                Code = HttpStatusCode.InternalServerError,
                                Description = updateResult.ErrorMessage
                            });
                            break;
                        }
                        response.IsSuccess = true;
                        response.Data = updateResult.Value;
                        break;

                    }
                    catch (Exception)
                    {
                        response.IsSuccess = false;
                        response.SetError(new ErrorMessage
                        {
                            Code = HttpStatusCode.InternalServerError,
                            Description = "An error occurred while updating the department."
                        });
                        break;
                    }
                }

            case "getalldepartmentsashtmlasync":
                {
                    if (!canView)
                    {
                        response.IsSuccess = false;
                        response.SetError(new ErrorMessage
                        {
                            Code = HttpStatusCode.Forbidden,
                            Description = "You do not have permission to view departments."
                        });
                        break;
                    }
                    var departments = await _departmentService.GetAllDepartmentsAsHtmlAsync();
                    if (!departments.IsSuccess)
                    {
                        response.IsSuccess = false;
                        response.SetError(new ErrorMessage
                        {
                            Code = HttpStatusCode.InternalServerError,
                            Description = departments.ErrorMessage
                        });
                        break;
                    }
                    return Ok(departments);
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
