using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using StockSimulator.Domain.Cache;
using StockSimulator.Domain.Interfaces.Services;
using StockSimulator.Domain.ValuableObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace StockSimulator.Service.QuoteSimulator
{
    public class Listener : IListenerService
    {
        public Listener()
        {

        }

        //private MemoryCache _cache;
        public dynamic Item { get; set; }
        public List<Quote> Items { get; set; }
        public bool Started { get; set; }
        private Task listenTask;
        private CancellationTokenSource src = new CancellationTokenSource();


        public void StopListening()
        {
            this.src.Cancel();
        }

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



        public async Task<dynamic> Listen(string url)
        {
            if (String.IsNullOrEmpty(url) || String.IsNullOrWhiteSpace(url))
                throw new Exception("A url is required");

            Started = true;

            ClientWebSocket socket = new ClientWebSocket();
            listenTask = Task.Run(async () =>
            {
                try
                {
                    bool notFound = true;
                    byte[] buffer = new byte[1024];
                    await socket.ConnectAsync(new Uri(url), CancellationToken.None);
                    Items = new List<Quote>();

                    while (true)
                    {
                        WebSocketReceiveResult result = await socket.ReceiveAsync(buffer, CancellationToken.None);
                        string data = Encoding.UTF8.GetString(buffer, 0, result.Count);
                        var obj = JsonConvert.DeserializeObject(data);
                        var quote = ConvertToQuote(obj);
                        Item = quote;

                        Items.Add(quote);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }, src.Token);

            return Item;
        }

        public IEnumerable<dynamic> GetMostTradedOperations(int max)
        {
            IEnumerable<dynamic> querybase = null;
            if (Items != null)
            {
                querybase = (from t in ((from o in Items
                                         where o.Timestamp >= Convert.ToDateTime(Convert.ToDateTime(DateTime.Now)).AddDays(-7) && o.Timestamp <= DateTime.Now
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
                                 CurrentPrice = Items?.Where(f => f.Name.Equals(t.Name))?.OrderByDescending(f => f.Timestamp)?.FirstOrDefault()?.Value ?? 0M
                             })
                             .Take(max);
            }

            return querybase;
        }

        public Quote ConvertToQuote(object obj)
        {
            var obj2 = obj.ToString().Replace(":", "").Replace("\r\n", "").Replace("{", "").Replace("}", "").Trim().Split("\"");
            Quote quote = new Quote();
            quote.Name = obj2[1];
            quote.Value = decimal.Parse(obj2[2].Replace(", ", "").Trim().Replace(".", ","));
            var timestamp = Math.Round(decimal.Parse(obj2[4].ToString().Trim().Replace(".", ",")), 0);
            quote.Timestamp = StockSimulator.CrossCutting.Utils.Converters.UnixTimeStampToDateTime((double)timestamp);
            return quote;
        }

    }
}
