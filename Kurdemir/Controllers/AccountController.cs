using AspNetCoreGeneratedDocument;
using Kurdemir.BL.ExternalServices.Abstractions;
using Kurdemir.BL.Helpers;
using Kurdemir.BL.Services.Abstractions;
using Kurdemir.BL.ViewModels.AccountVMs;
using Kurdemir.BL.ViewModels.MixViewModels;
using Kurdemir.BL.ViewModels.PatientVMs;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Runtime.CompilerServices;
using static System.Net.WebRequestMethods;

namespace Kurdemir.MVC.Controllers
{
    public class AccountController(IAccountService accountService ,IPatientService patientService,IEmailService _emailService) : Controller
    {
        readonly IAccountService _accountService = accountService;
        readonly IPatientService _patientService = patientService;

        [HttpGet]
        public IActionResult Register()
        {
            return View(new PatientRegisterVm());
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
                return View(registerPatientVm);
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

            return RedirectToAction(nameof(OTPCHECK), new { email = registerVm.Email });
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
        [HttpGet]
        public async Task<IActionResult> OTPCheck(string email)
        {
            Random random = new Random();
            int a = random.Next(100000, 999999);
            _emailService.SendEmailConfirmation(email,"KURDEMIR HOSPITAL",a);
            CookieOptions options = new CookieOptions
            {
                Expires = DateTimeOffset.Now.AddMinutes(5),
                HttpOnly = true
            };

            Response.Cookies.Append("OTP", a.ToString(), options);
            return View();
        }
        [HttpPost]
        public IActionResult OTPCheck(OTPCHECK? oTPCHECK)
        {
            Request.Cookies.TryGetValue("OTP", out string? otpValue);
             int.TryParse(otpValue, out int security);
            if (otpValue == null )
            {
                ModelState.AddModelError("OTP","Dogrulama kodu yanlisdir ve ya muddeti bitib");
            }
            if (security!= oTPCHECK.OTP)
            {
                ModelState.AddModelError("OTP", "Dogrulama kodu yanlisdir ve ya muddeti bitib");
            }
            if (!ModelState.IsValid)
            {
                return View();
            }
          



            return RedirectToAction("Login");
        }


        public IActionResult Logout()
        {
            _accountService.Logout();
            return RedirectToAction("index", "home");
        }
    }
}
