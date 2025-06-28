using Kurdemir.Core.Models;
using Kurdemir.DAL.DAL;
using Kurdemir.DAL.Repositories.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kurdemir.DAL.Repositories.Implementations;

public class PatientRepository : GenericRepository<Patient>, IPatientRepository
{
    public PatientRepository(AppDbContext context) : base(context)
    {
    }
}
