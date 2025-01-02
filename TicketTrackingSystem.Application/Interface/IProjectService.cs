using TicketTrackingSystem.Application.Dto;
using TicketTrackingSystem.Application.Model;
using TicketTrackingSystem.Common.Model;

namespace TicketTrackingSystem.Application.Interface;
public interface IProjectService
{
    /// <summary>
    /// Creates a new project asynchronously.
    /// </summary>
    /// <param name="newProject">The details of the project to be created.</param>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// The result of the task contains a <see cref="Result{ProjectDto}"/> object.
    /// If the operation is successful, the <see cref="Result{ProjectDto}.IsSuccess"/> property will be true,
    /// and the <see cref="Result{ProjectDto}.Data"/> property will contain the created project's details.
    /// If the operation fails, the <see cref="Result{ProjectDto}.IsSuccess"/> property will be false,
    /// and the <see cref="Result{ProjectDto}.ErrorMessage"/> property will contain the error message.
    /// </returns>
    Task<Result<ProjectDto>> CreateProjectAsync(CreateProjectDto newProject);
    /// <summary>
    /// Creates a new project asynchronously.
    /// </summary>
    /// <param name="newProject">The details of the project to be created.</param>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// The result of the task contains a <see cref="Result{ProjectDto}"/> object.
    /// If the operation is successful, the <see cref="Result{ProjectDto}.IsSuccess"/> property will be true,
    /// and the <see cref="Result{ProjectDto}.Data"/> property will contain the created project's details.
    /// If the operation fails, the <see cref="Result{ProjectDto}.IsSuccess"/> property will be false,
    /// and the <see cref="Result{ProjectDto}.ErrorMessage"/> property will contain the error message.
    /// </returns>
    Task<Result<ProjectDto>> UpdateProjectAsync(UpdateProjectDto projectDto);
    /// <summary>
    /// Deletes a project asynchronously by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the project to be deleted.</param>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// The result of the task contains a <see cref="Result{string}"/> object.
    /// If the operation is successful, the <see cref="Result{string}.IsSuccess"/> property will be true,
    /// and the <see cref="Result{string}.Data"/> property will contain a success message.
    /// If the operation fails, the <see cref="Result{string}.IsSuccess"/> property will be false,
    /// and the <see cref="Result{string}.ErrorMessage"/> property will contain the error message.
    /// </returns>
    Task<Result<string>> DeleteProjectAsync(Guid id);
    /// <summary>
    /// Retrieves a paginated list of projects asynchronously based on the provided data tables request.
    /// </summary>
    /// <param name="request">The data tables request object containing pagination, sorting, and filtering information.</param>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// The result of the task contains a <see cref="Result{DataTablesResponse{ProjectDto}}"/> object.
    /// If the operation is successful, the <see cref="Result{DataTablesResponse{ProjectDto}}.IsSuccess"/> property will be true,
    /// and the <see cref="Result{DataTablesResponse{ProjectDto}}.Data"/> property will contain the paginated list of projects.
    /// If the operation fails, the <see cref="Result{DataTablesResponse{ProjectDto}}.IsSuccess"/> property will be false,
    /// and the <see cref="Result{DataTablesResponse{ProjectDto}}.ErrorMessage"/> property will contain the error message.
    /// </returns>
    Task<Result<DataTablesResponse<ProjectDto>>> GetAllProjectPaginatedAsync(DataTablesRequest request);
    /// <summary>
    /// Retrieves a project by its unique identifier asynchronously.
    /// </summary>
    /// <param name="id">The unique identifier of the project to be retrieved.</param>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// The result of the task contains a <see cref="Result{ProjectDto}"/> object.
    /// If the operation is successful, the <see cref="Result{ProjectDto}.IsSuccess"/> property will be true,
    /// and the <see cref="Result{ProjectDto}.Data"/> property will contain the project's details.
    /// If the operation fails, the <see cref="Result{ProjectDto}.IsSuccess"/> property will be false,
    /// and the <see cref="Result{ProjectDto}.ErrorMessage"/> property will contain the error message.
    /// </returns>
    Task<Result<ProjectDto>> GetProjectByIdAsync(Guid id);
    /// <summary>
    /// Retrieves a paginated list of projects for a specific client asynchronously based on the provided data tables request.
    /// </summary>
    /// <param name="request">The data tables request object containing pagination, sorting, and filtering information.</param>
    /// <param name="clientId">The unique identifier of the client for which to retrieve the projects.</param>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// The result of the task contains a <see cref="Result{DataTablesResponse{ProjectDto}}"/> object.
    /// If the operation is successful, the <see cref="Result{DataTablesResponse{ProjectDto}}.IsSuccess"/> property will be true,
    /// and the <see cref="Result{DataTablesResponse{ProjectDto}}.Data"/> property will contain the paginated list of projects for the specified client.
    /// If the operation fails, the <see cref="Result{DataTablesResponse{ProjectDto}}.IsSuccess"/> property will be false,
    /// and the <see cref="Result{DataTablesResponse{ProjectDto}}.ErrorMessage"/> property will contain the error message.
    /// </returns>
    Task<Result<DataTablesResponse<ProjectDto>>> GetAllUserProjectsAsync(DataTablesRequest request, Guid clientId);
    /// <summary>
    /// Assigns a user to a project with an optional stage.
    /// </summary>
    /// <param name="userId">The unique identifier of the user to be assigned to the project.</param>
    /// <param name="projectId">The unique identifier of the project to which the user will be assigned.</param>
    /// <param name="stage">
    /// The stage at which the user will be assigned to the project.
    /// If null, the user will be assigned to the project without a specific stage.
    /// </param>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// The result of the task contains a <see cref="Result{string}"/> object.
    /// If the operation is successful, the <see cref="Result{string}.IsSuccess"/> property will be true,
    /// and the <see cref="Result{string}.Data"/> property will contain a success message.
    /// If the operation fails, the <see cref="Result{string}.IsSuccess"/> property will be false,
    /// and the <see cref="Result{string}.ErrorMessage"/> property will contain the error message.
    /// </returns>
    Task<Result<string>> SetUserForProjectAsync(Guid userId, Guid projectId, int? stage);
    /// <summary>
    /// Removes a user from a project asynchronously.
    /// </summary>
    /// <param name="userId">The unique identifier of the user to be removed from the project.</param>
    /// <param name="projectId">The unique identifier of the project from which the user will be removed.</param>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// The result of the task contains a <see cref="Result{string}"/> object.
    /// If the operation is successful, the <see cref="Result{string}.IsSuccess"/> property will be true,
    /// and the <see cref="Result{string}.Data"/> property will contain a success message.
    /// If the operation fails, the <see cref="Result{string}.IsSuccess"/> property will be false,
    /// and the <see cref="Result{string}.ErrorMessage"/> property will contain the error message.
    /// </returns>
    Task<Result<string>> RemoveUserFromProjectAsync(Guid userId, Guid projectId);
    /// <summary>
    /// Retrieves a dropdown list of project stages as HTML-formatted options.
    /// </summary>
    /// <returns>
    /// A string containing HTML-formatted options for the project stages.
    /// Each option has a value representing the stage ID and a text label representing the stage name.
    /// If the operation fails, an empty string is returned.
    /// </returns>
    string GetStageDropdown();
}
