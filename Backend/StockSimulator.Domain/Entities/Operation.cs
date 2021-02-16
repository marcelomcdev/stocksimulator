using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using static StockSimulator.Domain.Enums.Enumerators;

namespace StockSimulator.Domain.Entities
{
    public class Operation : BaseEntity
    {
        public int AccountId { get; set; }
        public virtual Account Account { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public OperationTypeEnum OperationType { get; set; }

        [ReadOnly(true)]
        public decimal TotalValue => Quantity * Price;

    }
}
