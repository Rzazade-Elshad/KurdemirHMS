using Kurdemir.Core.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kurdemir.DAL.Repositories.Abstractions;

public interface IGenericRepository<Tentity> where Tentity : BaseModel
{
    Task CreateAsync(Tentity entity);
    void Update(Tentity entity);
    void Delete(Tentity entity);
    Task<bool> isExsist(int id);
    Task SaveChangeAsync();
    
}
