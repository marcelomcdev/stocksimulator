using Newtonsoft.Json;

namespace StockSimulator.Application.Security
{
    public class SessionToken
    {
        [JsonProperty("access-token")]
        public string AccessToken { get; set; }

        [JsonProperty("client")]
        public string Client { get; set; }

        [JsonProperty("uid")]
        public string UID { get; set; }
    }
}
