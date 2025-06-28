using Kurdemir.BL.ViewModels.AccountVMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kurdemir.BL.Services.Abstractions;

public interface IAccountService
{
    Task<string> LoginAsync(LoginVm loginVm);
    Task Logout();
    Task<string> RegisterAsync(RegisterPatientVm registerVm, int RoleValue);
    Task PatientCreateAsync(RegisterPatientVm patientVm);
}
