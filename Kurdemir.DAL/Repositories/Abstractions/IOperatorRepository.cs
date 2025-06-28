using Kurdemir.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kurdemir.DAL.Repositories.Abstractions;

public interface IOperatorRepository :IGenericRepository<Operator>
{
    public Task<List<Operator>> GetAllAsync();
}
