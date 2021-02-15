using FluentValidation;
using StockSimulator.Domain.Entities;

namespace StockSimulator.Service.Validators
{
    public class OperationValidator : AbstractValidator<Operation>
    {
        public OperationValidator()
        {
            RuleFor(x => x.Id).NotNull();
            RuleFor(x => x.Name).NotNull().NotEmpty().Length(3, 50);
            RuleFor(x => x.Quantity).GreaterThan(0);
            RuleFor(x => x.OperationType).InclusiveBetween((int)Domain.Enums.Enumerators.OperationTypeEnum.Buy, (int)Domain.Enums.Enumerators.OperationTypeEnum.Sell);
        }
    }
}
