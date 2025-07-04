using Kurdemir.Core.Models;
using Kurdemir.DAL.DAL;
using Kurdemir.DAL.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kurdemir.DAL.Repositories.Implementations;

public class PatientFileRepositoy : GenericRepository<PatientFile>, IPatientFileRepository
{
    public PatientFileRepositoy(AppDbContext context) : base(context)
    {
    }

    public async Task<List<PatientFile>> GetAllPatientsAsync()
    {
        return await _dbcontext.PatientFiles.Include(p=>p.Patient).ToListAsync();
    }
}
