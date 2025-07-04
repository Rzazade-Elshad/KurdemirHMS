using Kurdemir.BL.Services.Abstractions;
using Kurdemir.BL.ViewModels.AppointmentVMs;
using Kurdemir.Core.Enums;
using Kurdemir.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Kurdemir.MVC.Areas.Doctor.Controllers;

[Area("Doctor"), Authorize(Roles = nameof(UserRoles.Doctor))]

public class AppointmentController(IAppointmentService appointmentService,
    IDoctorService doctorService,
    UserManager<AppUser > userManager) : Controller
{
    readonly IAppointmentService _appointmentService=appointmentService;
    readonly IDoctorService _doctorService=doctorService;
    readonly UserManager<AppUser> _userManager=userManager;

    [HttpGet]
    public async Task<IActionResult> UserAppointmentsHistory()
    {
        string? userdId = _userManager.GetUserId(User);
        if (userdId == null)
        {
            return RedirectToAction("Login", "Account", new { area = "" });

        }
        List<AppointmentReadVm> appointments = await _appointmentService.GetDoctorAppointments(userdId);
        List<AppointmentReadVm> OldApps = appointments.Where(x => x.DateTime < DateTime.Now.AddHours(4)).ToList();
        return View(OldApps);
    }
    [HttpGet]
    public async Task<IActionResult> UserWaitingAppointments()
    {
        string? userdId = _userManager.GetUserId(User);
        if (userdId == null)
        {
            return RedirectToAction("Login", "Account", new { area = "" });

        }
        List<AppointmentReadVm> appointments = await _appointmentService.GetDoctorAppointments(userdId);
        List<AppointmentReadVm> NewApps = appointments.Where(x => x.DateTime >= DateTime.Now.AddHours(4)).ToList();

        return View(NewApps);
    }
}
