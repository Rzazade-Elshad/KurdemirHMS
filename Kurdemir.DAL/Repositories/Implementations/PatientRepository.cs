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

public class PatientRepository : GenericRepository<Patient>, IPatientRepository
{
    public PatientRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<List<Patient>> GetAllPatientsAsync()
    {
      return await _dbcontext.Patients.Include(p=>p.AppUser).ToListAsync();
    }

    public Task<Patient?> GetByIdAsync(int id)
    {
        return _dbcontext.Patients.AsNoTracking().FirstOrDefaultAsync();
    }

    public async Task<int> GetIdByUserId(string userId)
    {
        Patient? patient = await _dbcontext.Patients.AsNoTracking().FirstOrDefaultAsync(p => p.AppUserId == userId);
        if(patient != null)
        {
            return patient.Id;
        }
        return 0;
    }
}
