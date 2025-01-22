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


    public async Task<Result<DataTablesResponse<TicketHistoryReportDto>>> GetAllTicketHistoryForReportAsync(DataTablesRequest request, string stageFilter = null, string deliveryStatusFilter = null)
    {
        try
        {
            // Base query with required includes
            var query = _unitOfWork.Tickets.GetAllAsQueryable()
                .Include(t => t.AssignedTo) // Include AssignedTo for Ticket
                .Include(t => t.TicketHistories) // Include TicketHistories
                    .ThenInclude(h => h.AssignedTo) // Include AssignedTo for TicketHistory
                .Include(t => t.TicketHistories)
                    .ThenInclude(h => h.User) // Include User for TicketHistory
                .Include(t => t.TicketHistories)
                    .ThenInclude(h => h.Parent) // Include Parent for TicketHistory
                .AsNoTracking();

            // Apply global search using WhereIf
            if (!string.IsNullOrEmpty(request.Search?.Value))
            {
                var searchValue = request.Search.Value.ToLower();
                query = query.Where(t =>
                    t.AssignedTo != null && t.AssignedTo.UserName.ToLower().Contains(searchValue) ||
                    t.TicketHistories.Any(h => h.User.UserName.ToLower().Contains(searchValue))
                );
            }

            if (string.IsNullOrEmpty(stageFilter) && string.IsNullOrEmpty(deliveryStatusFilter))
            {
                // No filters applied
                query = query;
            }
            else
            {
                // Parse the stageFilter and deliveryStatusFilter if provided
                if (!string.IsNullOrEmpty(stageFilter))
                {
                    Enum.TryParse(stageFilter, true, out Stage stageEnum);
                    query = query.Where(t => t.TicketHistories.Any(h =>
                        h.StageAfterChange == stageEnum || h.StageBeforeChange == stageEnum));
                }

                if (!string.IsNullOrEmpty(deliveryStatusFilter))
                {
                    Enum.TryParse(deliveryStatusFilter, true, out DeliveryStatus deliveryStatusEnum);
                    query = query.Where(t => t.TicketHistories.Where(c => c.HistoryType.Equals(HistoryType.Assignment))
                    .OrderByDescending(h => h.Date)
                    .FirstOrDefault().DeliveryStatus == deliveryStatusEnum
                    );
                }
            }



            // Get total count before applying filters
            var recordsTotal = await query.CountAsync();

            // Select latest history for each ticket
            var ticketHistoriesWithLatest = query.Select(ticket => new
            {
                Ticket = ticket,
                LatestHistory = ticket.TicketHistories
                    .Where(c => c.HistoryType.Equals(HistoryType.Assignment))
                    .OrderByDescending(h => h.Date)
                    .Select(h => new
                    {
                        History = h,
                        AssignedTo = h.AssignedTo, // Include AssignedTo navigation property
                        User = h.User, // Include User navigation property
                        Parent = h.Parent // Include Parent navigation property
                    })
                    .FirstOrDefault() // Get the latest history for the ticket
            });

            // Apply pagination
            var paginatedData = await ticketHistoriesWithLatest
                .Skip(request.Start)
                .Take(request.Length)
                .ToListAsync();

            // Map to DTOs
            var ticketHistoryReportDtos = paginatedData.Select(x =>
            {
                var ticket = x.Ticket;
                var lastHistory = x.LatestHistory?.History; // Access the History property
                var parentHistory = lastHistory?.Parent; // Access the Parent property

                if (lastHistory == null)
                {
                    return new TicketHistoryReportDto
                    {
                        TicketId = ticket.Id,
                        Title = ticket.Title,
                    };
                }

                if (lastHistory.StageAfterChange == Stage.Stage2)
                {
                    if (x.LatestHistory.AssignedTo == null)
                    {
                        return new TicketHistoryReportDto
                        {
                            TicketId = ticket.Id,
                            Title = ticket.Title,
                            DeliveryStatusFrom = parentHistory?.DeliveryStatus.ToString(),
                            AssignedFrom = x.LatestHistory.User.UserName,
                            StageFrom = parentHistory?.StageAfterChange.ToString(),
                            AssignedTo = "Not Set Yet.",
                            DeliveryStatusTo = "Not Set Yet.",
                            StageTo = lastHistory.StageAfterChange.ToString(),
                        };
                    }
                    else
                    {
                        return new TicketHistoryReportDto
                        {
                            TicketId = ticket.Id,
                            Title = ticket.Title,
                            DeliveryStatusFrom = parentHistory?.DeliveryStatus.ToString(),
                            AssignedFrom = x.LatestHistory.User.UserName,
                            StageFrom = parentHistory?.StageAfterChange.ToString(),
                            AssignedTo = x.LatestHistory.AssignedTo?.UserName,
                            DeliveryStatusTo = lastHistory.DeliveryStatus.HasValue ? lastHistory.DeliveryStatus.ToString() : "Pending...",
                            StageTo = lastHistory.StageAfterChange.ToString(),
                        };
                    }
                }
                else
                {
                    if (parentHistory == null)
                    {
                        if (!ticket.Stage.Equals(Stage.NoStage))
                        {
                            return new TicketHistoryReportDto
                            {
                                TicketId = ticket.Id,
                                Title = ticket.Title,
                                DeliveryStatusFrom = lastHistory.AssignedTo != null ? lastHistory.DeliveryStatus.HasValue ? lastHistory.DeliveryStatus.ToString() : "Pending..." : "Within user to take the ticket",
                                AssignedFrom = lastHistory.AssignedTo != null ? lastHistory.AssignedTo?.UserName : "Not Set Yet.",
                                StageFrom = lastHistory.StageBeforeChange.ToString(),
                                AssignedTo = "Not Set Yet.",
                                DeliveryStatusTo = "Not Set Yet.",
                                StageTo = "Not Set Yet.",
                            };
                        }
                        else
                        {
                            return new TicketHistoryReportDto
                            {
                                TicketId = ticket.Id,
                                Title = ticket.Title,
                                DeliveryStatusFrom = "The Ticket is finished.",
                                AssignedFrom = lastHistory.AssignedTo != null ? lastHistory.AssignedTo?.UserName : "Not Set Yet.",
                                StageFrom = lastHistory.StageBeforeChange.ToString(),
                                AssignedTo = "Not Set Yet.",
                                DeliveryStatusTo = "Not Set Yet.",
                                StageTo = "Not Set Yet.",
                            };
                        }
                    }
                    return new TicketHistoryReportDto
                    {
                        TicketId = ticket.Id,
                        Title = ticket.Title,
                        DeliveryStatusFrom = lastHistory.DeliveryStatus.HasValue ? lastHistory.DeliveryStatus.ToString() : "Pending...",
                        AssignedFrom = x.LatestHistory?.AssignedTo?.UserName,
                        StageFrom = lastHistory.StageAfterChange.ToString(),
                        AssignedTo = x.LatestHistory?.User.UserName,
                        DeliveryStatusTo = !ticket.Stage.Equals(Stage.NoStage) ? "Waiting step one to finish." : parentHistory.DeliveryStatus.ToString(),
                        StageTo = parentHistory?.StageAfterChange.ToString(),
                    };
                }
            }).ToList();

            // Prepare response
            var response = new DataTablesResponse<TicketHistoryReportDto>
            {
                Draw = request.Draw,
                RecordsTotal = recordsTotal,
                RecordsFiltered = recordsTotal, // Update if filtering logic is added
                Data = ticketHistoryReportDtos
            };

            return Result<DataTablesResponse<TicketHistoryReportDto>>.Success(response);
        }
        catch (Exception ex)
        {
            return Result<DataTablesResponse<TicketHistoryReportDto>>.Failure($"Something went wrong: {ex.Message}");
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
    public string? DeliveryStatusFrom { get; set; } = "Not Set Yet.";
    public string? AssignedFrom { get; set; } = "Not Set Yet.";
    public string? StageFrom { get; set; } = "Not Set Yet.";
    public string? DeliveryStatusTo { get; set; } = "Not Set Yet.";
    public string? AssignedTo { get; set; } = "Not Set Yet.";
    public string? StageTo { get; set; } = "Not Set Yet.";
}