using Kurdemir.BL.Helpers.Exceptions;
using Kurdemir.BL.Services.Abstractions;
using Kurdemir.BL.ViewModels.AccountVMs;
using Kurdemir.BL.ViewModels.OperatorVMs;
using Kurdemir.Core.Enums;
using Kurdemir.Core.Models;
using Kurdemir.DAL.Repositories.Abstractions;


namespace Kurdemir.BL.Services.Implementations;

public  class OperatorService(IOperatorRepository operatorRepository ,IAppUserRepository appUserRepository) :IOperatorService
{
    readonly IOperatorRepository _operatorRepository=operatorRepository;
    readonly IAppUserRepository _appUserRepository=appUserRepository;

    public async Task<string> RegisterAsync(OperatorCreateVm operatorVm)
    {
        AppUser appUser = new AppUser()
        {
            UserName = operatorVm.Username,
            Email = operatorVm.Email,
            RoleName = "Operator"
        };

        var Result = await _appUserRepository.CreateAsync(appUser, operatorVm.Password);
        if (!Result.Succeeded)
        {
            string errors = string.Empty;
            foreach (var error in Result.Errors)
            {
                errors += error.Description + "\n";
            }
            return errors;
        }

        await _appUserRepository.AddtoRoleAsync(appUser, UserRoles.Operator.ToString());

        operatorVm.UserId = appUser.Id;

        return "Succeeded";
    }
    public async Task OperatorCreate(OperatorCreateVm operatorVm)
    {
        if (!Enum.IsDefined(typeof(UserRoles), operatorVm.Gender))
        {
            throw new Exception404();
        }

        Operator operatorm= new Operator()
        {
            AppUserId=operatorVm.UserId,
            Firstname = operatorVm.Firstname,
            Lastname = operatorVm.Lastname,
            Gender=(Genders)operatorVm.Gender,
            ImageUrl=operatorVm.ImgUrl,

        };
        await _operatorRepository.CreateAsync(operatorm);
        await _operatorRepository.SaveChangeAsync();
    }
    public async Task<List<OperatorReadVm>> OperatorGetAll()
    {
        List<Operator> operators=await _operatorRepository.GetAllAsync();
        List<OperatorReadVm> operatorReadVms = operators.Select(o=> new OperatorReadVm() {
            Id= o.Id,
          UserId=o.AppUserId,
          ImgUrl =o.ImageUrl,
          Firstname =o.Firstname,
          Lastname=o.Lastname,
          Gender =o.Gender.ToString(),
        GenderValue=((int)o.Gender)
        }).ToList();
        return operatorReadVms;
    }
    public async Task<OperatorReadVm> OperatorGet(int id)
    {
        Operator? @operator = await _operatorRepository.GetByIdAsync(id);
        OperatorReadVm operatorRead = new OperatorReadVm()
        {
            Id = id,
            Firstname=@operator.Firstname,
            Lastname=@operator.Lastname,
            Gender =@operator.Gender.ToString(),
            GenderValue=((int)@operator.Gender),
            ImgUrl=@operator.ImageUrl,
            UserId=@operator.AppUserId,
        };
        return operatorRead;
    }
    public  async Task SoftDelete(OperatorReadVm operatorReadVm)
    {
        Operator @operator = new Operator()
        {
            Id = operatorReadVm.Id,
            AppUserId=operatorReadVm.UserId,
            Firstname=operatorReadVm.Firstname,
            Lastname=operatorReadVm.Lastname,
            ImageUrl = operatorReadVm.ImgUrl,
            Gender=(Genders)operatorReadVm.GenderValue,
            DeleteTime=DateTime.UtcNow.AddHours(4),
        };
         _operatorRepository.SoftDelete(@operator);
        await _operatorRepository.SaveChangeAsync();
    }
    public async Task Delete(OperatorReadVm operatorReadVm)
    {
        Operator @operator = new Operator()
        {
            Id = operatorReadVm.Id,
            AppUserId = operatorReadVm.UserId,
            Firstname = operatorReadVm.Firstname,
            Lastname = operatorReadVm.Lastname,
            ImageUrl = operatorReadVm.ImgUrl,
            Gender = (Genders)operatorReadVm.GenderValue,
        };
        _operatorRepository.Delete(@operator);
        await _operatorRepository.SaveChangeAsync();
    }
}
