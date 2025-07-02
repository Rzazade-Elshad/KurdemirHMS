using Kurdemir.BL.Helpers.Exceptions;
using Kurdemir.BL.Services.Abstractions;
using Kurdemir.BL.ViewModels.DepartmetVMs;
using Kurdemir.Core.Models;
using Kurdemir.DAL.Repositories.Abstractions;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kurdemir.BL.Services.Implementations;

public class DepartmentService(IDepartmentRepository departmentRepository) :IDepartmentService
{
    readonly IDepartmentRepository _departmentRepo =departmentRepository;

    public async Task DepartmentCreateAsync(DepartmentCreateVm departmantCreateVm)
    {
        Department department=new Department()
        {
            Name= departmantCreateVm.Name
        };
        await _departmentRepo.CreateAsync(department);
        await _departmentRepo.SaveChangeAsync();
    }
    public async Task DepartmantUpdateAsync(DepartmentUpdateReadVm departmentUpdateVm )
    {
        Department department = new Department() 
        {
            Id= departmentUpdateVm.Id,
            Name= departmentUpdateVm.Name,
        };
        _departmentRepo.Update(department);
        await _departmentRepo.SaveChangeAsync();
    }
    public async Task<DepartmentUpdateReadVm> GetDepartment(int id)
    {
        Department? department=await _departmentRepo.GetByIdAsync(id);
        DepartmentUpdateReadVm departmentGet=new DepartmentUpdateReadVm()
        {
            Id=department.Id,
            Name= department.Name,
        };
        return departmentGet;
    }
    public async Task<List<DepartmentUpdateReadVm>> GetAll()
    {
        List<Department> departments= await _departmentRepo.GetAllDepartmentsAsync();
        List<DepartmentUpdateReadVm> departmentsGetAll=departments.Select(d=> new DepartmentUpdateReadVm
        {
            Id=d.Id,
            Name= d.Name,
            DoctorCount=d.Doctors.Count,
        }).ToList();
        return departmentsGetAll;
    }
    public async Task Delete(DepartmentUpdateReadVm departmentVm)
    {
        Department department = new Department()
        {
            Id = departmentVm.Id,
            Name = departmentVm.Name,
        };
       _departmentRepo.Delete(department);
      await  _departmentRepo.SaveChangeAsync();
    }
    public async Task IsExsist(int id)
    {
        bool isexsist = await _departmentRepo.isExsist(id);
       if (! isexsist)
        {
            throw new Exception404();
        }
    }
    public async Task<List<SelectListItem>> DepartmentSelectListItem()
    {
        
        List<Department> departments =await _departmentRepo.GetAllDepartmentsAsync();
        List<SelectListItem> selectLists = departments.Select(x => new SelectListItem()
        {
            Value = x.Id.ToString(),
            Text = x.Name,
        }).ToList();
        return selectLists;
    }
}
