using Kurdemir.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kurdemir.DAL.Repositories.Abstractions;

public interface IAppointmentRepository :IGenericRepository<Appointment>
{
     Task<List<Appointment>> GetAllAsync();
    Task<Appointment?> GetByIdAsync(int id);
    Task TrackNoUpdate(Appointment appointment);
}
