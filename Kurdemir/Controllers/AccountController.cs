using AspNetCoreGeneratedDocument;
using Kurdemir.BL.Services.Abstractions;
using Kurdemir.BL.ViewModels.AccountVMs;
using Kurdemir.BL.ViewModels.MixViewModels;
using Kurdemir.BL.ViewModels.PatientVMs;
using Microsoft.AspNetCore.Mvc;

namespace Kurdemir.MVC.Controllers
{
    public class AccountController(IAccountService accountService ,IPatientService patientService) : Controller
    {
        readonly IAccountService _accountService = accountService;
        readonly IPatientService _patientService = patientService;

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(PatientRegisterVm registerPatientVm)
        {
            if (!ModelState.IsValid)
            {
                return View(registerPatientVm);
            }
            RegisterVm registerVm = new RegisterVm()
            {
                Email = registerPatientVm.Email,
                Username = registerPatientVm.Username,
                Password = registerPatientVm.Password,
            };
            string Result= string.Empty;
            try
            {
            Result=await _accountService.RegisterAsync(registerVm,2);

            }
            catch (Exception)
            {
                return RedirectToAction("View404", "home");
            }

            if(Result != "Succeeded")
            {
                ModelState.AddModelError(string.Empty, Result);
                return View(registerVm);
            }

            PatientCreate patient = new PatientCreate()
            {
                Firstname =registerPatientVm.Firstname,
                Lastname =registerPatientVm.Lastname,
                Gender =registerPatientVm.Gender,
                UserId=registerVm.UserId,
            };
            try
            {
                await _patientService.PatientCreateAsync(patient);
            }
            catch (Exception ex)
            {
                return RedirectToAction("View404","home");
            }
            return RedirectToAction(nameof(Index), "home");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login( LoginVm  loginVm)
        {
            if (!ModelState.IsValid)
            {
                return View(loginVm);
            }
            string Result= await _accountService.LoginAsync(loginVm);
            if(Result != "Succeeded")
            {
                ModelState.AddModelError(string.Empty,Result);
                return View(loginVm);
            }
            return RedirectToAction(nameof(Index),"home");

        }

        public IActionResult Logout()
        {
            _accountService.Logout();
            return RedirectToAction("index", "home");
        }
    }
}
