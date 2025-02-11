using TicketTrackingSystem.Application.Dto;
using TicketTrackingSystem.Application.Model;
using TicketTrackingSystem.Common.Model;

namespace TicketTrackingSystem.Application.Interface;
public interface ITicketService
{
    /// <summary>
    /// Retrieves a paginated list of tickets for a specific user based on the provided DataTables request and project ID.
    /// </summary>
    /// <param name="request">The DataTables request containing pagination, sorting, and filtering information.</param>
    /// <param name="projectId">The unique identifier of the project for which tickets are to be retrieved.</param>
    /// <param name="userId">The unique identifier of the user for whom tickets are to be retrieved.</param>
    /// <returns>
    /// A Task that represents the asynchronous operation.
    /// The Result object contains the paginated list of tickets wrapped in a DataTablesResponse object.
    /// If the operation is successful, the Result.IsSuccess property will be true, and the Result.Data property will contain the paginated list of tickets.
    /// If the operation fails, the Result.IsSuccess property will be false, and the Result.ErrorMessage property will contain the error message.
    /// </returns>
    Task<Result<DataTablesResponse<TicketDto>>> GetAllTicketForUserPaginatedAsync(DataTablesRequest request, Guid projectId, Guid userId);
    /// <summary>
    /// Retrieves a ticket by its unique identifier asynchronously.
    /// </summary>
    /// <param name="id">The unique identifier of the ticket to retrieve.</param>
    /// <returns>
    /// A Task that represents the asynchronous operation.
    /// The Result object contains the retrieved ticket wrapped in a TicketDto object.
    /// If the operation is successful, the Result.IsSuccess property will be true, and the Result.Data property will contain the ticket.
    /// If the operation fails, the Result.IsSuccess property will be false, and the Result.ErrorMessage property will contain the error message.
    /// </returns>
    Task<Result<TicketDto>> GetTicketByIdAsync(Guid id);
    /// <summary>
    /// Adds a new ticket to the system asynchronously.
    /// </summary>
    /// <param name="ticketDto">The CreateTicketDto object containing the details of the ticket to be added.</param>
    /// <returns>
    /// A Task that represents the asynchronous operation.
    /// The Result object contains the added ticket wrapped in a TicketDto object.
    /// If the operation is successful, the Result.IsSuccess property will be true, and the Result.Data property will contain the added ticket.
    /// If the operation fails, the Result.IsSuccess property will be false, and the Result.ErrorMessage property will contain the error message.
    /// </returns>
    Task<Result<TicketDto>> AddTicketAsync(CreateTicketDto ticketDto);
    /// <summary>
    /// Updates the ticket with the provided parameters. If the user is finished with the ticket, it will update the ticket's status and add a message. If the user is in any stage, it will check if the message is visible for the user or not. If the user is in the stage 1, it will add a message for the member in the stage 2 from stage 1. If the ticket is returned to reassign it for the user, it will log that the ticket is not assigned to any member. If the ticket is for the first time, it will log that the ticket is not assigned to any member. If the user is in the stage 2, it will check if the user wants to return to the ticket to stage 1. If the user solves the ticket and finishes the ticket, it will log the change of the stage and add a message.
    /// </summary>
    /// <param name="id">The ID of the ticket to update.</param>
    /// <param name="status">The new status of the ticket.</param>
    /// <param name="isFinished">A boolean value indicating if the user is finished with the ticket.</param>
    /// <param name="userId">The ID of the user who is updating the ticket.</param>
    /// <param name="message">The message to be added to the ticket.</param>
    /// <returns>A Result<TicketDto> object containing the updated ticket data or a failure message if an error occurs.</returns>
    Task<Result<TicketDto>> UpdateTicketWithAutoStageAsync(Guid id, string status, bool isFinished, Guid userId, string message = null);
    Task<Result<DataTablesResponse<TicketDto>>> GetAllTicketForMemberPaginatedAsync(DataTablesRequest request, Guid projectId, Guid userId, bool reserved);
    Task<Result<TicketDto>> RemoveTicketFromUserAsync(Guid ticketId, Guid userId);
    string GetTicketStatusDropdown();
    Task<string> GetAllFreeMembersDropdownAsync(Guid projectId, Guid userId);
    /// <summary>
    /// Assigns a ticket to a user with the given estimation time.
    /// </summary>
    /// <param name="ticketId">The unique identifier of the ticket to be assigned.</param>
    /// <param name="userId">The unique identifier of the user to be assigned to the ticket.</param>
    /// <param name="estimationTime">The estimated completion date for the ticket.</param>
    /// <returns>A <see cref="Result{TicketDto}"/> containing the <see cref="TicketDto"/> of the ticket if successful, or a <see cref="Result{string}"/> containing an error message if not.</returns>
    /// <exception cref="ArgumentException">Thrown when the estimation time is in the past or when the ticket or user does not exist.</exception>
    Task<Result<TicketDto>> AssignTicketToUserAsync(Guid ticketId, Guid userId, DateTime estimationTime);
    Task<Result<bool>> CheckEstimatedCompletionDateAsync(Guid ticketId);
    Task<Result<bool>> SetEstimatedCompletionDateForReassignTicketAsync(Guid ticketId, Guid userId, DateTime estimationTime);
    Task<Result<TicketDto>> ReAssignTicketAsync(Guid ticketId, Guid userId);
}
