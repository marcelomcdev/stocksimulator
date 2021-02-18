using FluentValidation;
using StockSimulator.Domain.Entities;

namespace StockSimulator.Service.Validators
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(x => x.Name).NotNull().NotEmpty().Length(3, 100);
            RuleFor(x => x.Email).NotNull().NotEmpty().EmailAddress(FluentValidation.Validators.EmailValidationMode.AspNetCoreCompatible).Length(1,150);
            RuleFor(x => x.CPF).NotNull().NotEmpty().Length(11);
        }
    }
}
