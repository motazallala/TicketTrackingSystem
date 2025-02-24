using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using TicketTrackingSystem.Application.Dto;
using TicketTrackingSystem.Application.HttpResponse;
using TicketTrackingSystem.Application.Interface;
using TicketTrackingSystem.Common.Model;

namespace TicketTrackingSystem.MVC.Controllers;

[Authorize]
[Route("department")]
public class DepartmentController : Controller
{
    private readonly IPermissionService _permissionService;
    private readonly IUserService _userService;
    private readonly IDepartmentService _departmentService;

    public DepartmentController(
        IPermissionService permissionService,
        IUserService userService,
        IDepartmentService departmentService)
    {
        _permissionService = permissionService;
        _userService = userService;
        _departmentService = departmentService;
    }

    public async Task<IActionResult> Index()
    {
        // Check necessary permissions before displaying the view.
        var permissions = await CheckPermissionsAsync(PermissionName.ViewDepartment.ToString());
        if (!permissions[PermissionName.ViewDepartment.ToString()])
        {
            return Forbid();
        }
        return View();
    }

    [HttpPost("getalldepartmentspaginatedasync")]
    public async Task<IActionResult> GetAllDepartmentsPaginatedAsync([FromBody] DataTablesRequest dataTablesRequest)
    {
        var response = new BaseResponse();

        // Check if the model binding for dataTablesRequest is valid.
        if (!ModelState.IsValid)
        {
            response.IsSuccess = false;
            response.SetErrorFromModelState(ModelState);
            return Ok(response);
        }

        var permissions = await CheckPermissionsAsync(PermissionName.ViewDepartment.ToString());
        if (!permissions[PermissionName.ViewDepartment.ToString()])
        {
            response.IsSuccess = false;
            response.SetError(new ErrorMessage
            {
                Code = HttpStatusCode.Forbidden,
                Description = "You do not have permission to view departments."
            });
            return Ok(response);
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
            return Ok(response);
        }

        response.IsSuccess = true;
        response.Data = paginationResult.Value;
        return Ok(response);
    }

    [HttpPost("createdepartmentasync")]
    public async Task<IActionResult> CreateDepartmentAsync([FromBody] CreateDepartmentDto department)
    {
        var response = new BaseResponse();

        // Check the model state for CreateDepartmentDto.
        if (!ModelState.IsValid)
        {
            response.IsSuccess = false;
            response.SetErrorFromModelStateAsDictionary(ModelState);
            return Ok(response);
        }

        var permissions = await CheckPermissionsAsync(PermissionName.CreateDepartment.ToString());
        if (!permissions[PermissionName.CreateDepartment.ToString()])
        {
            response.IsSuccess = false;
            response.SetError(new ErrorMessage
            {
                Code = HttpStatusCode.Forbidden,
                Description = "You do not have permission to create departments."
            });
            return Ok(response);
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
            return Ok(response);
        }

        response.IsSuccess = true;
        response.Data = createResult.Value;
        response.SuccessMessage = $"The {department.Name} Department Created Successfully";
        return Ok(response);
    }

    [HttpDelete("deletedepartmentasync/{id}")]
    public async Task<IActionResult> DeleteDepartmentAsync(Guid id)
    {
        var response = new BaseResponse();

        var permissions = await CheckPermissionsAsync(PermissionName.DeleteDepartment.ToString());
        if (!permissions[PermissionName.DeleteDepartment.ToString()])
        {
            response.IsSuccess = false;
            response.SetError(new ErrorMessage
            {
                Code = HttpStatusCode.Forbidden,
                Description = "You do not have permission to delete departments."
            });
            return Ok(response);
        }

        if (id.Equals(Guid.Empty))
        {
            response.IsSuccess = false;
            response.SetError(new ErrorMessage
            {
                Code = HttpStatusCode.BadRequest,
                Description = "Invalid Department ID."
            });
            return Ok(response);
        }

        var result = await _departmentService.DeleteDepartmentAsync(id);
        if (!result.IsSuccess)
        {
            response.IsSuccess = false;
            response.SetError(new ErrorMessage
            {
                Code = HttpStatusCode.NotFound,
                Description = result.ErrorMessage
            });
            return Ok(response);
        }

        response.IsSuccess = true;
        response.Data = "The delete is complete!";
        response.SuccessMessage = "The Department is Deleted!";
        return Ok(response);
    }

