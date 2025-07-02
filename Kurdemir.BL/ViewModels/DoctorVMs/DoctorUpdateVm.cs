using Kurdemir.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kurdemir.BL.ViewModels.DoctorVMs;

public class DoctorUpdateVm
{
    public int Id { get; set; }
    public string ImgUrl { get; set; }
    public string Firstname { get; set; }
    public string Lastname { get; set; }
    public int Gender { get; set; }
    public IFormFile? FormFile { get; set; }

    public string UserId { get; set; }
    public string Email { get; set; }
    public string Uername { get; set; }

    public int DepartmentId {  get; set; }
    public List<SelectListItem> Departments { get; set; }
}
