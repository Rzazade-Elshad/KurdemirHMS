using Kurdemir.BL.ViewModels.AppointmentVMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kurdemir.BL.Services.Abstractions;

public interface IAppointmentService
{
    Task CreateAsync(AppointmentCreateVm createVm);
    Task<List<AppointmentReadVm>> GetAllAsync();
    Task<AppointmentReadVm> GetAppointmentAsync(int id);
    Task isExist(int id);
    Task Delete(int id);
    Task Update(AppointmentUpdate updatevm);
    Task<List<DateTime>> GetPossibleDatetime(int DoctorId, int PatienId);
    Task<List<AppointmentReadVm>> GetPatientAppointments(string id);
    Task<List<AppointmentReadVm>> GetDoctorAppointments(string id);
}
