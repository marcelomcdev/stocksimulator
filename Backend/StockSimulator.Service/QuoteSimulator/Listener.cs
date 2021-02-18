using Newtonsoft.Json;
using StockSimulator.Domain.ValuableObjects;
using System;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace StockSimulator.Service.QuoteSimulator
{
    public class Listener
    {
        public void StopListening()
        {
            this.src.Cancel();
        }

        public dynamic Item { get; set; }
        public List<dynamic> Items { get; set; }

        private Task listenTask;
        private CancellationTokenSource src = new CancellationTokenSource();

        public async Task<dynamic> Listen(string url, string filter)
        {
            if (String.IsNullOrEmpty(filter) || String.IsNullOrWhiteSpace(filter))
                throw new Exception("A filter is required.");

            if (String.IsNullOrEmpty(url) || String.IsNullOrWhiteSpace(url))
                throw new Exception("A url is required");

            ClientWebSocket socket = new ClientWebSocket();
            listenTask = Task.Run(async () =>
            {
                try
                {
                    bool notFound = true;
                    byte[] buffer = new byte[1024];
                    await socket.ConnectAsync(new Uri(url), CancellationToken.None);
                    while (notFound)
                    {
                        WebSocketReceiveResult result = await socket.ReceiveAsync(buffer, CancellationToken.None);
                        string data = Encoding.UTF8.GetString(buffer, 0, result.Count);
                        if (data.Contains(filter))
                        {
                            var obj = JsonConvert.DeserializeObject(data);
                            var quote = ConvertToQuote(obj);
                            Item = quote;
                            notFound = false;
                        }
                        Console.WriteLine(data);
                    }
                }
                catch (Exception ex)
                {
                    //treat exception
                }
            }, src.Token);

            return Item;
        }

        public Quote ConvertToQuote(object obj)
        {
            var obj2 = obj.ToString().Replace(":", "").Replace("\r\n", "").Replace("{", "").Replace("}", "").Trim().Split("\"");
            Quote quote = new Quote();
            quote.Name = obj2[1];
            quote.Value = decimal.Parse(obj2[2]);
            var timestamp = Math.Round(decimal.Parse(obj2[4].ToString().Trim().Replace(".", ",")), 0);
            quote.Timestamp = StockSimulator.CrossCutting.Utils.Converters.UnixTimeStampToDateTime((double)timestamp);
            return quote;
        }

    }
}
