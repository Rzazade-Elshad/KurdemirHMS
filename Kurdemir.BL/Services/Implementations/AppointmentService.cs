using Kurdemir.BL.Helpers.Exceptions;
using Kurdemir.BL.Services.Abstractions;
using Kurdemir.BL.ViewModels.AppointmentVMs;
using Kurdemir.Core.Models;
using Kurdemir.DAL.Repositories.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kurdemir.BL.Services.Implementations;

public class AppointmentService(IAppointmentRepository appointmentRepository) : IAppointmentService
{
    readonly IAppointmentRepository _appointmentRepository = appointmentRepository;

    public async Task<List<DateTime>> GetPossibleDatetime(int DoctorId,int PatienId)
    {

        List<DateTime> result = new();
        DateTime now = DateTime.Now;
        DateTime today = now.Date;

        // Bütün mövcud randevuları öncədən çək
        var appointments = await _appointmentRepository.GetAllAsync();

        for (int i = 0; i < 5; i++)
        {
            DateTime day = today.AddDays(i);

            // İcazəli saat aralıqları (09–13, 14–18)
            var validHours = Enumerable.Range(9, 4).Concat(Enumerable.Range(14, 5));

            foreach (int hour in validHours)
            {
                DateTime slot = day.AddHours(hour);

                // Əgər keçmiş saatdırsa — keç
                if (slot < now)
                    continue;

                // Əgər həkimin və ya pasiyentin həmin saatda artıq randevusu varsa — keç
                bool isTaken = appointments.Any(a =>
                    a.DateTime == slot &&
                    (a.DoctorId == DoctorId || a.PatientId == PatienId));

                if (!isTaken)
                    result.Add(slot);
            }
        }

        return result;
    }

    public async Task CreateAsync(AppointmentCreateVm createVm)
    {
        Appointment appointment = new Appointment()
        {
            DoctorId = createVm.DoctorId,
            PatientId = createVm.PatientId,
            DateTime = createVm.DateTime,
        };
        await _appointmentRepository.CreateAsync(appointment);
        await _appointmentRepository.SaveChangeAsync();

    }

    public async Task<List<AppointmentReadVm>> GetAllAsync()
    {
        List<Appointment> appointments = await _appointmentRepository.GetAllAsync();
        List<AppointmentReadVm> readVm = appointments.Select(x => new AppointmentReadVm()
        {
            id = x.Id,
            DateTime = x.DateTime,
            DoctorFullName = x.Doctor.Firstname + " " + x.Doctor.Lastname,
            PatientFullName = x.Patient.Firstname + " " + x.Patient.Lastname,
        }).ToList();
        return readVm;
    }

    public async Task<AppointmentReadVm> GetAppointmentAsync(int id)
    {
        Appointment? appointment = await _appointmentRepository.GetByIdAsync(id);
        AppointmentReadVm appointmentRead = new AppointmentReadVm()
        {
            id = appointment.Id,
            DateTime = appointment.DateTime,
            DoctorId = appointment.DoctorId,
            PatienId=appointment.PatientId,
            DoctorFullName = appointment.Doctor.Firstname + " " + appointment.Doctor.Lastname,
            PatientFullName = appointment.Patient.Firstname + " " + appointment.Patient.Lastname,
        };
        return appointmentRead;
    }

    public async Task isExist(int id)
    {
        if (await _appointmentRepository.isExsist(id))
        {
            throw new Exception404();
        }
    }

    public async Task Delete(int id)
    {
        Appointment appointment = await _appointmentRepository.GetByIdAsync(id);
        _appointmentRepository.Delete(appointment);
        await _appointmentRepository.SaveChangeAsync();
    }

    public async Task Update(AppointmentUpdate updatevm)
    {
        Appointment appointment=new Appointment()
        {
            Id=updatevm.id,
            DateTime = updatevm.DateTime,
            PatientId=updatevm.PatienId,
            DoctorId=updatevm.DoctorId,
        };
        _appointmentRepository.Update(appointment);
        await _appointmentRepository.SaveChangeAsync();
    }
}
