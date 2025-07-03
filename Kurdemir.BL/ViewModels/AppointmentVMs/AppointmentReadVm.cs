using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kurdemir.BL.ViewModels.AppointmentVMs;

public class AppointmentReadVm
{
    public int id {  get; set; }
    public int PatienId {  get; set; }
    public int DoctorId {  get; set; }
    public string DoctorFullName {  get; set; }
    public string PatientFullName {  get; set; }
    public DateTime DateTime { get; set; }
}
