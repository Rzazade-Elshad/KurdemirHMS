using Kurdemir.Core.Models;
using Kurdemir.DAL.DAL;
using Kurdemir.DAL.Repositories.Abstractions;
namespace Kurdemir.DAL.Repositories.Implementations;

public class DoctorRepository : GenericRepository<Doctor>, IDoctorRepository
{
    public DoctorRepository(AppDbContext context) : base(context)
    {
    }
}
