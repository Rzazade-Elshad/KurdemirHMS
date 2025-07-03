using Kurdemir.BL.Helpers.File_Extencions;
using Kurdemir.BL.Services.Abstractions;
using Kurdemir.BL.Services.Implementations;
using Kurdemir.BL.ViewModels.AccountVMs;
using Kurdemir.BL.ViewModels.DoctorVMs;
using Kurdemir.BL.ViewModels.MixViewModels;
using Kurdemir.BL.ViewModels.PatientVMs;
using Kurdemir.Core.Models;
using Microsoft.AspNetCore.Mvc;
using System.Numerics;

namespace Kurdemir.MVC.Areas.Admin.Controllers;
[Area("Admin")]
public class DoctorController(IDoctorService doctorService,
    IDepartmentService departmentService,
    IWebHostEnvironment webHost,
    IAccountService accountService) : Controller
{
    readonly IAccountService _accountService = accountService;
    readonly IDoctorService _doctorService = doctorService;
    readonly IDepartmentService _departmentService = departmentService;
    readonly IWebHostEnvironment _webHostEnvironment = webHost;


    public async Task<IActionResult> Read()
    {
        List<DoctorReadVm> doctors = await _doctorService.DoctorGetAll();
        return View(doctors);
    }
    [HttpGet]
    public async Task<IActionResult> Create()
    {
        DoctorRegister doctorRegister = new DoctorRegister()
        {
            Departments = await _departmentService.DepartmentSelectListItem()
        };
        return View(doctorRegister);
    }
    [HttpPost]
    public async Task<IActionResult> Create(DoctorRegister Doctorvm)
    {
        if (!ModelState.IsValid)
        {
            Doctorvm.Departments = await _departmentService.DepartmentSelectListItem();
            return View(Doctorvm);
        }
        if (!Doctorvm.Image.IsValidType("image"))
        {
            ModelState.AddModelError(string.Empty, "File format is wrong");
            Doctorvm.Departments = await _departmentService.DepartmentSelectListItem();
            return View(Doctorvm);
        }
        if (!Doctorvm.Image.IsValidSize(100))
        {
            ModelState.AddModelError(string.Empty, "File size is wrong");
            Doctorvm.Departments = await _departmentService.DepartmentSelectListItem();
            return View(Doctorvm);
        }
        Doctorvm.ImgUrl = await Doctorvm.Image.UploadAsync(_webHostEnvironment.WebRootPath, "Upload", "Image", "Doctor");

        RegisterVm registerVm = new RegisterVm()
        {
            Email = Doctorvm.Email,
            Username = Doctorvm.Username,
            Password = Doctorvm.Password,
        };

        string Result = string.Empty;
        try
        {
            Result = await _accountService.RegisterAsync(registerVm, 1);

        }
        catch (Exception)
        {
            return RedirectToAction("View404", "Dashboard");
        }

        if (Result != "Succeeded")
        {
            ModelState.AddModelError(string.Empty, Result);
            return View(registerVm);
        }
        DoctorCreateVm createVm = new DoctorCreateVm()
        {
            Firstname = Doctorvm.Firstname,
            Lastname = Doctorvm.Lastname,
            DepartmentId = Doctorvm.DepartmentId,
            Gender = Doctorvm.Gender,
            ImgUrl = Doctorvm.ImgUrl,
            UserId = registerVm.UserId,
        };
        try
        {
            await _departmentService.IsExsist(createVm.DepartmentId);
            await _doctorService.DoctorCreate(createVm);
        }
        catch (Exception ex)
        {
            return RedirectToAction("View404", "Dashboard");
        }

        return RedirectToAction("read");
    }

    [HttpGet]
    public async Task<IActionResult> Update(int id)
    {
        try
        {
            await _doctorService.IsExist(id);
        }
        catch (Exception)
        {
            return RedirectToAction("View404", "Dashboard");
        }
        DoctorUpdateVm doctorUpdate = await _doctorService.DoctorGet(id);
        doctorUpdate.Departments = await _departmentService.DepartmentSelectListItem();
        return View(doctorUpdate);

    }
    [HttpPost]
    public async Task<IActionResult> Update(DoctorUpdateVm updateVm)
    {
        if (!ModelState.IsValid)
        {
            updateVm.Departments = await _departmentService.DepartmentSelectListItem();
            return View(updateVm);
        }
        if (updateVm.Image != null)
        {
            if (!updateVm.Image.IsValidType("image"))
            {
                ModelState.AddModelError(string.Empty, "File format is wrong");
                updateVm.Departments = await _departmentService.DepartmentSelectListItem();
                return View(updateVm);
            }
            if (!updateVm.Image.IsValidSize(100))
            {
                ModelState.AddModelError(string.Empty, "File size is wrong");
                updateVm.Departments = await _departmentService.DepartmentSelectListItem();
                return View(updateVm);
            }
            File_Extencion.Delete(_webHostEnvironment.WebRootPath, "Upload", "Image", "Doctor", updateVm.ImgUrl);
            updateVm.ImgUrl = await updateVm.Image.UploadAsync(_webHostEnvironment.WebRootPath, "Upload", "Image", "Doctor");
        }
        UserUpdate userUpdate = new UserUpdate()
        {
            Email = updateVm.Email,
            UserID = updateVm.UserId,
            UserName = updateVm.Uername,
        };
        string errors = string.Empty;
        try
        {
            errors = await _accountService.Update(userUpdate);

        }
        catch (Exception)
        {

            return RedirectToAction("View404", "Dashboard");
        }
        if (errors != "Succeeded")
        {
            ModelState.AddModelError(string.Empty, errors);
            updateVm.Departments = await _departmentService.DepartmentSelectListItem();
            return View(updateVm);
        }
        await _doctorService.DoctorUpdate(updateVm);
        return RedirectToAction("read");
    }

    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _doctorService.IsExist(id);
        }
        catch (Exception)
        {
            return RedirectToAction("View404", "Dashboard");
        }
        await _doctorService.Delete(id);
        return RedirectToAction("read");
    }
    public async Task<IActionResult> SoftDelete(int id)
    {
        DoctorUpdateVm doctor = await _doctorService.DoctorGet(id);
        try
        {
            await _accountService.SoftDeleteAsync(doctor.UserId);
        }
        catch (Exception)
        {
            return RedirectToAction("View404", "Dashboard");
        }
        return RedirectToAction("Read");
    }
}
