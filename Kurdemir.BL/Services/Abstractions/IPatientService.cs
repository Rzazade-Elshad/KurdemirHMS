
using Kurdemir.BL.ViewModels.PatientVMs;

namespace Kurdemir.BL.Services.Abstractions;

public interface IPatientService 
{
    Task PatientCreateAsync(PatientCreate patientVm);
}
