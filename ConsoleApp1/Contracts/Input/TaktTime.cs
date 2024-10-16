using Newtonsoft.Json;

namespace ConsoleApp1.Contracts.Input;

public class TaktTime
{
    [JsonProperty("from")]
    public int From { get; set; }
    [JsonProperty("to")]
    public int To { get; set; }
    [JsonProperty("time")]
    public long Time { get; set; }
}