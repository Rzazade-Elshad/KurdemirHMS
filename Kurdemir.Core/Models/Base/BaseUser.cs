using Kurdemir.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kurdemir.Core.Models.Base;

public abstract class BaseUser :BaseModel
{
    public string Firstname { get; set; }

    public string Lastname { get; set; }

    public Genders Gender { get; set; }

}
