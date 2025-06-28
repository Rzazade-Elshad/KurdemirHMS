using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kurdemir.BL.ViewModels.DoctorVMs;

public class DoctorCreateVm
{
    public string? UserId { get; set; }
    public string? ImgUrl { get; set; }
    public IFormFile Image { get; set; }
    public string Firstname { get; set; }
    public string Lastname { get; set; }
    public int Gender { get; set; }

    public string Username { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public string ConfiConfrimPassword { get; set; }

    public int DepartmentId { get; set; }
    public List <SelectListItem> Departments { get; set; }

}
