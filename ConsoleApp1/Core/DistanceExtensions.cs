using ConsoleApp1.Contracts.Input;

namespace ConsoleApp1.Core;

public static class DistanceExtensions
{
    public static long GetDistance(this IEnumerable<Distance> distances, int from, int to)
    {
        if (distances == null)
            return 0;

        long distance = 0;

        if (distances.Any(d => d.From == from && d.To == to))
            distance = distances.Single(d => d.From == from && d.To == to).Dist;
        else if (distances.Any(d => d.From == to && d.To == from))
            distance = distances.Single(d => d.From == to && d.To == from).Dist;

        return distance;
    }

    public static long GetTransportTime(this long distance)
    {
        return distance * AgvConstants.AgvSpeedMeterPerSecond;
    }
}