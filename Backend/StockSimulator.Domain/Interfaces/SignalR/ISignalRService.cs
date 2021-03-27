using StockSimulator.Domain.ValuableObjects;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StockSimulator.Domain.Interfaces.SignalR
{
    public interface ISignalRService
    {
        Task SendMessage(string message);
        Task SendQuotes(ICollection<Quote> quotes);
    }
}
