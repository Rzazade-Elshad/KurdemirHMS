using Kurdemir.BL.Services.Abstractions;
using Kurdemir.BL.ViewModels.DoctorVMs;
using Microsoft.AspNetCore.Mvc;

namespace Kurdemir.MVC.Controllers
{
    public class HomeController(IDoctorService doctorService) : Controller
    {
        readonly IDoctorService _doctorservice=doctorService;
        public async Task<IActionResult> Index()
        {
            List<DoctorReadVm> doctorReads = await _doctorservice.DoctorGetAll();

            // Əgər 4-dən az həkim varsa, hamısını götür
            int countToTake = Math.Min(4, doctorReads.Count);

            // Random 4 həkim seç
            Random rnd = new Random();
            List<DoctorReadVm> doctorReadVms = doctorReads
                .OrderBy(x => rnd.Next())
                .Take(countToTake)
                .ToList();

            return View(doctorReadVms);
        }
        public IActionResult View404()
        {
            return View();
        }
        public IActionResult About()
        {
            return View();
        }
    }
}
