using Hangfire;
using Microsoft.EntityFrameworkCore;
using TicketTrackingSystem.Core.Model.Enum;
using TicketTrackingSystem.DAL.Interface;

namespace TicketTrackingSystem.Application.Hangfire.Services;
public class TicketJobsService : ITicketJobsService
{
    private readonly IUnitOfWork _unitOfWork;

    public TicketJobsService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task CheckOverdueTickets()
    {
        var tickets = await _unitOfWork.TicketHistory.GetAllAsQueryable()
                                            .Where(p => p.HistoryType.Equals(HistoryType.Assignment) && p.EstimatedCompletionDate <= DateTime.Now && !p.DeliveryStatus.HasValue)
                                            .Select(p => p.Ticket).ToListAsync();
        foreach (var ticket in tickets)
        {
            await _unitOfWork.Tickets.SetLateTicketsAsync(ticket.Id);
        }
    }

    public async void ConfigureRecurringJobs()
    {
        RecurringJob.AddOrUpdate(
            "check-overdue-tickets",
                            () => CheckOverdueTickets(),
                            Cron.Minutely());
    }
}
