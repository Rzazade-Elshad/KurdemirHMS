using Kurdemir.BL.ViewModels.DoctorVMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kurdemir.BL.Services.Abstractions;

public interface IDoctorService
{
    Task DoctorCreate(DoctorCreateVm doctorCreate);
    Task<List<DoctorReadVm>> DoctorGetAll();
    Task<DoctorUpdateVm> DoctorGet(int id);
    Task Delete(int id);
    Task IsExist(int id);
    Task DoctorUpdate(DoctorUpdateVm updateVm);
}
