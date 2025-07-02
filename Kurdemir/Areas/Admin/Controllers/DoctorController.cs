using Kurdemir.BL.Services.Abstractions;
using Kurdemir.BL.ViewModels.DoctorVMs;
using Kurdemir.BL.ViewModels.MixViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Kurdemir.MVC.Areas.Admin.Controllers;
[Area("Admin")]
public class DoctorController(IDoctorService doctorService, IDepartmentService departmentService) : Controller
{
    readonly IDoctorService _doctorService=doctorService;
    readonly IDepartmentService _departmentService=departmentService;

    public async Task<IActionResult> Read()
    {
        List<DoctorReadVm> doctors=await _doctorService.DoctorGetAll();
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
    public async Task<IActionResult> Create(DoctorCreateVm vm)
    {
        return RedirectToAction("read");
    }

}
