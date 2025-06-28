using Kurdemir.Core.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kurdemir.Core.Models;

public class Patient: BaseUser
{
    public string AppUserId {  get; set; }
    AppUser AppUser { get; set; }
}
