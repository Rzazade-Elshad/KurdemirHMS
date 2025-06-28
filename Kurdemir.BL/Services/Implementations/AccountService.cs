using Kurdemir.BL.Helpers.Exceptions;
using Kurdemir.BL.Services.Abstractions;
using Kurdemir.BL.ViewModels.AccountVMs;
using Kurdemir.Core.Enums;
using Kurdemir.Core.Models;
using Kurdemir.DAL.Repositories.Abstractions;
namespace Kurdemir.BL.Services.Implementations;

public class AccountService(IAppUserRepository appUserRepo, IPatientRepository patientRepository) :IAccountService
{
    readonly IAppUserRepository _appUserRepo=appUserRepo;
    readonly IPatientRepository _patientRepository = patientRepository;

    public async Task<string> RegisterAsync(RegisterPatientVm registerVm)
    {
        AppUser appUser = new AppUser()
        {
            UserName = registerVm.Username,
            Email = registerVm.Email,
            RoleName = "Patient"
        };
        var Result = await _appUserRepo.CreateAsync(appUser, registerVm.Password);
        if (!Result.Succeeded)
        {
            string errors = string.Empty;
            foreach (var error in Result.Errors)
            {
                errors += error.Description + "\n";
            }
            return errors;
        }
        await _appUserRepo.AddtoRoleAsync(appUser, UserRoles.Patient.ToString());
        registerVm.UserId = appUser.Id;
        return "Succeeded";
    }
    public async Task PatientCreateAsync(RegisterPatientVm patientVm)
    {
        if (!Enum.IsDefined(typeof(UserRoles), patientVm.Gender))
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
    public async Task<string> LoginAsync(LoginVm loginVm)
    {
        AppUser? appUser = await _appUserRepo.FindByEmailAsync(loginVm.EmailOrUsername) ??
            await _appUserRepo.FindByNameAsync(loginVm.EmailOrUsername);
        if (appUser == null)
        {
            return "Username or Password is false";
        }
        var result = await _appUserRepo.CheckPasswordSignAsync(appUser, loginVm.Password, loginVm.Remember);
        if (!result.Succeeded)
        {
            return "Username or Password is false";
        }
        await _appUserRepo.SignInAsync(appUser, loginVm.Remember);
        return "Succeeded";
    }
    public async Task Logout()
    {
        await _appUserRepo.SignOutAsync();
    }
}
