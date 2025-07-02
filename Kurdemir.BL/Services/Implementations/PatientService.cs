
using Kurdemir.BL.Helpers.Exceptions;
using Kurdemir.BL.Services.Abstractions;
using Kurdemir.BL.ViewModels.AccountVMs;
using Kurdemir.BL.ViewModels.PatientVMs;
using Kurdemir.Core.Enums;
using Kurdemir.Core.Models;
using Kurdemir.DAL.Repositories.Abstractions;

namespace Kurdemir.BL.Services.Implementations;

public class PatientService(IPatientRepository patientRepository) :IPatientService
{

    readonly IPatientRepository _patientRepository = patientRepository;

    public async Task PatientCreateAsync(PatientCreate patientVm)
    {
        if (!Enum.IsDefined(typeof(Genders), patientVm.Gender))
        {
            throw new Exception404();
        }
        Patient patient = new Patient()
        {
            AppUserId = patientVm.UserId,
            Firstname = patientVm.Firstname,
            Lastname = patientVm.Lastname,
            Gender = (Genders)patientVm.Gender,
        };
        await _patientRepository.CreateAsync(patient);
        await _patientRepository.SaveChangeAsync();
    }
}
