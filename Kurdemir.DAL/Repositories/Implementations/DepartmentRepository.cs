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

public class DepartmentRepository :GenericRepository<Department> ,IDepartmentRepository 
{
    public DepartmentRepository(AppDbContext context):base(context)
    {
    }
    public async Task<List<Department>> GetAllDepartmentsAsync()
    {
       return await _dbcontext.Departments.Include(d=>d.Doctors).ToListAsync();
    }
    public async Task<Department?> GetByIdAsync(int id)
    {
        return await _dbcontext.Departments.AsNoTracking().FirstOrDefaultAsync();
    }
}
