using StockSimulator.Domain.Entities;
using StockSimulator.Domain.Interfaces.Business;
using StockSimulator.Domain.ValuableObjects;
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
                 where o.OperationDate >= Convert.ToDateTime(Convert.ToDateTime(DateTime.Now)).AddDays(-37) && o.OperationDate <= DateTime.Now
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


        public IEnumerable<dynamic> GetMostTradedOperations(List<Quote> quotes, int max)
        {
            IEnumerable<dynamic> querybase = null;
            if (quotes != null)
            {
                querybase = (from t in ((from o in quotes
                                         where o.Timestamp >= Convert.ToDateTime(Convert.ToDateTime(DateTime.Now)).AddDays(-37) && o.Timestamp <= DateTime.Now
                                         group o by new { o.Name } into g
                                         select new
                                         {
                                             g.Key.Name,
                                             Total = g.Count()
                                         }))
                             orderby t.Total descending
                             select new
                             {
                                 Symbol = t.Name,
                                 Total = t.Total,
                                 CurrentPrice = quotes?.Where(f => f.Name.Equals(t.Name))?.OrderByDescending(f => f.Timestamp)?.FirstOrDefault()?.Value ?? 0M
                             }).Take(max);
            }
            return querybase;
        }

    }
}
