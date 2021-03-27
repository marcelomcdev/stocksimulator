using Microsoft.AspNetCore.SignalR;
using StockSimulator.Domain.Interfaces.SignalR;
using StockSimulator.Domain.ValuableObjects;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StockSimulator.Service.SignalR
{
    public class SignalRService : ISignalRService
    {
        private readonly IHubContext<TradeHub> _hubContext;

        public SignalRService(IHubContext<TradeHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task SendMessage(string message)
        {
            await _hubContext.Clients.All.SendAsync("Notify", message);
        }

        public async Task SendQuotes(ICollection<Quote> quotes)
        {
            await _hubContext.Clients.All.SendAsync("notifyQuotes", quotes);
        }


        public dynamic GetLoggedUsers()
        {
            return TradeHub.Connections.GetAllConnections();
        }

    }
}
