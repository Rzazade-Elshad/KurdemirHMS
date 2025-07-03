using Kurdemir.Core.Models;
using Kurdemir.DAL.DAL;
using Kurdemir.DAL.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;
namespace Kurdemir.DAL.Repositories.Implementations;

public class DoctorRepository : GenericRepository<Doctor>, IDoctorRepository
{
    public DoctorRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<List<Doctor>> GetAllAsync()
    {
        return await _dbcontext.Doctors.Include(d=>d.AppUser).Include(d=>d.Department).ToListAsync();
    }

    public Task<Doctor?> GetByIdAsync(int id)
    {
        return _dbcontext.Doctors.Include(d=>d.AppUser).Include(d=>d.Department).AsNoTracking().FirstOrDefaultAsync(d=>d.Id==id);
    }
}
