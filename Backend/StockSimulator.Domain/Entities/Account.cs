using System;
using System.Collections.Generic;
using System.Text;

namespace StockSimulator.Domain.Entities
{
    public class Account : BaseEntity
    {
        public string Name { get; set; }
        public decimal TotalBalance { get; set; }
        public int UserId { get; set; }
    }
}