    [HttpDelete("deletedepartmentcascadeasync/{id}")]
    public async Task<IActionResult> DeleteDepartmentCascadeAsync(Guid id)
    {
        var response = new BaseResponse();

        var permissions = await CheckPermissionsAsync(PermissionName.DeleteDepartment.ToString());
        if (!permissions[PermissionName.DeleteDepartment.ToString()])
        {
            response.IsSuccess = false;
            response.SetError(new ErrorMessage
            {
                Code = HttpStatusCode.Forbidden,
                Description = "You do not have permission to delete departments."
            });
            return Ok(response);
        }

        if (id.Equals(Guid.Empty))
        {
            response.IsSuccess = false;
            response.SetError(new ErrorMessage
            {
                Code = HttpStatusCode.BadRequest,
                Description = "Invalid Department ID."
            });
            return Ok(response);
        }

        var result = await _departmentService.DeleteDepartmentCascadeAsync(id);
        if (!result.IsSuccess)
        {
            response.IsSuccess = false;
            response.SetError(new ErrorMessage
            {
                Code = HttpStatusCode.NotFound,
                Description = result.ErrorMessage
            });
            return Ok(response);
        }

        response.IsSuccess = true;
        response.Data = "The delete is complete!";
        return Ok(response);
    }

    [HttpPut("updatedepartmentasync")]
    public async Task<IActionResult> UpdateDepartmentAsync([FromBody] UpdateDepartmentDto updateDepartmentDto)
    {
        var response = new BaseResponse();

        // Check the model state for UpdateDepartmentDto.
        if (!ModelState.IsValid)
        {
            response.IsSuccess = false;
            response.SetErrorFromModelStateAsDictionary(ModelState);
            return Ok(response);
        }

        var permissions = await CheckPermissionsAsync(PermissionName.EditDepartment.ToString());
        if (!permissions[PermissionName.EditDepartment.ToString()])
        {
            response.IsSuccess = false;
            response.SetError(new ErrorMessage
            {
                Code = HttpStatusCode.Forbidden,
                Description = "You do not have permission to edit departments."
            });
            return Ok(response);
        }

        if (updateDepartmentDto == null || updateDepartmentDto.Id == Guid.Empty ||
            string.IsNullOrEmpty(updateDepartmentDto.Name) || string.IsNullOrEmpty(updateDepartmentDto.Description))
        {
            response.IsSuccess = false;
            response.SetError(new ErrorMessage
            {
                Code = HttpStatusCode.BadRequest,
                Description = "Invalid department data."
            });
            return Ok(response);
        }

        var updateResult = await _departmentService.UpdateDepartmentAsync(updateDepartmentDto);
        if (!updateResult.IsSuccess)
        {
            response.IsSuccess = false;
            response.SetError(new ErrorMessage
            {
                Code = HttpStatusCode.InternalServerError,
                Description = updateResult.ErrorMessage
            });
            return Ok(response);
        }

        response.IsSuccess = true;
        response.Data = updateResult.Value;
        return Ok(response);
    }

    [HttpGet("getalldepartmentsashtmlasync")]
    public async Task<IActionResult> GetAllDepartmentsAsHtmlAsync()
    {
        var response = new BaseResponse();

        var permissions = await CheckPermissionsAsync(PermissionName.ViewDepartment.ToString());
        if (!permissions[PermissionName.ViewDepartment.ToString()])
        {
            response.IsSuccess = false;
            response.SetError(new ErrorMessage
            {
                Code = HttpStatusCode.Forbidden,
                Description = "You do not have permission to view departments."
            });
            return Ok(response);
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
            return Ok(response);
        }

        response.IsSuccess = true;
        response.Data = departments;
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
