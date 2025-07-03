using Microsoft.AspNetCore.Mvc;

namespace Kurdemir.MVC.Areas.Doctors.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
