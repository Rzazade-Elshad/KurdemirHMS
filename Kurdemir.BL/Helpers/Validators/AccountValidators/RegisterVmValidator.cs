using FluentValidation;
using Kurdemir.BL.ViewModels.AccountVMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kurdemir.BL.Helpers.Validators.AccountValidators
{

    public class RegisterVmValidator : AbstractValidator<RegisterVm>
    {
        public RegisterVmValidator()
        {
            RuleFor(x => x.Username)
                .NotEmpty().WithMessage("İstifadəçi adı boş ola bilməz")
                .MinimumLength(3).WithMessage("İstifadəçi adı ən azı 3 simvol olmalıdır")
                .MaximumLength(30).WithMessage("İstifadəçi adı 30 simvoldan çox ola bilməz");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Şifrə boş ola bilməz")
                .MinimumLength(6).WithMessage("Şifrə ən azı 6 simvol olmalıdır")
                .Matches("[A-Z]").WithMessage("Şifrə ən azı bir böyük hərf içerməlidir")
                .Matches("[a-z]").WithMessage("Şifrə ən azı bir kiçik hərf içerməlidir")
                .Matches("[0-9]").WithMessage("Şifrə ən azı bir rəqəm içerməlidir");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email boş ola bilməz")
                .EmailAddress().WithMessage("Email formatı düzgün deyil");

            // UserId optional olduğu üçün heç bir qayda əlavə etmirik
        }
    }

}
