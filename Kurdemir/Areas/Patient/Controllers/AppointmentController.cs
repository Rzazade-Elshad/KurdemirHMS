using Kurdemir.BL.Services.Abstractions;
using Kurdemir.BL.ViewModels.AppointmentVMs;
using Kurdemir.BL.ViewModels.DoctorVMs;
using Kurdemir.Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Kurdemir.MVC.Areas.Patients.Controllers;

[Area("Patient")]
public class AppointmentController(IAppointmentService appointmentService,
    IDoctorService doctorService,
    IPatientService patientService,
    UserManager<AppUser> userManager) : Controller
{
    readonly IAppointmentService _appointmentService=appointmentService;
    readonly IDoctorService _doctorService=doctorService;
    readonly IPatientService _patientService=patientService;
    readonly UserManager<AppUser> _userManager=userManager;

    public async Task<IActionResult> Index()
    {
        List<DoctorReadVm> doctorReads=await _doctorService.DoctorGetAll();
        List<DoctorReadVm> doctors=doctorReads.Where(d=>d.IsDelete==false).ToList();
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
            RedirectToAction("view404", "home", new {area=" "});
        }
        string? userdId = _userManager.GetUserId(User);
        if(userdId==null)
        {
            return RedirectToAction("Login", "Account", new { area = "" });

        }
        int patientId=await _patientService.GetIdByUserId(userdId);
        AppointmentCreateVm appointmentCreateVm = new AppointmentCreateVm()
        {
            DoctorId = id,
            PatientId=patientId,
            DateTimes =await _appointmentService.GetPossibleDatetime(id,patientId),
        };
        return View(appointmentCreateVm);
    }
    [HttpPost]
    public async Task<IActionResult> Booking(AppointmentCreateVm appointmentCreateVm)
    {
        List<DateTime> PossibleDatetime= await _appointmentService.GetPossibleDatetime(appointmentCreateVm.DoctorId,appointmentCreateVm.PatientId);
        if(!ModelState.IsValid)
        {
            appointmentCreateVm.DateTimes=PossibleDatetime;
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



}
