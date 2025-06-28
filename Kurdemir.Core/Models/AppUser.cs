using Kurdemir.Core.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kurdemir.Core.Models;

public class AppUser :IdentityUser
{
    public bool RememberMe { get; set; } = false;
    public string RoleName { get; set; }
}
