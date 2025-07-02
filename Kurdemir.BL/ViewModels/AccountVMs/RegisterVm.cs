using Kurdemir.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kurdemir.BL.ViewModels.AccountVMs;

public class RegisterVm
{
    public string Username { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }

    public string? UserId { get; set; }

}
