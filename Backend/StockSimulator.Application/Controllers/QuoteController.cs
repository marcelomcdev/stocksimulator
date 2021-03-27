using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using StockSimulator.Domain.Interfaces.Services;
using StockSimulator.Domain.Interfaces.SignalR;
using StockSimulator.Domain.ValuableObjects;
using StockSimulator.Service.Cache;
using StockSimulator.Service.TimerFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockSimulator.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuoteController : ControllerBase
    {
        private readonly ISignalRService _signalRService;
        private readonly IListenerService _listenerService;
        private readonly IMemoryCache _cache;
        public QuoteController(ISignalRService signalRService, IListenerService listenerService, IMemoryCache cache)
        {
            _signalRService = signalRService;
            _listenerService = listenerService;
            _cache = cache;
        }
       
        [HttpGet]
        public IActionResult GetQuotes()
        {
            _listenerService.Listen("ws://localhost:8080/quotes");

            //var cacheEntry = _cache.GetOrCreate("Quotes", entry =>
            //{
            //    entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30);
            //    entry.SetPriority(CacheItemPriority.High);
            //    return Items;
            //});

            var quotes = _cache.Get<List<Quote>>("Quotes");

            var timermanager = new TimerManager(()=> _signalRService.SendQuotes(quotes));
            return Ok( new { Message = "Request Complete", Quotes = quotes });
        }
    }
}
