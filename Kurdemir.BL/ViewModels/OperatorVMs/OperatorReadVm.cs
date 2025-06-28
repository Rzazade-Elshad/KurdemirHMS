using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kurdemir.BL.ViewModels.OperatorVMs;

public class OperatorReadVm
{
    public int Id {  get; set; }
    public string UserId { get; set; }
    public string ImgUrl { get; set; }
    public string Firstname { get; set; }
    public string Lastname { get; set; }
    public string? Gender { get; set; }
    public int GenderValue { get; set; }

}
