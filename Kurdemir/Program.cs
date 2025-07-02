using Kurdemir.BL.Helpers.Extencions;
using Kurdemir.BL.Services.Abstractions;
using Kurdemir.BL.Services.Implementations;
using Kurdemir.Core.Models;
using Kurdemir.DAL.DAL;
using Kurdemir.DAL.Repositories.Abstractions;
using Kurdemir.DAL.Repositories.Implementations;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppDbContext>(opt =>
opt.UseSqlServer(builder.Configuration.GetConnectionString("MsSQL")));

builder.Services.AddIdentity<AppUser,IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddScoped<IAccountService,AccountService>();
builder.Services.AddScoped<IDepartmentService,DepartmentService>();
builder.Services.AddScoped<IDoctorService,DoctorService>();
builder.Services.AddScoped<IPatientService,PatientService>();

builder.Services.AddScoped<IPatientRepository, PatientRepository>();
builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
builder.Services.AddScoped<IDoctorRepository, DoctorRepository>();
builder.Services.AddScoped<IAppUserRepository, AppUserRepository>();



var app = builder.Build();

app.UseUserSeed();

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapControllerRoute(
            name: "areas",
            pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}"
          );

app.MapControllerRoute(
    name:"Default",
    pattern:"{controller=Home}/{action=index}");

app.Run();
