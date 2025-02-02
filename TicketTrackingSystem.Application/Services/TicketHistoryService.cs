using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TicketTrackingSystem.Application.Interface;
using TicketTrackingSystem.Application.Model;
using TicketTrackingSystem.Common.Model;
using TicketTrackingSystem.Core.Model.Enum;
using TicketTrackingSystem.DAL.Interface;

namespace TicketTrackingSystem.Application.Services;
public class TicketHistoryService : ITicketHistoryService
{
    private readonly IUnitOfWork _unitOfWork;
    public TicketHistoryService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<DataTablesResponse<TicketHistoryReportDto>>> GetAllTicketHistoryForReportAsync(
            DataTablesRequest request,
            string? stageFilter,
            string? deliveryStatusFilter)
    {
        try
        {
            // Base query (unchanged)
            var query = _unitOfWork.TicketHistory.GetAllAsQueryable()
                .Include(t => t.Ticket)
                .Include(t => t.AssignedTo)
                .AsNoTracking();


            // Subqueries for Stage1 and Stage2
            var stage1Query = query.Where(h => h.StageAfterChange == Stage.Stage1);
            var stage2Query = query.Where(h => h.StageAfterChange == Stage.Stage2);

            // Combine using left join to include Stage1 entries with/without Stage2
            var combinedQuery =
                from s1 in stage1Query
                join s2 in stage2Query
                    on new { s1.CycleNumber, s1.TicketId }
                    equals new { s2.CycleNumber, s2.TicketId }
                    into s2Group
                from s2 in s2Group.DefaultIfEmpty() // Left join
                select new
                {
                    s1.TicketId,
                    s1.Ticket.Title,
                    s1.Date,
                    s1.DeliveryStatus,
                    AssignedFrom = s1.AssignedTo.UserName,
                    s1.StageAfterChange,
                    s1Estimation = s1.EstimatedCompletionDate,
                    s2Data = s2 != null ? s2.Date : (DateTime?)null,
                    s2DeliveryStatus = (DeliveryStatus?)s2.DeliveryStatus,
                    s2AssignedTo = s2 != null ? s2.AssignedTo.UserName : null,
                    s2StageAfterChange = (Stage?)s2.StageAfterChange,
                    s2Estimation = s2 != null ? s2.EstimatedCompletionDate : (DateTime?)null,
                };


            var totalRecords = await combinedQuery.CountAsync();

            if (!string.IsNullOrEmpty(stageFilter) && Enum.TryParse<Stage>(stageFilter, true, out var stageEnum))
            {
                if (!string.IsNullOrEmpty(deliveryStatusFilter) &&
                    Enum.TryParse<DeliveryStatus>(deliveryStatusFilter, true, out var deliveryStatusEnum))
                {
                    if (stageEnum.Equals(Stage.Stage1))
                    {
                        combinedQuery = combinedQuery.Where(h => h.StageAfterChange == Stage.Stage1 && h.DeliveryStatus.Equals(deliveryStatusEnum));
                    }
                    else
                    {
                        combinedQuery = combinedQuery.Where(h => h.s2StageAfterChange == Stage.Stage2 && h.s2DeliveryStatus.Equals(deliveryStatusEnum));
                    }
                }
                else
                {
                    if (stageEnum.Equals(Stage.Stage1))
                    {
                        combinedQuery = combinedQuery.Where(h => h.StageAfterChange == Stage.Stage1);
                    }
                    else
                    {
                        combinedQuery = combinedQuery.Where(h => h.s2StageAfterChange == Stage.Stage2);
                    }
                }

            }
            else
            {
                if (!string.IsNullOrEmpty(deliveryStatusFilter) &&
                    Enum.TryParse<DeliveryStatus>(deliveryStatusFilter, true, out var deliveryStatusEnumx))
                {
                    combinedQuery = combinedQuery.Where(h => h.DeliveryStatus == deliveryStatusEnumx &&
                                          h.s2DeliveryStatus == deliveryStatusEnumx);
                }
            }


            if (!string.IsNullOrEmpty(request.Search?.Value))
            {
                var searchValue = request.Search.Value.ToLower();
                combinedQuery = combinedQuery.Where(p => p.Title.ToLower().Contains(searchValue) ||
                                                         p.AssignedFrom.ToLower().Contains(searchValue) ||
                                                         p.s2AssignedTo.ToLower().Contains(searchValue));
            }

            // Get counts from the server-side query
            var filteredRecords = await combinedQuery.CountAsync();

            // Apply pagination on the server side
            var xxxx = combinedQuery
                .OrderByDescending(c => c.Date)
                .ThenByDescending(c => c.s2Data)
                .Skip(request.Start)
                .Take(request.Length)
                .ToQueryString();
            var paginatedServerData = await combinedQuery
                .OrderByDescending(c => c.Date)
                .ThenByDescending(c => c.s2Data)
                .Skip(request.Start)
                .Take(request.Length)
                .ToListAsync(); // Execute SQL query here

            // Convert to DTOs on the client side (in-memory)
            var paginatedData = paginatedServerData.Select(x => new TicketHistoryReportDto
            {
                TicketId = x.TicketId,
                Title = x.Title,
                DeliveryStatusFrom = x.s1Estimation.HasValue ? x.DeliveryStatus.HasValue ? x.DeliveryStatus.ToString() : "OnTime" : "N/A", // Client-side conversion
                AssignedFrom = x.AssignedFrom,
                StageFrom = x.StageAfterChange.ToString(),
                DeliveryStatusTo = x.s2Estimation.HasValue ? x.s2DeliveryStatus.HasValue ? x.s2DeliveryStatus?.ToString() : "OnTime" : "N/A",
                AssignedTo = x.s2AssignedTo ?? "N/A",
                StageTo = x.s2StageAfterChange?.ToString() ?? "N/A"
            }).ToList();

            // Build response
            var response = new DataTablesResponse<TicketHistoryReportDto>
            {
                Draw = request.Draw,
                RecordsTotal = totalRecords,
                RecordsFiltered = filteredRecords,
                Data = paginatedData
            };

            return Result<DataTablesResponse<TicketHistoryReportDto>>.Success(response);
        }
        catch (Exception ex)
        {
            return Result<DataTablesResponse<TicketHistoryReportDto>>.Failure($"Error: {ex.Message}");
        }
    }

    public string GetDeliveryStatusDropdown()
    {
        var deliveryStatus = Enum.GetValues(typeof(DeliveryStatus)).Cast<DeliveryStatus>().Select(v => new SelectListItem
        {
            Value = ((int)v).ToString(),
            Text = v.ToString()
        }).ToList();

        var htmlString = new System.Text.StringBuilder();
        foreach (var item in deliveryStatus)
        {
            htmlString.Append($"<option value='{item.Value}'>{item.Text}</option>");
        }
        return htmlString.ToString();
    }
}

public class TicketHistoryReportDto
{
    public Guid TicketId { get; set; }
    public string Title { get; set; }
    public string? DeliveryStatusFrom { get; set; }
    public string? AssignedFrom { get; set; }
    public string? StageFrom { get; set; }
    public string? DeliveryStatusTo { get; set; }
    public string? AssignedTo { get; set; }
    public string? StageTo { get; set; }
}