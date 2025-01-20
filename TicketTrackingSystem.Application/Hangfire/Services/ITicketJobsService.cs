namespace TicketTrackingSystem.Application.Hangfire.Services;
public interface ITicketJobsService
{
    void ConfigureRecurringJobs();
    Task CheckOverdueTickets();
}
