using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using StockSimulator.Data.DataStorage;
using StockSimulator.Service.HubConfig;
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

        public ChartController(IHubContext<ChartHub> hub)
        {
            _hub = hub;
        }

        public IActionResult Get()
        {
            var timerManager = new TimerManager(()=> _hub.Clients.All.SendAsync("transferchartdata", DataManager.GetData()));
            return Ok(new { Message = "Request Completed" });
        }
    }
}
