using Kurdemir.Core.Models;
using Kurdemir.DAL.Repositories.Abstractions;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kurdemir.DAL.Repositories.Implementations;

public class AppUserRepository(UserManager<AppUser> userManager ,
                               RoleManager<IdentityRole> roleManager, 
                               SignInManager<AppUser> signinManger)  : IAppUserRepository

{
    readonly UserManager<AppUser> _userManager= userManager;
    readonly RoleManager<IdentityRole> _roleManager= roleManager;
    readonly SignInManager<AppUser> _signinManger= signinManger;

    public async Task<IdentityResult> CreateAsync(AppUser appUser,string password)
    {
        return await _userManager.CreateAsync(appUser, password);
    }

    public async Task<IdentityResult> AddtoRoleAsync(AppUser appUser ,string Role)
    {
        return await _userManager.AddToRoleAsync(appUser, Role);
    }

    public async Task<AppUser?> FindByEmailAsync(string Email)
    {
        return await _userManager.FindByEmailAsync(Email);
    }

    public async Task<AppUser?> FindByNameAsync(string Name)
    {
        return await _userManager.FindByNameAsync(Name);
    }

    public async Task<SignInResult> CheckPasswordSignAsync(AppUser user, string Password, bool Remember)
    {
        return await _signinManger.CheckPasswordSignInAsync(user, Password, Remember);
    }

    public async Task SignInAsync(AppUser user, bool Remember)
    {
         await _signinManger.SignInAsync(user, Remember);
    }

    public async Task SignOutAsync()
    {
        await _signinManger.SignOutAsync();
    }
    public async Task CreateRolesAsync(string role)
    {
        await _roleManager.CreateAsync(new IdentityRole(role.ToString()));
    }

    public async Task<AppUser?> FindByIdAsync(string id)
    {
       return await _userManager.FindByIdAsync(id);
    }

    public async Task<IdentityResult> UpdateAsync(AppUser appUser)
    {
        return await _userManager.UpdateAsync(appUser);
    }
}
