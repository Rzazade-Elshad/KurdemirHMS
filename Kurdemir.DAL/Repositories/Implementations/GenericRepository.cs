using Kurdemir.Core.Models.Base;
using Kurdemir.DAL.DAL;
using Kurdemir.DAL.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kurdemir.DAL.Repositories.Implementations;

public class GenericRepository<Tentity>(AppDbContext context) : 
    IGenericRepository<Tentity> where Tentity : BaseModel

{
    protected readonly AppDbContext _dbcontext=context;

    DbSet<Tentity> _dbSet => _dbcontext.Set<Tentity>();

    public async Task CreateAsync(Tentity entity)
    {
        await _dbSet.AddAsync(entity);
    }

    public  void Delete(Tentity entity)
    {
         _dbSet.Remove(entity);
    }

    public Task<Tentity?> GetByIdAsync(int id)
    {
        return _dbSet.AsNoTracking().FirstOrDefaultAsync();
    }

    public async Task<bool> isExsist(int id)
    {
        return await _dbSet.AnyAsync(e=>e.Id==id);
    }

    public async Task SaveChangeAsync()
    {
         await _dbcontext.SaveChangesAsync();
    }

    public void Update(Tentity entity)
    {
        _dbSet.Update(entity);
    }
}
