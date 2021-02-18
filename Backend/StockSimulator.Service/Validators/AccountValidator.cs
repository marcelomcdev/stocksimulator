using FluentValidation;
using StockSimulator.Domain.Entities;

namespace StockSimulator.Service.Validators
{
    public class AccountValidator : AbstractValidator<Account>
    {
        public AccountValidator()
        {
            RuleFor(x => x.Id).NotNull();
            RuleFor(x => x.Bank).NotNull();
            RuleFor(x => x.Branch).NotNull();
            RuleFor(x => x.AccountNumber).NotNull();
            RuleFor(x => x.UserId).NotNull();
        }
    }
}
