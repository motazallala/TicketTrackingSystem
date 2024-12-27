using System.Net;

namespace TicketTrackingSystem.Application.HttpResponse;
public class ErrorMessage
{
    public HttpStatusCode Code { get; set; }
    public string Description { get; set; }
}
