using Kurdemir.Core.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kurdemir.Core.Models;

public class Doctor : BaseUser
{
    public string ImageUrl { get; set; }

    public string AppUserId { get; set; }
    public int DepartmentId { get; set; }

    AppUser AppUser { get; set; }
    public Department Department { get; set; }
}
