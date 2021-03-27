using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StockSimulator.Service.SignalR
{
    public class TradeHub : Hub
    {
        private readonly static ConnectionMapping<string> _connections = new ConnectionMapping<string>();
        public static ConnectionMapping<string> Connections { get { return _connections; } }

        public TradeHub()
        {

        }

        [Authorize]
        public override Task OnConnectedAsync()
        {
            _connections.Add(Context.ConnectionId, "SignalRUsers");
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            _connections.Remove(Context.ConnectionId, "SignalRUsers");
            return base.OnDisconnectedAsync(exception);
        }
    }
}
