using Kurdemir.BL.Services.Abstractions;
using Kurdemir.BL.ViewModels.DepartmetVMs;
using Kurdemir.Core.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Kurdemir.MVC.Areas.Admin.Controllers;

[Area("Admin"), Authorize(Roles = nameof(UserRoles.Admin))]
public class DepartmentController(IDepartmentService departmentService) : Controller
{
    readonly IDepartmentService _departmentService= departmentService;

    public async Task<IActionResult> Read()
    {
        List<DepartmentUpdateReadVm> departments=await _departmentService.GetAll();
        return View(departments);
    }
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(DepartmentCreateVm createVm)
    {
        if(! ModelState.IsValid)
        {
            return View(createVm);
        }
        await _departmentService.DepartmentCreateAsync(createVm);
        return RedirectToAction("Read");
    }
    [HttpGet]
    public async Task<IActionResult> Update(int id)
    {
        try
        {
           await _departmentService.IsExsist(id);
        }
        catch (Exception)
        {
           return RedirectToAction("View404", "Dashboard");
        }
        DepartmentUpdateReadVm updateVm=await _departmentService.GetDepartment(id);
        return View(updateVm);
    }
    [HttpPost]
    public async Task<IActionResult> Update(DepartmentUpdateReadVm updateVm)
    {
        if(!ModelState.IsValid)
        {
            return View(updateVm);
        }
        try
        {
            await _departmentService.IsExsist(updateVm.Id);
        }
        catch (Exception)
        {

          return  RedirectToAction("View404", "Dashboard");
        }
        await _departmentService.DepartmantUpdateAsync(updateVm);
        return RedirectToAction("Read");
    }
    [HttpGet]
    public async Task<IActionResult> Delete(int id) 
    {
        try
        {
            await _departmentService.IsExsist(id);
        }
        catch (Exception)
        {
         return  RedirectToAction("View404", "Dashboard");
        }
        DepartmentUpdateReadVm readVm = await _departmentService.GetDepartment(id);

        await _departmentService.Delete(readVm);

        return RedirectToAction("Read");
    }
}
