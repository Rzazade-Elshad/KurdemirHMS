
using Kurdemir.BL.Helpers.Exceptions;
using Kurdemir.BL.Services.Abstractions;
using Kurdemir.BL.ViewModels.AccountVMs;
using Kurdemir.BL.ViewModels.PatientVMs;
using Kurdemir.Core.Enums;
using Kurdemir.Core.Models;
using Kurdemir.DAL.Repositories.Abstractions;
using Kurdemir.DAL.Repositories.Implementations;

namespace Kurdemir.BL.Services.Implementations;

public class PatientService(IPatientRepository patientRepository) :IPatientService
{

    readonly IPatientRepository _patientRepository = patientRepository;

    public async Task<List<PatientReadVm>> GetAllAsync()
    {
        List<Patient> patients= await _patientRepository.GetAllPatientsAsync();
        List<PatientReadVm> patientReads = patients.Select(x => new PatientReadVm()
        {
            Id = x.Id,
            FirstName = x.Firstname,
            LastName = x.Lastname,
            Email = x.AppUser.Email,
            Username = x.AppUser.UserName,
            Gender = x.Gender.ToString(),
        }).ToList();
        return patientReads;
    }

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


    public async Task<int> GetIdByUserId(string userId)
    {
        return await _patientRepository.GetIdByUserId(userId);
    }
    public async Task IsExist(int id)
    {
        Patient? patient = await _patientRepository.GetByIdAsync(id);
        if (patient == null)
        {
            throw new Exception404();
        }
    }
}
