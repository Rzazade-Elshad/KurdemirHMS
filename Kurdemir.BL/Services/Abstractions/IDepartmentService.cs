using Kurdemir.BL.ViewModels.DepartmetVMs;
using Kurdemir.Core.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kurdemir.BL.Services.Abstractions;

public interface IDepartmentService
{
    Task DepartmentCreateAsync(DepartmentCreateVm departmantCreateVm);
    Task DepartmantUpdateAsync(DepartmentUpdateReadVm departmentUpdateVm);
    Task<DepartmentUpdateReadVm> GetDepartment(int id);
    Task<List<DepartmentUpdateReadVm>> GetAll();
    Task Delete(DepartmentUpdateReadVm departmentVm);
    Task IsExsist(int id);
    Task<List<SelectListItem>> DepartmentSelectListItem();
}
