using Newtonsoft.Json;

namespace ConsoleApp1.Contracts.Input;

public class Distance
{
    [JsonProperty("from")]
    public int From { get; set; }
    [JsonProperty("to")]
    public int To { get; set; }
    [JsonProperty("distance")]
    public long Dist { get; set; }
}