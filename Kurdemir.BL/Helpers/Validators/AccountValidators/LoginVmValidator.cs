using FluentValidation;
using Kurdemir.BL.ViewModels.AccountVMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kurdemir.BL.Helpers.Validators.AccountValidators;

public class LoginVmValidator : AbstractValidator<LoginVm>
{
    public LoginVmValidator()
    {
        RuleFor(x => x.EmailOrUsername)
            .NotEmpty().WithMessage("Email və ya istifadəçi adı boş ola bilməz")
            .MinimumLength(3).WithMessage("Ən azı 3 simvol olmalıdır");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Şifrə boş ola bilməz")
            .MinimumLength(6).WithMessage("Şifrə ən azı 6 simvoldan ibarət olmalıdır");

        // Remember sahəsi sadəcə boolean olduğu üçün ayrıca validasiya ehtiyac yoxdur.
    }
}
