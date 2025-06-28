using Microsoft.AspNetCore.Mvc;

namespace Kurdemir.MVC.Controllers
{
    public class HomeController : Controller
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
}
