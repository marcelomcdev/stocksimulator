using System;
using System.Collections.Generic;
using System.Text;

namespace StockSimulator.Tests.Models
{
    public class Acao
    {
        public string Symbol { get; set; }
        public int Quantity { get; set; }
        public decimal Value { get; set; }
        public Domain.Enums.Enumerators.OperationTypeEnum Operation { get; set; }
    }
}
