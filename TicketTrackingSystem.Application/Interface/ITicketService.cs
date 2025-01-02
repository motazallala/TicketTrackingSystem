using TicketTrackingSystem.Application.Dto;
using TicketTrackingSystem.Application.Model;
using TicketTrackingSystem.Common.Model;

namespace TicketTrackingSystem.Application.Interface;
public interface ITicketService
{
    /// <summary>
    /// Retrieves a paginated list of tickets based on the provided DataTables request and project ID.
    /// </summary>
    /// <param name="request">The DataTables request containing pagination, sorting, and filtering information.</param>
    /// <param name="projectId">The unique identifier of the project for which tickets are to be retrieved.</param>
    /// <returns>
    /// A Result object containing the paginated list of tickets wrapped in a DataTablesResponse object.
    /// If the operation is successful, the Result.IsSuccess property will be true, and the Result.Data property will contain the paginated list of tickets.
    /// If the operation fails, the Result.IsSuccess property will be false, and the Result.ErrorMessage property will contain the error message.
    /// </returns>
    Task<Result<DataTablesResponse<TicketDto>>> GetAllTicketPaginatedAsync(DataTablesRequest request, Guid projectId);
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

    Task<Result<TicketDto>> UpdateTicketStatusAsync(Guid id, int status);
    Task<Result<TicketDto>> UpdateTicketStageAsync(Guid id, int stage);
    Task<Result<TicketDto>> UpdateTicketMessageAsync(Guid id, string message);
    Task<Result<TicketDto>> UpdateTicketStatusWithAutoStageAsync(Guid id, int status, bool isFinished);
    Task<Result<TicketDto>> UpdateTicketWithAutoStageAsync(Guid id, string status, bool isFinished, string message = null);
    string GetTicketStatusDropdown();
}
