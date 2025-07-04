using Kurdemir.BL.Helpers.File_Extencions;
using Kurdemir.BL.Services.Abstractions;
using Kurdemir.BL.ViewModels.PatientFileVMs;
using Kurdemir.BL.ViewModels.PatientVMs;
using Kurdemir.Core.Enums;
using Kurdemir.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace Kurdemir.MVC.Areas.Doctor.Controllers
{
    [Area("Doctor"), Authorize(Roles = nameof(UserRoles.Doctor))]

    public class PrescriptionController(IPatientService patientService, IPatientFileService patientFileService, IWebHostEnvironment webHostEnvironment) : Controller
    {
        readonly IPatientFileService _patientFileService = patientFileService;
        readonly IPatientService _patientService = patientService;
        readonly IWebHostEnvironment _webHostEnvironment = webHostEnvironment;
        [HttpGet]
        public async Task<IActionResult> PatientAll()
        {
            List<PatientReadVm> patientReadVms = await _patientService.GetAllAsync();
            return View(patientReadVms);
        }

        [HttpGet]
        public async Task<IActionResult> UploadFile(int id)
        {
            try
            {
                await _patientService.IsExist(id);
            }
            catch (Exception)
            {

                return RedirectToAction("view404", "home", new { area = " " });
            }
            PatientFileUpload patientFileUpload = new PatientFileUpload();
            patientFileUpload.PatientId = id;
            return View(patientFileUpload);
        }
        [HttpPost]
        public async Task<IActionResult> UploadFile(PatientFileUpload patientFileUpload)
        {
            if (!ModelState.IsValid)
            {
                return View(patientFileUpload);
            }
            try
            {
                await _patientService.IsExist(patientFileUpload.PatientId);
            }
            catch (Exception)
            {

                return RedirectToAction("view404", "home", new { area = " " });
            }

            // Fayl yoxlaması – yalnız PDF və maksimum 5MB
            if (patientFileUpload.File == null ||
                patientFileUpload.File.ContentType != "application/pdf" ||
                !patientFileUpload.File.IsValidSize(5030))
            {
                ModelState.AddModelError("File", "File must be a PDF and less than 5MB.");
                return View(patientFileUpload);
            }
            patientFileUpload.FileUrl= await patientFileUpload.File
            .UploadAsync(_webHostEnvironment.WebRootPath, "Upload", "PDF", "Patient");

            // Model yarat
            PatientFile patientFile = new PatientFile
            {
                Title = patientFileUpload.Title,
                FileURL = patientFileUpload.FileUrl,
                PatientId = patientFileUpload.PatientId
            };
            await _patientFileService.Upload(patientFileUpload);
            return RedirectToAction("PatientAll");
        }
        [HttpGet]
        public async Task<IActionResult> GetFiles(int id)
        {
            try
            {
                await _patientService.IsExist(id);
            }
            catch (Exception)
            {

                return RedirectToAction("view404", "home", new { area = " " });
            }
           
            List<PatientFileRead> patientFileReads = await _patientFileService.GetPatientFile(id);
            return View(patientFileReads);
        }
    }
}
