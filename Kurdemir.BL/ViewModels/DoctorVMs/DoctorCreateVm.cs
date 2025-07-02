using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
namespace Kurdemir.BL.ViewModels.DoctorVMs;

public class DoctorCreateVm
{
    public string UserId { get; set; }
    public string ImgUrl { get; set; }
    public string Firstname { get; set; }
    public string Lastname { get; set; }
    public int Gender { get; set; }
    public int DepartmentId { get; set; }

}
