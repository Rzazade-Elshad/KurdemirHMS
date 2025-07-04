using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kurdemir.BL.ViewModels.PatientVMs;

public class PatientReadVm
{
    public int Id {  get; set; }
    public string Username {  get; set; }

    public string FirstName {  get; set; }
    public string LastName { get; set; }
    public string Gender {  get; set; }
    public string Email {  get; set; }
}
