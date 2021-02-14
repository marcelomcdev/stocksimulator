using System;
using System.Collections.Generic;
using System.Text;

namespace StockSimulator.Domain.Entities
{
    public class Quote
    {
        public string Name { get; set; }
        public decimal Value { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
