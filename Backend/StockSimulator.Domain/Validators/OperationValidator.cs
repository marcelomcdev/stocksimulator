using FluentValidation;
using StockSimulator.Domain.Entities;

namespace StockSimulator.Domain.Validators
{
    public class OperationValidator : AbstractValidator<Operation>
    {
        public OperationValidator()
        {
            RuleFor(x => x.Id).NotNull();
            RuleFor(x=> x.Name).NotNull().NotEmpty().Length(3, 50);
            RuleFor(x => x.Quantity).GreaterThan(0);
            RuleFor(x => x.OperationType).InclusiveBetween((int)Enums.Enumerators.OperationTypeEnum.Buy, (int)Enums.Enumerators.OperationTypeEnum.Sell);
        }
    }
}
