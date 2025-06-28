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
    public async Task DepartmantUpdateAsync(DepartmentUpdateCreateVm departmentUpdateVm )
    {
        Department department = new Department() 
        {
            Id= departmentUpdateVm.Id,
            Name= departmentUpdateVm.Name,
            IsDeleted= departmentUpdateVm.IsDeleted,
            CreateTime=departmentUpdateVm.Createtime,
        };
        _departmentRepo.Update(department);
        await _departmentRepo.SaveChangeAsync();
    }
    public async Task<DepartmentUpdateCreateVm> GetDepartment(int id)
    {
        Department? department=await _departmentRepo.GetByIdAsync(id);
        DepartmentUpdateCreateVm departmentGet=new DepartmentUpdateCreateVm()
        {
            Id=department.Id,
            Name= department.Name,
            IsDeleted= department.IsDeleted,
            Createtime=department.CreateTime,
        };
        return departmentGet;
    }
    public async Task<ICollection<DepartmentUpdateCreateVm>> GetAll()
    {
        List<Department> departments= await _departmentRepo.GetAllDepartmentsAsync();
        List<DepartmentUpdateCreateVm> departmentsGetAll=departments.Select(d=> new DepartmentUpdateCreateVm
        {
            Id=d.Id,
            Name= d.Name,
            IsDeleted= d.IsDeleted,
            Createtime = d.CreateTime,
        }).ToList();
        return departmentsGetAll;
    }
    public async Task SoftDelete(DepartmentUpdateCreateVm departmentVm)
    {
        Department department=new Department()
        {
            Id=departmentVm.Id,
            Name= departmentVm.Name,
            IsDeleted= departmentVm.IsDeleted,
            CreateTime=departmentVm.Createtime,
            DeleteTime = DateTime.UtcNow.AddHours(4),
        };
        _departmentRepo.SoftDelete(department);
        _departmentRepo.SaveChangeAsync();
    }
    public async Task Delete(DepartmentUpdateCreateVm departmentVm)
    {
        Department department = new Department()
        {
            Id = departmentVm.Id,
            Name = departmentVm.Name,
            IsDeleted = departmentVm.IsDeleted,
            CreateTime = departmentVm.Createtime,
        };
        _departmentRepo.Delete(department);
        _departmentRepo.SaveChangeAsync();
    }
    public async Task IsExsist(int id)
    {
        bool isexsist = await _departmentRepo.isExsist(id);
       if (! isexsist)
        {
            throw new Exception404();
        }
    }
    public async Task<List<SelectListItem>> DepartmentSelectListItem(List<SelectListItem> selectLists)
    {
        selectLists.Clear();
        List<Department> departments =await _departmentRepo.GetAllDepartmentsAsync();
        selectLists = departments.Select(x => new SelectListItem()
        {
            Value = x.Id.ToString(),
            Text = x.Name,
        }).ToList();
        return selectLists;
    }
}
