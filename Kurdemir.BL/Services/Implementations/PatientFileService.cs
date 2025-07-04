using Kurdemir.BL.Services.Abstractions;
using Kurdemir.BL.ViewModels.PatientFileVMs;
using Kurdemir.Core.Models;
using Kurdemir.DAL.Repositories.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kurdemir.BL.Services.Implementations;

public class PatientFileService(IPatientFileRepository patientFileRepository) :IPatientFileService
{
    readonly IPatientFileRepository _patientFileRepository=patientFileRepository;
    public async Task Upload(PatientFileUpload patientFileVm)
    {
        PatientFile patientFile =new PatientFile()
        {
            PatientId = patientFileVm.PatientId,
            FileURL=patientFileVm.FileUrl,
            Title = patientFileVm.Title,
        };
       await _patientFileRepository.CreateAsync(patientFile);
        await _patientFileRepository.SaveChangeAsync();
    }
    public async Task<List<PatientFileRead >> GetPatientFile(int id)
    {
        List<PatientFile> patientFiles= await _patientFileRepository.GetAllPatientsAsync();
        List<PatientFileRead> patientFileReads = patientFiles.Where(p=>p.PatientId == id).Select(p=> new PatientFileRead()
        {
            id = p.Id,
            FileUrl=p.FileURL,
            Title = p.Title,
        }).ToList();

        return patientFileReads;
    }

}
