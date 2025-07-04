using Kurdemir.BL.ViewModels.PatientFileVMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kurdemir.BL.Services.Abstractions;

public interface  IPatientFileService
{
    Task Upload(PatientFileUpload patientFileVm);
    Task<List<PatientFileRead>> GetPatientFile(int id);
}
