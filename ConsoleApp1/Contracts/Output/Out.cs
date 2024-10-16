using Newtonsoft.Json;

namespace ConsoleApp1.Contracts.Output;

public class Out
{
    [JsonProperty("transportOrders")]
    public List<TransportOrder> TransportOrders { get; set; } = new();

    [JsonProperty("idleTime")]
    public long IdleTime { get; set; }

    [JsonProperty("penaltyTime")]
    public long PenaltyTime { get; set; }

    [JsonProperty("minimumAgvCount")]
    public int MinimumAgvCount { get; set; }
}