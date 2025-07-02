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
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Kurdemir.BL.Services.Implementations;

public class DoctorService(IDoctorRepository doctorRepository) : IDoctorService
{
    readonly IDoctorRepository _doctorRepository = doctorRepository;
    public async Task DoctorCreate(DoctorCreateVm doctorCreate)
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

            AppUserId = doctorCreate.UserId,

            DepartmentId = doctorCreate.DepartmentId,

        };
        await _doctorRepository.CreateAsync(doctor);
        await _doctorRepository.SaveChangeAsync();
    }
    public async Task<List<DoctorReadVm>> DoctorGetAll()
    {
        List<Doctor> doctors = await _doctorRepository.GetAllAsync();
        List<DoctorReadVm> doctorReads = doctors.Select(o => new DoctorReadVm()
        {
            Id = o.Id,
            ImgUrl = o.ImageUrl,
            Firstname = o.Firstname,
            Lastname = o.Lastname,
            Gender = o.Gender.ToString(),

            UserId = o.AppUserId,
            Username = o.AppUser.UserName,
            Email = o.AppUser.Email,
            IsDelete=o.AppUser.IsDeleted,
            DeleteTime = o.AppUser.DeleteTime,

            DepartmentName=o.Department.Name,

        }).ToList();
        return doctorReads;
    }
    public async Task<DoctorUpdateVm> DoctorGet(int id)
    {
        Doctor? doctor = await _doctorRepository.GetByIdAsync(id);
        DoctorUpdateVm doctorUpdateVm = new DoctorUpdateVm()
        {
            Id = id,
            Firstname = doctor.Firstname,
            Lastname = doctor.Lastname,
            ImgUrl = doctor.ImageUrl,
            Gender=(int)doctor.Gender,

            UserId = doctor.AppUserId,
            Uername=doctor.AppUser.UserName,
            Email=doctor.AppUser.Email,

            Departments=new(),
            DepartmentId=doctor.Department.Id,

        };
        return doctorUpdateVm;

    }
    public async Task DoctorUpdate(DoctorUpdateVm updateVm)
    {
        Doctor doctor = new Doctor()
        {
            Id = updateVm.Id,
            Firstname=updateVm.Firstname,
            Lastname=updateVm.Lastname,
            ImageUrl=updateVm.ImgUrl,
            Gender=(Genders)updateVm.Gender,
            AppUserId=updateVm.UserId,
            DepartmentId=updateVm.DepartmentId,
        };
        _doctorRepository.Update(doctor);
       await _doctorRepository.SaveChangeAsync();

    }
    public async Task IsExist(int id)
    {
        Doctor? doctor=await _doctorRepository.GetByIdAsync(id);
        if(doctor == null)
        {
            throw new Exception404();
        }
    }
    public async Task Delete(int id)
    {
        Doctor? doctor = await _doctorRepository.GetByIdAsync(id);
        _doctorRepository.Delete(doctor);
        await _doctorRepository.SaveChangeAsync();
    }
}
