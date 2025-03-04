using Microsoft.AspNetCore.Mvc;

namespace TaskManagementSystem.MVC.Areas.Admin.Controllers;
[Area(nameof(Admin))]
//[Authorize(Roles = nameof(Admin))]
public class DashboardController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
