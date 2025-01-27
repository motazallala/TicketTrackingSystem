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


    //public async Task<Result<DataTablesResponse<TicketHistoryReportDto>>> GetAllTicketHistoryForReportAsync(
    //    DataTablesRequest request,
    //    string stageFilter = null,
    //    string deliveryStatusFilter = null)
    //{
    //    try
    //    {
    //        // Base query for child histories with children
    //        var query = _unitOfWork.TicketHistory.GetAllAsQueryable().Where(p => p.HistoryType == HistoryType.Assignment)
    //            .Include(h => h.Ticket)
    //            .Include(h => h.AssignedTo)
    //            .Include(h => h.User)
    //            .Include(h => h.Children)
    //                .ThenInclude(c => c.AssignedTo)
    //            .Include(h => h.Children)
    //                .ThenInclude(c => c.User)
    //            .Where(h => h.ParentId == null) // Only child histories
    //            .AsNoTracking();

    //        // Apply global search
    //        if (!string.IsNullOrEmpty(request.Search?.Value))
    //        {
    //            var searchValue = request.Search.Value.ToLower();
    //            query = query.Where(h =>
    //                h.Ticket.Title.ToLower().Contains(searchValue) ||
    //                h.Ticket.AssignedTo.UserName.ToLower().Contains(searchValue) ||
    //                h.User.UserName.ToLower().Contains(searchValue)
    //            );
    //        }


    //        if (!string.IsNullOrEmpty(stageFilter) && Enum.TryParse<Stage>(stageFilter, true, out var stageEnum))
    //        {
    //            if (!string.IsNullOrEmpty(deliveryStatusFilter) &&
    //                Enum.TryParse<DeliveryStatus>(deliveryStatusFilter, true, out var deliveryStatusEnum))
    //            {
    //                if (stageEnum.Equals(Stage.Stage1))
    //                {
    //                    query = query.Where(h => h.StageAfterChange == Stage.Stage1 && h.DeliveryStatus.Equals(deliveryStatusEnum));
    //                }
    //                else
    //                {
    //                    query = query.Where(h => h.Children.Any(c => c.StageAfterChange == Stage.Stage2 && c.DeliveryStatus.Equals(deliveryStatusEnum)));
    //                }
    //            }
    //            else
    //            {
    //                if (stageEnum.Equals(Stage.Stage1))
    //                {
    //                    query = query.Where(h => h.StageAfterChange == Stage.Stage1);
    //                }
    //                else
    //                {
    //                    query.Where(h => h.Children.Any(c => c.StageAfterChange == Stage.Stage2));
    //                }
    //            }

    //        }
    //        else
    //        {
    //            if (!string.IsNullOrEmpty(deliveryStatusFilter) &&
    //                Enum.TryParse<DeliveryStatus>(deliveryStatusFilter, true, out var deliveryStatusEnumx))
    //            {
    //                query = query.Where(h => h.DeliveryStatus == deliveryStatusEnumx &&
    //                                      h.Children.Any(c => c.DeliveryStatus == deliveryStatusEnumx));
    //            }
    //        }

    //        // Get counts
    //        var recordsTotal = await _unitOfWork.TicketHistory.GetAllAsQueryable().Where(p => p.HistoryType == HistoryType.Assignment).CountAsync(h => h.ParentId == null);
    //        var recordsFiltered = await query.CountAsync();


    //        // Apply pagination
    //        var paginatedData = await query
    //            .OrderBy(c => c.Date)
    //            .Skip(request.Start)
    //            .Take(request.Length)
    //            .ToListAsync();

    //        // Map to DTOs
    //        var dtos = new List<TicketHistoryReportDto>();
    //        foreach (var parent in paginatedData)
    //        {
    //            // Create entry for child with children
    //            if (parent.Children.Any())
    //            {
    //                foreach (var child in parent.Children)
    //                {
    //                    dtos.Add(new TicketHistoryReportDto
    //                    {
    //                        TicketId = parent.Ticket.Id,
    //                        Title = parent.Ticket.Title,
    //                        DeliveryStatusFrom = parent.DeliveryStatus?.ToString() ?? "N/A",
    //                        AssignedFrom = parent.AssignedTo?.UserName ?? "N/A",
    //                        StageFrom = parent.StageAfterChange.ToString(),
    //                        DeliveryStatusTo = child.DeliveryStatus?.ToString() ?? "N/A",
    //                        AssignedTo = child.AssignedTo?.UserName ?? "N/A",
    //                        StageTo = child.StageAfterChange.ToString()
    //                    });
    //                }
    //            }
    //            // Create entry for child without children
    //            else
    //            {
    //                dtos.Add(new TicketHistoryReportDto
    //                {
    //                    TicketId = parent.Ticket.Id,
    //                    Title = parent.Ticket.Title,
    //                    DeliveryStatusFrom = parent.DeliveryStatus?.ToString() ?? "N/A",
    //                    AssignedFrom = parent.AssignedTo?.UserName ?? "N/A",
    //                    StageFrom = parent.StageAfterChange.ToString(),
    //                    DeliveryStatusTo = "N/A",
    //                    AssignedTo = parent.StageBeforeChange.Equals(Stage.Stage2) ? parent.User.UserName : "N/A",
    //                    StageTo = parent.StageBeforeChange.Equals(Stage.Stage2) ? parent.StageBeforeChange.ToString() : "N/A"
    //                });
    //            }
    //        }

    //        return Result<DataTablesResponse<TicketHistoryReportDto>>.Success(new DataTablesResponse<TicketHistoryReportDto>
    //        {
    //            Draw = request.Draw,
    //            RecordsTotal = recordsTotal,
    //            RecordsFiltered = recordsFiltered,
    //            Data = dtos
    //        });
    //    }
    //    catch (Exception ex)
    //    {
    //        return Result<DataTablesResponse<TicketHistoryReportDto>>.Failure($"Error: {ex.Message}");
    //    }
    //}


    public async Task<Result<DataTablesResponse<TicketHistoryReportDto>>> GetAllTicketHistoryForReportAsync(
    DataTablesRequest request,
    string stageFilter = null,
    string deliveryStatusFilter = null)
    {
        try
        {
            // Base query for child histories with children
            var query = _unitOfWork.TicketHistory.GetAllAsQueryable().Where(p => p.HistoryType == HistoryType.Assignment && p.ActionName != ActionName.Reassign)
                .Include(h => h.Ticket)
                .Include(h => h.AssignedTo)
                .Include(h => h.User)
                .Include(h => h.Parent)
                    .ThenInclude(c => c.User)
                .Include(h => h.Parent)
                    .ThenInclude(c => c.AssignedTo)
                .AsNoTracking();

            // Apply global search
            if (!string.IsNullOrEmpty(request.Search?.Value))
            {
                var searchValue = request.Search.Value.ToLower();
                query = query.Where(h =>
                    h.Ticket.Title.ToLower().Contains(searchValue) ||
                    h.Ticket.AssignedTo.UserName.ToLower().Contains(searchValue) ||
                    h.User.UserName.ToLower().Contains(searchValue)
                );
            }


            if (!string.IsNullOrEmpty(stageFilter) && Enum.TryParse<Stage>(stageFilter, true, out var stageEnum))
            {
                if (!string.IsNullOrEmpty(deliveryStatusFilter) &&
                    Enum.TryParse<DeliveryStatus>(deliveryStatusFilter, true, out var deliveryStatusEnum))
                {
                    if (stageEnum.Equals(Stage.Stage1))
                    {
                        query = query.Where(h => h.StageBeforeChange == Stage.Stage1 && h.Parent.DeliveryStatus.Equals(deliveryStatusEnum));
                    }
                    else
                    {
                        query = query.Where(h => h.StageAfterChange == Stage.Stage2 && h.DeliveryStatus.Equals(deliveryStatusEnum));
                    }
                }
                else
                {
                    if (stageEnum.Equals(Stage.Stage1))
                    {
                        query = query.Where(h => h.StageBeforeChange == Stage.Stage1);
                    }
                    else
                    {
                        query.Where(h => h.StageAfterChange == Stage.Stage2);
                    }
                }

            }
            else
            {
                if (!string.IsNullOrEmpty(deliveryStatusFilter) &&
                    Enum.TryParse<DeliveryStatus>(deliveryStatusFilter, true, out var deliveryStatusEnumx))
                {
                    query = query.Where(h => h.Parent.DeliveryStatus == deliveryStatusEnumx &&
                                          h.DeliveryStatus == deliveryStatusEnumx);
                }
            }

            // Get counts
            var recordsTotal = await _unitOfWork.TicketHistory.GetAllAsQueryable().Where(p => p.HistoryType == HistoryType.Assignment && p.ActionName != ActionName.Reassign).CountAsync();
            var recordsFiltered = await query.CountAsync();


            // Apply pagination
            var paginatedData = await query
                .OrderByDescending(c => c.Date)
                .Skip(request.Start)
                .Take(request.Length)
                .ToListAsync();

            // Map to DTOs
            var dtos = new List<TicketHistoryReportDto>();
            foreach (var child in paginatedData)
            {
                // Create entry for child with children
                if (child.Parent is not null)
                {

                    dtos.Add(new TicketHistoryReportDto
                    {
                        TicketId = child.Ticket.Id,
                        Title = child.Ticket.Title,
                        DeliveryStatusFrom = child.Parent.DeliveryStatus?.ToString() ?? "N/A",
                        AssignedFrom = child.Parent.AssignedTo?.UserName ?? "N/A",
                        StageFrom = child.Parent.StageAfterChange.ToString(),
                        DeliveryStatusTo = child.DeliveryStatus?.ToString() ?? "N/A",
                        AssignedTo = child.AssignedTo?.UserName ?? "N/A",
                        StageTo = child.StageAfterChange.ToString(),
                    });
                }
                // Create entry for child without children
                else
                {

                    dtos.Add(new TicketHistoryReportDto
                    {
                        TicketId = child.Ticket.Id,
                        Title = child.Ticket.Title,
                        DeliveryStatusFrom = child.DeliveryStatus?.ToString() ?? "N/A",
                        AssignedFrom = child.AssignedTo?.UserName ?? "N/A",
                        StageFrom = child.StageAfterChange.ToString(),
                        DeliveryStatusTo = "N/A",
                        AssignedTo = "N/A",
                        StageTo = "N/A"
                    });
                }
            }

            return Result<DataTablesResponse<TicketHistoryReportDto>>.Success(new DataTablesResponse<TicketHistoryReportDto>
            {
                Draw = request.Draw,
                RecordsTotal = recordsTotal,
                RecordsFiltered = recordsFiltered,
                Data = dtos
            });
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