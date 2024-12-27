using TicketTrackingSystem.Application.Dto;
using TicketTrackingSystem.Application.Model;
using TicketTrackingSystem.Common.Model;

namespace TicketTrackingSystem.Application.Interface;
public interface IDepartmentService
{
    Task<Result<DataTablesResponse<DepartmentDto>>> GetAllDepartmentsPaginatedAsync(DataTablesRequest request);
    Task<Result<DepartmentDto>> CreateDepartmentAsync(CreateDepartmentDto department);
    Task<Result<string>> GetAllDepartmentsAsHtmlAsync();
    Task<Result<string>> DeleteDepartmentAsync(string id);
    Task<Result<DepartmentDto>> UpdateDepartmentAsync(UpdateDepartmentDto updateDepartmentDto);
}
