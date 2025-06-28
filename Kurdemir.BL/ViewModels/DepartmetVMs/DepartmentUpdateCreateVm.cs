using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kurdemir.BL.ViewModels.DepartmetVMs;

public class DepartmentUpdateCreateVm
{
    public int Id { get; set; }
    public string Name { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime Createtime { get; set; }
}
