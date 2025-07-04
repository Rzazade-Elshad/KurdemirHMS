using Kurdemir.Core.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kurdemir.Core.Models
{
    public class PatientFile : BaseModel
    {
        public string Title { get; set; } = null!;
        public string FileURL { get; set; } = null!;
        public int PatientId { get; set; }
        public Patient? Patient { get; set; }
    }
}
