using Kurdemir.BL.Services.Abstractions;
using Kurdemir.Core.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Kurdemir.MVC.Areas.Admin.Controllers;
[Area("Admin"), Authorize(Roles = nameof(UserRoles.Admin))]
public class DashboardController(IDoctorService doctorService, IPatientService patientService, IAppointmentService appointmentService) : Controller
{
    public async Task<IActionResult> Index()
    {
       var doctors= await doctorService.DoctorGetAll();
        var patients= await patientService.GetAllAsync();
        var appointments = await appointmentService.GetAllAsync();
        List<int> ints = new List<int>();
        ints.Add( doctors.Count());
        ints.Add( patients.Count());
        ints.Add( appointments.Count());
        return View(ints);
    }
    public IActionResult View404()
    {
        return View();
    }
}
