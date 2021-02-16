using System;
using System.Collections.Generic;
using System.Text;

namespace StockSimulator.CrossCutting.Configuration
{
    public class GeneralConfig
    {
        public string Secret { get; set; }
        public int ExpirationHours { get; set; }
        public string Issuer { get; set; }
        public IEnumerable<string> ValidIn { get; set; }
    }
}
