using FluentValidation;
using StockSimulator.Domain.Entities;
using static StockSimulator.Domain.Enums.Enumerators;

namespace StockSimulator.Service.Validators
{
    public class OperationValidator : AbstractValidator<Operation>
    {
        public OperationValidator()
        {
            RuleFor(x => x.Id).NotNull();
            RuleFor(x => x.Name).NotNull().NotEmpty().Length(3, 50);
            RuleFor(x => x.Quantity).GreaterThan(0);
        }
    }
}
