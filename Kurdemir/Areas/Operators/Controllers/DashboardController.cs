using Microsoft.AspNetCore.Mvc;

namespace Kurdemir.MVC.Areas.Operators.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
