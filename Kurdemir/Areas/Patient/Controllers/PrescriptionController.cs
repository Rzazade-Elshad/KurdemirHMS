using Kurdemir.BL.Services.Abstractions;
using Kurdemir.BL.ViewModels.PatientFileVMs;
using Kurdemir.Core.Enums;
using Kurdemir.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Kurdemir.MVC.Areas.Patient.Controllers;


[Area("Patient"), Authorize(Roles = nameof(UserRoles.Patient))]

public class PrescriptionController(IPatientService patientService,
    IPatientFileService patientFileService ,
    UserManager<AppUser > userManager
    ) : Controller
{
    readonly IPatientFileService _patientFileService= patientFileService;
    readonly IPatientService _patientService= patientService;
    readonly UserManager<AppUser> _userManager= userManager;

    [HttpGet]

    public async Task<IActionResult> GetFiles()
    {
        string? userdId = _userManager.GetUserId(User);
        if (userdId == null)
        {
            return RedirectToAction("Login", "Account", new { area = "" });

        }
        int patientId = await _patientService.GetIdByUserId(userdId);
        try
        {
            await _patientService.IsExist(patientId);
        }
        catch (Exception)
        {

            return RedirectToAction("view404", "home", new { area = " " });
        }
        List<PatientFileRead> patientFileReads = await _patientFileService.GetPatientFile(patientId);
        return View(patientFileReads);
    }
}
