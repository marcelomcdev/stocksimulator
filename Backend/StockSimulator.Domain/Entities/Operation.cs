using System.ComponentModel;
using static StockSimulator.Domain.Enums.Enumerators;

namespace StockSimulator.Domain.Entities
{
    public class Operation : BaseEntity
    {
        public string UserId { get; set; }
        public virtual User User { get; set; }
        public string Symbol { get; set; }
        public int Quantity { get; set; }
        public decimal CurrentPrice { get; set; }
        public OperationTypeEnum OperationType { get; set; }

        [ReadOnly(true)]
        public decimal TotalValue => Quantity * CurrentPrice;

    }
}
