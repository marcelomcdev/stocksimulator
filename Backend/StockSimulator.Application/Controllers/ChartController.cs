using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using StockSimulator.Data.DataStorage;
using StockSimulator.Domain.Cache;
using StockSimulator.Domain.Interfaces.Services;
using StockSimulator.Service.HubConfig;
using StockSimulator.Service.QuoteSimulator;
using StockSimulator.Service.TimerFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockSimulator.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChartController : Controller
    {
        private IHubContext<ChartHub> _hub;
        private readonly IListenerService _listenerService;

        public ChartController(IHubContext<ChartHub> hub, IListenerService listenerService)
        {
            _hub = hub;
            _listenerService = listenerService;
        }


        [HttpGet("listen")]
        public IActionResult Listen()
        {           
            return Ok(new { Message = "Request Completed" });
        }

        [HttpGet]
        public IActionResult Get()
        {
            if (!_listenerService.Started)
            {
                _listenerService.Listen("ws://localhost:8080/quotes");
                var timerManager = new TimerManager(() => _hub.Clients.All.SendAsync("transferchartdata", _listenerService.GetMostTradedOperations(5)), 3);
            }
            return Ok(new { Message = "Request Completed" });
        }
    }
}
