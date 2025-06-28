using Microsoft.AspNetCore.Mvc;

namespace Kurdemir.MVC.Areas.Admin.Controllers;

    [Area("Admin")]
public class DashboardController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
    public IActionResult View404()
    {
        return View();
    }
}
