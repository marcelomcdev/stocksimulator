using FluentValidation;
using StockSimulator.Domain.Entities;

namespace StockSimulator.Domain.Validators
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(x => x.Name).NotNull().NotEmpty().Length(3, 100);
            RuleFor(x => x.Email).NotNull().NotEmpty().EmailAddress(FluentValidation.Validators.EmailValidationMode.AspNetCoreCompatible);
            RuleFor(x => x.Password).NotNull().NotEmpty().Length(8, 20);
        }
    }
}
