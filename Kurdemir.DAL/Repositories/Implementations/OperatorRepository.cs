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

public class OperatorRepository : GenericRepository<Operator>, IOperatorRepository
{
    public OperatorRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<List<Operator>> GetAllAsync()
    {
        return await _dbcontext.Operators.ToListAsync();
    }
}
