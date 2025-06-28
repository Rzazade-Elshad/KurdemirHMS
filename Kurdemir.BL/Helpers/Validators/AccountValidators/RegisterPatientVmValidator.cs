using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Kurdemir.BL.ViewModels.AccountVMs;
namespace Kurdemir.BL.Helpers.Validators.RegisterPatientValitadors;
public class RegisterPatientVmValidator : AbstractValidator<RegisterPatientVm>
{
    public RegisterPatientVmValidator()
    {
        RuleFor(x => x.Firstname)
            .NotNull()
            .NotEmpty()
            .MinimumLength(3)
            .MaximumLength(25);

        RuleFor(x => x.Lastname)
            .NotNull()
            .NotEmpty()
            .MinimumLength(3)
            .MaximumLength(25);

        RuleFor(x => x.Username)
            .NotNull()
            .NotEmpty()
            .MinimumLength(4)
            .MaximumLength(30);

        RuleFor(x => x.Password)
            .NotNull()
            .NotEmpty()
            .MinimumLength(6)
            .Matches("[A-Z]")
            .Matches("[a-z]")
            .Matches("[0-9]");

        RuleFor(x => x.ConfiConfrimPassword)
            .NotNull()
            .NotEmpty()
            .Equal(x => x.Password);

        RuleFor(x => x.Email)
            .NotNull()
            .NotEmpty()
            .EmailAddress();

        RuleFor(x => x.Gender).Must(c=>c==1||c==2)
            .NotEmpty()
            .NotNull();
    }
}