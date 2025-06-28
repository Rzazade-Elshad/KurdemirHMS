using Kurdemir.BL.ViewModels.DepartmetVMs;
using Kurdemir.BL.ViewModels.OperatorVMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kurdemir.BL.Services.Abstractions;

public interface IOperatorService
{
    Task<string> RegisterAsync(OperatorCreateVm operatorVm, int roleValue);
    Task OperatorCreate(OperatorCreateVm operatorVm);
    Task<List<OperatorReadVm>> OperatorGetAll();
    Task<OperatorReadVm> OperatorGet(int id);
    Task SoftDelete(OperatorReadVm operatorReadVm);
    Task Delete(OperatorReadVm operatorReadVm);
}
