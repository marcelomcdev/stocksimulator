using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Primitives;
using StockSimulator.Domain.DependencyInjection;
using StockSimulator.Domain.ValuableObjects;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace StockSimulator.Service.Cache
{
    public class CacheService
    {
        //private readonly IMemoryCache _cache;
        //public CacheService(IMemoryCache cache)
        //{
        //    _cache = cache;
        //}
        public static List<Quote> Quotes
        {
            get
            {
                List<Quote> list;
                if(!Dependencies.MemoryCache.TryGetValue("Quotes", out list))
                {
                    var cacheEntryOptions = new MemoryCacheEntryOptions()
                        .SetSlidingExpiration(TimeSpan.FromHours(1))
                        .AddExpirationToken(new CancellationChangeToken(new CancellationTokenSource(TimeSpan.FromHours(1)).Token));

                    Dependencies.MemoryCache.Set("Quotes", list, cacheEntryOptions);
                }

                return list;
            }
        }
    }
}
