using Newtonsoft.Json;

namespace ConsoleApp1.Contracts.Input;

public class In
{
    [JsonProperty("agvCount")]
    public int AgvCount { get; set; }

    [JsonProperty("totalDurationTime")]
    public long TotalDurationTime { get; set; }

    [JsonProperty("taktTimes")]
    public List<TaktTime> TaktTimes { get; set; } = new();

    [JsonProperty("distances")] 
    public List<Distance> Distances { get; set; } = new();
}