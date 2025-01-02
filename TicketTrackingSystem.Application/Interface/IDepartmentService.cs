using TicketTrackingSystem.Application.Dto;
using TicketTrackingSystem.Application.Model;
using TicketTrackingSystem.Common.Model;

namespace TicketTrackingSystem.Application.Interface;
public interface IDepartmentService
{
    /// <summary>
    /// Retrieves a paginated list of departments based on the provided DataTables request.
    /// </summary>
    /// <param name="request">The DataTables request containing pagination, sorting, and search parameters.</param>
    /// <returns>
    /// A Result object containing the paginated list of DepartmentDtos if successful, or an error message if an exception occurs.
    /// The DataTablesResponse contains the total number of records, filtered records, and the actual data.
    /// </returns>
    Task<Result<DataTablesResponse<DepartmentDto>>> GetAllDepartmentsPaginatedAsync(DataTablesRequest request);
    /// <summary>
    /// Creates a new department in the system.
    /// </summary>
    /// <param name="department">The Department data transfer object (DTO) containing the details of the new department.</param>
    /// <returns>
    /// A Result object containing the newly created DepartmentDto if successful, or an error message if an exception occurs.
    /// The DepartmentDto contains the unique identifier, name, and other relevant information about the department.
    /// </returns>
    Task<Result<DepartmentDto>> CreateDepartmentAsync(CreateDepartmentDto department);
    /// <summary>
    /// Retrieves a list of all departments in HTML format.
    /// </summary>
    /// <returns>
    /// A Result object containing a string representing the HTML markup of all departments if successful,
    /// or an error message if an exception occurs.
    /// The string contains the HTML table with department details, including their unique identifiers, names, and other relevant information.
    /// </returns>
    Task<Result<string>> GetAllDepartmentsAsHtmlAsync();
    /// <summary>
    /// Deletes a department from the system based on the provided unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the department to be deleted.</param>
    /// <returns>
    /// A Result object containing a string representing the success message if the department is deleted successfully,
    /// or an error message if an exception occurs.
    /// The string contains the success message indicating that the department has been deleted.
    /// </returns>
    Task<Result<string>> DeleteDepartmentAsync(string id);
    /// <summary>
    /// Updates an existing department in the system based on the provided UpdateDepartmentDto.
    /// </summary>
    /// <param name="updateDepartmentDto">
    /// The Department data transfer object (DTO) containing the unique identifier and updated details of the department.
    /// The DTO should include the department's unique identifier, name, and other relevant information.
    /// </param>
    /// <returns>
    /// A Result object containing the updated DepartmentDto if successful, or an error message if an exception occurs.
    /// The DepartmentDto contains the unique identifier, name, and other relevant information about the department.
    /// </returns>
    Task<Result<DepartmentDto>> UpdateDepartmentAsync(UpdateDepartmentDto updateDepartmentDto);
}
