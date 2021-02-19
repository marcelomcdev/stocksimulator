using StockSimulator.Domain.Entities;
using StockSimulator.Domain.Interfaces.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StockSimulator.CrossCutting.Business
{
    public class TradeOperations : ITradeOperations
    {
        public IEnumerable<dynamic> GetMostTradedOperations(List<Operation> operations) 
        {
            var querybase = (from t in ((from o in operations
                 where o.OperationDate >= Convert.ToDateTime(Convert.ToDateTime(DateTime.Now)).AddDays(-7) && o.OperationDate <= DateTime.Now
                 group o by new {o.Symbol } into g 
                 select new
                 {
                     g.Key.Symbol,
                     Total = g.Count()
                 }))
                 orderby t.Total descending
                 select new
                 {
                    Symbol = t.Symbol,
                    Total = t.Total,
                    CurrentPrice = operations?.Where(f => f.Symbol.Equals(t.Symbol))?.OrderByDescending(f => f.OperationDate)?.FirstOrDefault()?.CurrentPrice ?? 0M
                 }).Take(5);

            return querybase;

        }
    }
}
