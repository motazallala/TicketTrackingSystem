using Microsoft.AspNetCore.Mvc;

namespace TicketTrackingSystem.MVC.Areas.Admin.Controllers;
[Area("Admin")]
public class DashboardController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
