using Kurdemir.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kurdemir.DAL.Repositories.Abstractions
{
    public interface IDoctorRepository :IGenericRepository<Doctor>
    {
        Task<List<Doctor>> GetAllAsync();
    }
}
