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
    public bool IsDeleted { get; set; } = false;

    public DateTime CreateTime { get; set; } = DateTime.UtcNow.AddHours(4);

    public DateTime? DeleteTime { get; set; }

    public bool RememberMe { get; set; } = false;

    public string RoleName { get; set; }
}
