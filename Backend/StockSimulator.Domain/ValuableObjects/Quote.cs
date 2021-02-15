using System;

namespace StockSimulator.Domain.ValuableObjects
{
    public class Quote
    {
        public string Name { get; set; }
        public decimal Value { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
