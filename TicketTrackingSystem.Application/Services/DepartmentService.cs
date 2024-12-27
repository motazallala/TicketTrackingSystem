using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TicketTrackingSystem.Application.Dto;
using TicketTrackingSystem.Application.Interface;
using TicketTrackingSystem.Application.Model;
using TicketTrackingSystem.Common.ExtensionMethod;
using TicketTrackingSystem.Common.Model;
using TicketTrackingSystem.Core.Model;
using TicketTrackingSystem.DAL.Interface;

namespace TicketTrackingSystem.Application.Services;
public class DepartmentService : IDepartmentService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public DepartmentService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<Result<DataTablesResponse<DepartmentDto>>> GetAllDepartmentsPaginatedAsync(DataTablesRequest request)
    {
        try
        {
            var query = _unitOfWork.Departments.GetAllAsQueryable().AsNoTracking();
            if (!string.IsNullOrEmpty(request.Search?.Value))
            {
                var searchValue = request.Search.Value.ToLower();
                query = query.Where(p => p.Name.ToLower().Contains(searchValue) || p.Description.ToLower().Contains(searchValue));
            }
            // Apply ordering
            if (request.Order != null && request.Order.Any())
            {
                var order = request.Order.First();
                var columnName = request.Columns[order.Column].Data;
                var direction = order.Dir;

                // Dynamically apply ordering
                query = direction == "asc"
                    ? query.OrderByDynamic(columnName, true)
                    : query.OrderByDynamic(columnName, false);
            }
            // Get the total count before pagination
            var recordsTotal = await query.CountAsync();
            // Apply pagination
            var paginatedData = await query
                .Skip(request.Start)
                .Take(request.Length)
                .ToListAsync();
            // Now project the roles separately in the mapping phase
            var departmentDtos = _mapper.Map<IEnumerable<DepartmentDto>>(paginatedData);
            var response = new DataTablesResponse<DepartmentDto>
            {
                Draw = request.Draw,
                RecordsTotal = recordsTotal,
                RecordsFiltered = recordsTotal,
                Data = departmentDtos
            };
            return Result<DataTablesResponse<DepartmentDto>>.Success(response);
        }
        catch (Exception ex)
        {

            return Result<DataTablesResponse<DepartmentDto>>.Failure(ex.Message);
        }
    }
    public async Task<Result<DepartmentDto>> CreateDepartmentAsync(CreateDepartmentDto department)
    {
        var departmentEntity = _mapper.Map<Department>(department);
        await _unitOfWork.Departments.AddAsync(departmentEntity);
        try
        {
            await _unitOfWork.CompleteAsync();
            return Result<DepartmentDto>.Success();
        }
        catch (Exception ex)
        {

            return Result<DepartmentDto>.Failure(ex.Message);
        }
    }
    public async Task<Result<string>> DeleteDepartmentAsync(string id)
    {
        var department = await _unitOfWork.Departments.GetByIdAsync(Guid.Parse(id));
        if (department == null)
        {
            return Result<string>.Failure("Department not found.");
        }

        _unitOfWork.Departments.Remove(department);
        await _unitOfWork.CompleteAsync();
        return Result<string>.Success("Department Is Deleted");
    }
    public async Task<Result<DepartmentDto>> UpdateDepartmentAsync(UpdateDepartmentDto updateDepartmentDto)
    {
        try
        {

            var department = await _unitOfWork.Departments.GetByIdAsync(updateDepartmentDto.Id);
            if (department == null)
            {
                return Result<DepartmentDto>.Failure("Department not found.");
            }

            _mapper.Map(updateDepartmentDto, department);
            _unitOfWork.Departments.Update(department);
            await _unitOfWork.CompleteAsync();
            var departmentDto = _mapper.Map<DepartmentDto>(department);
            return Result<DepartmentDto>.Success(departmentDto);
        }
        catch (Exception ex)
        {

            return Result<DepartmentDto>.Failure(ex.Message);
        }

    }
    public async Task<bool> GetDepartmentAsync()
    {
        throw new NotImplementedException();
    }
    public async Task<Result<string>> GetAllDepartmentsAsHtmlAsync()
    {

        try
        {
            var departments = await _unitOfWork.Departments.GetAllAsync();

            // Build HTML options for the dropdown
            var departmentDropdown = string.Join(Environment.NewLine,
                departments.Select(d => $"<option value='{d.Id}'>{d.Name}</option>"));

            return Result<string>.Success(departmentDropdown);

        }
        catch (Exception ex)
        {

            return Result<string>.Failure(ex.Message);
        }
    }

}
