using TicketTrackingSystem.Application.Model;
using TicketTrackingSystem.Application.Services;
using TicketTrackingSystem.Common.Model;

namespace TicketTrackingSystem.Application.Interface;
public interface ITicketHistoryService
{
    Task<Result<DataTablesResponse<TicketHistoryReportDto>>> GetAllTicketHistoryForReportAsync(DataTablesRequest request, string? stageFilter, string? deliveryStatusFilter);
    string GetDeliveryStatusDropdown();
}
