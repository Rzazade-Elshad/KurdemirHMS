using Microsoft.AspNetCore.Mvc;

namespace Kurdemir.MVC.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
