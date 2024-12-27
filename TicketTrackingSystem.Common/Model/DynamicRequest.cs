namespace TicketTrackingSystem.Common.Model;
public class DynamicRequest
{
    public string Method { get; set; }
    public object[] Parameters { get; set; }
}
