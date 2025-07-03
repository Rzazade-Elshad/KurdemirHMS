using Kurdemir.Core.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kurdemir.Core.Models;

public class Appointment :BaseModel
{
    public int DoctorId {  get; set; }

    public int PatientId {  get; set; }

    public Doctor Doctor { get; set; }
    
    public Patient Patient { get; set; }

    public DateTime DateTime { get; set; }
}
