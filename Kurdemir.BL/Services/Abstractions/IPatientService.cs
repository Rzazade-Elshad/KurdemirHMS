
using Kurdemir.BL.ViewModels.PatientVMs;

namespace Kurdemir.BL.Services.Abstractions;

public interface IPatientService 
{
    Task PatientCreateAsync(PatientCreate patientVm);
    Task<int> GetIdByUserId(string userId);
}
