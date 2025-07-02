using Kurdemir.Core.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kurdemir.Core.Models;

public class Department :BaseModel
{
    public string Name {  get; set; }
    public List<Doctor> Doctors { get; set; }
}
