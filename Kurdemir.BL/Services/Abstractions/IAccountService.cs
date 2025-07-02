using Kurdemir.BL.ViewModels.AccountVMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kurdemir.BL.Services.Abstractions;

public interface IAccountService
{
    Task<string> LoginAsync(LoginVm loginVm);
    Task Logout();
    Task<string> RegisterAsync(RegisterVm registerVm,int Role);
    Task<string> Update(UserUpdate update);
    public Task<bool> SoftDeleteAsync(string userId);
}
