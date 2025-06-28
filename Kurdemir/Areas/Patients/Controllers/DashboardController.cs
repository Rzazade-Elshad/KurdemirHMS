using Microsoft.AspNetCore.Mvc;

namespace Kurdemir.MVC.Areas.Patients.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
