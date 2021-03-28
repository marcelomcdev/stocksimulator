using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Primitives;
using StockSimulator.Domain.DependencyInjection;
using StockSimulator.Domain.Interfaces.Services;
using StockSimulator.Domain.ValuableObjects;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using static StockSimulator.Domain.Cache.Configurations.Constants;

namespace StockSimulator.Domain.Cache
{
    public class AppCache
    {
        private readonly IListenerService _listenerService;
        public AppCache(IListenerService listenerService)
        {
            _listenerService = listenerService;
        }

        public static List<Quote> Quotes
        {
            get
            {
                List<Quote> list = new List<Quote>();
                if(!Dependencies.MemoryCache.TryGetValue(CacheKeys.Quotes, out list))
                {
                    var item = Dependencies.MemoryCache.Get(CacheKeys.Quotes);
                    if (list == null) list = new List<Quote>();
                    else
                    list = (List<Quote>)item;

                    var cacheEntryOptions = new MemoryCacheEntryOptions()
                        .SetSize(1)
                         .SetSlidingExpiration(TimeSpan.FromHours(1))
                         .AddExpirationToken(new CancellationChangeToken(new CancellationTokenSource(TimeSpan.FromHours(1)).Token));
                    Dependencies.MemoryCache.Set(CacheKeys.Quotes, list, cacheEntryOptions);
                }
                return list;
            }
        }
    }
}
