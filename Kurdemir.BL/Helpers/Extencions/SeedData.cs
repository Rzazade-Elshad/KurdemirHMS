using Kurdemir.Core.Enums;
using Kurdemir.Core.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
namespace Kurdemir.BL.Helpers.Extencions;

public static class SeedData
{
    public static async void UseUserSeed(this IApplicationBuilder app)
    {


        using (var scope = app.ApplicationServices.CreateScope())
        {
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
            if (!await roleManager.Roles.AnyAsync())
            {
                foreach (var item in Enum.GetValues(typeof(UserRoles)))
                {
                    roleManager.CreateAsync(new IdentityRole(item.ToString())).Wait();
                }
                return;
            }
            if (!await userManager.Users.AnyAsync(x => x.NormalizedUserName == "Admin"))
            {
                AppUser appUser = new AppUser()
                {
                    UserName = "Admin123",
                    Email = "Admin@gmail.com",
                    RoleName = "Admin"
                };
                userManager.CreateAsync(appUser, "Admin123!").Wait();
                userManager.AddToRoleAsync(appUser, nameof(UserRoles.Admin)).Wait();
            }
        }
    }
}
