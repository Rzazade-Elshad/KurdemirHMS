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

public class AppointmentRepository : GenericRepository<Appointment>, IAppointmentRepository
{
    public AppointmentRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<List<Appointment>> GetAllAsync()
    {
        return await _dbcontext.Appointments.Include(a=>a.Doctor).Include(a=>a.Patient).ToListAsync();
    }

    public async Task<Appointment?> GetByIdAsync(int id)
    {
        return await _dbcontext.Appointments.Include(a => a.Doctor).Include(a => a.Patient).AsNoTracking().FirstOrDefaultAsync(a=>a.Id==id);
    }
    public async Task TrackNoUpdate(Appointment appointment)
    {


        _dbcontext.Attach(appointment);
        _dbcontext.Entry(appointment).State = EntityState.Modified;
        await _dbcontext.SaveChangesAsync();
    }
}
