using Kurdemir.BL.Helpers.Exceptions;
using Kurdemir.BL.Services.Abstractions;
using Kurdemir.BL.ViewModels.DoctorVMs;
using Kurdemir.Core.Enums;
using Kurdemir.Core.Models;
using Kurdemir.DAL.Repositories.Abstractions;
using Kurdemir.DAL.Repositories.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kurdemir.BL.Services.Implementations;

public class DoctorService(IDoctorRepository doctorRepository , IAppUserRepository appUserRepository) :IDoctorService
{
    readonly IDoctorRepository _doctorRepository=doctorRepository;
    readonly IAppUserRepository _appUserRepository=appUserRepository; 

    public async Task<string> Register(DoctorCreateVm doctorCreate)
    {
        AppUser appUser = new AppUser()
        {
            UserName = doctorCreate.Username,
            Email = doctorCreate.Email,
            RoleName = "Doctor"
        };

        var Result = await _appUserRepository.CreateAsync(appUser, doctorCreate.Password);
        if (!Result.Succeeded)
        {
            string errors = string.Empty;
            foreach (var error in Result.Errors)
            {
                errors += error.Description + "\n";
            }
            return errors;
        }

        await _appUserRepository.AddtoRoleAsync(appUser, UserRoles.Operator.ToString());

        doctorCreate.UserId = appUser.Id;

        return "Succeeded";
    }
    public async Task Create(DoctorCreateVm doctorCreate)
    {
        if (!Enum.IsDefined(typeof(UserRoles), doctorCreate.Gender))
        {
            throw new Exception404();
        }

         Doctor doctor = new Doctor()
        {
            Firstname = doctorCreate.Firstname,
            Lastname = doctorCreate.Lastname,
            Gender = (Genders)doctorCreate.Gender,
            ImageUrl = doctorCreate.ImgUrl,

            AppUserId=doctorCreate.UserId,

            DepartmentId = doctorCreate.DepartmentId,

        };
        await _doctorRepository.CreateAsync(doctor);
        await _doctorRepository.SaveChangeAsync();
    }

}
