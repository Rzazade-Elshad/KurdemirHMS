using Kurdemir.BL.Services.Abstractions;
using Kurdemir.BL.ViewModels.AppointmentVMs;
using Kurdemir.BL.ViewModels.DoctorVMs;
using Kurdemir.Core.Enums;
using Kurdemir.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Kurdemir.MVC.Areas.Patients.Controllers;
[Area("Patient"), Authorize(Roles = nameof(UserRoles.Patient))]
public class AppointmentController(IAppointmentService appointmentService,
    IDoctorService doctorService,
    IPatientService patientService,
    UserManager<AppUser> userManager) : Controller
{
    readonly IAppointmentService _appointmentService = appointmentService;
    readonly IDoctorService _doctorService = doctorService;
    readonly IPatientService _patientService = patientService;
    readonly UserManager<AppUser> _userManager = userManager;

    public async Task<IActionResult> Index()
    {
        List<DoctorReadVm> doctorReads = await _doctorService.DoctorGetAll();
        List<DoctorReadVm> doctors = doctorReads.Where(d => d.IsDelete == false).ToList();
        return View(doctors);
    }
    [HttpGet]
    public async Task<IActionResult> Booking(int id)
    {
        try
        {
            await _doctorService.IsExist(id);

        }
        catch (Exception)
        {
           return RedirectToAction("view404", "home", new { area = " " });
        }
        string? userdId = _userManager.GetUserId(User);
        if (userdId == null)
        {
            return RedirectToAction("Login", "Account", new { area = "" });

        }
        int patientId = await _patientService.GetIdByUserId(userdId);
        AppointmentCreateVm appointmentCreateVm = new AppointmentCreateVm()
        {
            DoctorId = id,
            PatientId = patientId,
            DateTimes = await _appointmentService.GetPossibleDatetime(id, patientId),
        };
        return View(appointmentCreateVm);
    }
    [HttpPost]
    public async Task<IActionResult> Booking(AppointmentCreateVm appointmentCreateVm)
    {
        List<DateTime> PossibleDatetime = await _appointmentService.GetPossibleDatetime(appointmentCreateVm.DoctorId, appointmentCreateVm.PatientId);
        if (!ModelState.IsValid)
        {
            appointmentCreateVm.DateTimes = PossibleDatetime;
            return View(appointmentCreateVm);
        }
        if (!PossibleDatetime.Contains(appointmentCreateVm.DateTime))
        {
            ModelState.AddModelError("", "Seçdiyiniz vaxt artıq doludur və ya mövcud deyil.");
            appointmentCreateVm.DateTimes = PossibleDatetime;
            return View(appointmentCreateVm);
        }

        await _appointmentService.CreateAsync(appointmentCreateVm);
        return RedirectToAction("Index");
    }
    [HttpGet]
    public async Task<IActionResult> UserAppointmentsHistory()
    {
        string? userdId = _userManager.GetUserId(User);
        if (userdId == null)
        {
            return RedirectToAction("Login", "Account", new { area = "" });

        }
        List<AppointmentReadVm> appointments = await _appointmentService.GetPatientAppointments(userdId);
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
        List<AppointmentReadVm> appointments = await _appointmentService.GetPatientAppointments(userdId);
        List<AppointmentReadVm> NewApps = appointments.Where(x => x.DateTime >= DateTime.Now.AddHours(4)).ToList();
        
        return View(NewApps);
    }
    [HttpGet]
    public async Task<IActionResult> Update(int id)
    {
        try
        {
            await _appointmentService.isExist(id);
        }
        catch (Exception)
        {
           return RedirectToAction("view404", "home", new { area = " " });
        }
        AppointmentReadVm appointmentRead=await _appointmentService.GetAppointmentAsync(id);
        AppointmentUpdate appointmentUpdate=new AppointmentUpdate()
        {
            id= appointmentRead.id,
            PatienId=appointmentRead.PatienId,
            DoctorId=appointmentRead.DoctorId,
            DateTimes= await _appointmentService.GetPossibleDatetime(appointmentRead.DoctorId,appointmentRead.PatienId),
        };

        return View(appointmentUpdate);
    }
    [HttpPost]
    public async Task<IActionResult> Update(AppointmentUpdate appointmentUpdate)
    {
        List<DateTime> PossibleDatetime = await _appointmentService.GetPossibleDatetime(appointmentUpdate.DoctorId, appointmentUpdate.PatienId);
        if (!ModelState.IsValid)
        {
            appointmentUpdate.DateTimes = PossibleDatetime;
            return View(appointmentUpdate);
        }
        if (!PossibleDatetime.Contains(appointmentUpdate.DateTime))
        {
            ModelState.AddModelError("", "Seçdiyiniz vaxt artıq doludur və ya mövcud deyil.");
            appointmentUpdate.DateTimes = PossibleDatetime;
            return View(appointmentUpdate);
        }

        await _appointmentService.Update(appointmentUpdate);

        return RedirectToAction("Index");
    }

}
