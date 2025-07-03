using Kurdemir.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kurdemir.DAL.Repositories.Abstractions;

public interface IPatientRepository:IGenericRepository<Patient>
{
    Task<Patient?> GetByIdAsync(int id);
    Task<List<Patient>> GetAllPatientsAsync();
    Task<int> GetIdByUserId(string userId);
}
