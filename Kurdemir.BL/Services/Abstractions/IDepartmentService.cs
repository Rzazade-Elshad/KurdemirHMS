using Kurdemir.BL.ViewModels.DepartmetVMs;
using Kurdemir.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kurdemir.BL.Services.Abstractions;

public interface IDepartmentService
{
    Task DepartmentCreateAsync(DepartmentCreateVm departmantCreateVm);
    Task DepartmantUpdateAsync(DepartmentUpdateCreateVm departmentUpdateVm);
    Task<DepartmentUpdateCreateVm> GetDepartment(int id);
    Task<ICollection<DepartmentUpdateCreateVm>> GetAll();
    Task SoftDelete(DepartmentUpdateCreateVm departmentVm);
    Task Delete(DepartmentUpdateCreateVm departmentVm);
    Task IsExsist(int id);
}
