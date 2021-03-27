using StockSimulator.Domain.ValuableObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace StockSimulator.Data.DataStorage
{
    public static class DataManager
    {
        public static List<ChartModel> GetData()
        {
            var r = new Random();
            return new List<ChartModel>()
            {
                new ChartModel { Data = new List<int> { r.Next(1,40) }, Label = "Data 1" },
                new ChartModel { Data = new List<int> { r.Next(1,40) }, Label = "Data 2" },
                new ChartModel { Data = new List<int> { r.Next(1,40) }, Label = "Data 3" },
                new ChartModel { Data = new List<int> { r.Next(1,40) }, Label = "Data 4" },
            };
        }
    }
}
