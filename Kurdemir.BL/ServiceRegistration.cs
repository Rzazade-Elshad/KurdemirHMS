using FluentValidation;
using Kurdemir.BL.ExternalServices.Abstractions;
using Kurdemir.BL.ExternalServices.Implements;
using Kurdemir.BL.Helpers.Validators.AccountValidators;
using Kurdemir.BL.Services.Abstractions;
using Kurdemir.BL.Services.Implementations;
using Kurdemir.BL.ViewModels.AccountVMs;
using Kurdemir.DAL.Repositories.Abstractions;
using Kurdemir.DAL.Repositories.Implementations;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Kurdemir.BL
{
    public  static class ServiceRegistration
    {
        public static IServiceCollection AddServices(this IServiceCollection services )
        {

            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IDepartmentService, DepartmentService>();
            services.AddScoped<IDoctorService, DoctorService>();
            services.AddScoped<IPatientService, PatientService>();
            services.AddScoped<IAppointmentRepository, AppointmentRepository>();
            services.AddScoped<IPatientFileRepository, PatientFileRepositoy>();

            services.AddScoped<IPatientFileService, PatientFileService>();
            services.AddScoped<IAppointmentService, AppointmentService>();
            services.AddScoped<IPatientRepository, PatientRepository>();
            services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            services.AddScoped<IDoctorRepository, DoctorRepository>();
            services.AddScoped<IAppUserRepository, AppUserRepository>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IValidator<LoginVm>, LoginVmValidator>();
            services.AddScoped<IValidator<RegisterVm>, RegisterVmValidator>();

            return services;
        }
    }
}
