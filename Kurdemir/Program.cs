using Kurdemir.BL.Helpers.Extencions;
using Kurdemir.BL.Services.Abstractions;
using Kurdemir.BL.Services.Implementations;
using Kurdemir.Core.Models;
using Kurdemir.DAL.DAL;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Kurdemir.BL;
using Kurdemir.BL.Helpers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppDbContext>(opt =>
opt.UseSqlServer(builder.Configuration.GetConnectionString("MsSQL")));

builder.Services.AddIdentity<AppUser,IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddServices();
SmtpOptions options = new SmtpOptions();
builder.Services.Configure<SmtpOptions>(builder.Configuration.GetSection("Smtp"));


builder.Services.AddServices();

var app = builder.Build();

app.UseUserSeed();

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
            name: "areas",
            pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}"
          );

app.MapControllerRoute(
    name:"Default",
    pattern:"{controller=Home}/{action=index}");

app.Run();
