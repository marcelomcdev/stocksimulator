using StockSimulator.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace StockSimulator.Domain.Interfaces.Business
{
    public interface ITradeOperations
    {
        IEnumerable<dynamic> GetMostTradedOperations(List<Operation> operations);
    }
}
