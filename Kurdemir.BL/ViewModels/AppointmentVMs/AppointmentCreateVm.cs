using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kurdemir.BL.ViewModels.AppointmentVMs;

public class AppointmentCreateVm
{
    public int DoctorId {  get; set; }
    public int PatientId {  get; set; }
    public DateTime DateTime { get; set; }
    public List<DateTime>? DateTimes { get; set; }
}
