using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kurdemir.BL.ViewModels.AccountVMs;

public class LoginVm
{
    public string EmailOrUsername {  get; set; }
    public string Password { get; set; }
    public bool Remember {  get; set; }
}
