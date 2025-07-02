using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kurdemir.BL.ViewModels.DoctorVMs;

public class DoctorReadVm
{
    public int Id { get; set; }
    public string ImgUrl { get; set; }
    public string Firstname { get; set; }
    public string Lastname { get; set; }
    public string? Gender { get; set; }

    public string UserId { get; set; }
    public string Email {  get; set; }
    public string Username {  get; set; }
    public bool IsDelete { get; set; }
    public DateTime? DeleteTime { get; set; }

    public string DepartmentName {  get; set; }
}
