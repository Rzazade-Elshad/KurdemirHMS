using Kurdemir.BL.Helpers.Exceptions;
using Kurdemir.BL.Services.Abstractions;
using Kurdemir.BL.ViewModels.AccountVMs;
using Kurdemir.Core.Enums;
using Kurdemir.Core.Models;
using Kurdemir.DAL.Repositories.Abstractions;
using Microsoft.AspNetCore.Identity;
namespace Kurdemir.BL.Services.Implementations;

public class AccountService(IAppUserRepository appUserRepo) :IAccountService
{
    readonly IAppUserRepository _appUserRepo=appUserRepo;

    public async Task<string> RegisterAsync(RegisterVm registerVm ,int Role)
    {
        if (!Enum.IsDefined(typeof(UserRoles),Role))
        {
            throw new Exception404();
        }
        var rolename = (UserRoles)Role;
        AppUser appUser = new AppUser()
        {
            Email = registerVm.Email,
            UserName=registerVm.Username,
            RoleName=rolename.ToString(),

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
        var selectedRole = (UserRoles)Role;
        await _appUserRepo.AddtoRoleAsync(appUser, selectedRole.ToString());
        registerVm.UserId = appUser.Id;
        return "Succeeded";
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
        if (appUser.IsDeleted == true)
        {
            return "This profile has been deleted";
        }
        await _appUserRepo.SignInAsync(appUser, loginVm.Remember);
        return "Succeeded";
    }
    public async Task Logout()
    {
        await _appUserRepo.SignOutAsync();
    }
    public async Task<bool> SoftDeleteAsync(string userId)
    {
        AppUser? user =await _appUserRepo.FindByIdAsync(userId);

        if (user == null)
            throw new Exception404();

        user.IsDeleted = true;
        user.DeleteTime = DateTime.UtcNow.AddHours(4); // Bakı vaxtına uyğun

        var result = await _appUserRepo.UpdateAsync(user);

        return result.Succeeded;
    }
    public async Task<string> Update(UserUpdate update )
    {
        AppUser? user = await _appUserRepo.FindByIdAsync(update.UserID);

        if (user == null)
            throw new Exception404();
        user.Email = update.Email;
        user.UserName = update.UserName;
        var Result= await _appUserRepo.UpdateAsync(user);
        string errors=string.Empty;
        if (!Result.Succeeded)
        {
            foreach (var error in Result.Errors)
            {
                errors += error + "\n";
            }
            return errors;
        }
        return "Succeeds";
    }
}
