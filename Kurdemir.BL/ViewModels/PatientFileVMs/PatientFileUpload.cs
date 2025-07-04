using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kurdemir.BL.ViewModels.PatientFileVMs;

public class PatientFileUpload
{
    public int PatientId {  get; set; }
    public string Title {  get; set; }
    public string? FileUrl {  get; set; }
    public IFormFile File { get; set; }
}
