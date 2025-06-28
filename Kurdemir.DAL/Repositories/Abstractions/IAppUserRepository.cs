using Kurdemir.Core.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kurdemir.DAL.Repositories.Abstractions;

public interface IAppUserRepository
{
    Task<IdentityResult> CreateAsync(AppUser appUser, string password);
    Task<IdentityResult> AddtoRoleAsync(AppUser appUser, string Role);
    Task<AppUser?> FindByEmailAsync(string Email);
    Task<AppUser?> FindByNameAsync(string Name);
    Task<SignInResult> CheckPasswordSignAsync(AppUser user, string Password, bool Remember);
    Task SignInAsync(AppUser user, bool Remember);
    Task SignOutAsync();
    Task CreateRolesAsync(string role);
}
