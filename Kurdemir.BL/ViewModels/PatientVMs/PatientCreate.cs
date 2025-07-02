using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kurdemir.BL.ViewModels.PatientVMs;

public class PatientCreate
{
    public string UserId { get; set; }
    public string Firstname { get; set; }
    public string Lastname { get; set; }
    public int Gender { get; set; }
}
