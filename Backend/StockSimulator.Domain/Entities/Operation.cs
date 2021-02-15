using System;
using System.Collections.Generic;
using System.Text;

namespace StockSimulator.Domain.Entities
{
    public class Operation : BaseEntity
    {
        public int AccountId { get; set; }
        public virtual Account Account { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public int OperationType { get; set; }
    }
}
