using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using StockSimulator.Domain.Interfaces.Repository;
using StockSimulator.Domain.Interfaces.Services;
using StockSimulator.Domain.Interfaces.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StockSimulator.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperationController : ControllerBase
    {
        private readonly IOperationService _operationService;
        private readonly IMemoryCache _cache;
        private readonly ISignalRService _signalRService;


        public OperationController(IOperationService operationService, IMemoryCache cache, ISignalRService signalRService)
        {
            _operationService = operationService;
            _cache = cache;
            _signalRService = signalRService;
        }

        // GET: api/<OperationController>
        [HttpGet]
        public IEnumerable<dynamic> GetOperationsMostNegotiated()
        {
            var cacheEntry = _cache.GetOrCreate("MyCacheKey", entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30);
                entry.SetPriority(CacheItemPriority.High);
                return _operationService.GetMostTradedOperations();
            });

            _signalRService.SendMessage("mensagem");

            return cacheEntry;
        }

    }
}
