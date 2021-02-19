using StockSimulator.Domain.ValuableObjects;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StockSimulator.Domain.Interfaces.Services
{
    public interface IListenerService
    {
        Task<dynamic> Listen(string url, string filter);
        Task<dynamic> Listen(string url);
        void StopListening();
        public dynamic Item { get; set; }
        public List<Quote> Items { get; set; }

    }
}
