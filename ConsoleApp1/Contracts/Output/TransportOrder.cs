using Newtonsoft.Json;

namespace ConsoleApp1.Contracts.Output
{
    public class TransportOrder
    {
        [JsonProperty("time")]
        public long Time { get; set; }

        [JsonProperty("agv")]
        public int Agv { get; set; }
        
        [JsonProperty("from")]
        public int From { get; set; }

        [JsonProperty("to")]
        public int To { get; set; }       
    }
}
