namespace ConsoleApp1.Core;

public static class Generators
{
    public static Dictionary<int, (int? avg, long? transportTime)> GenerateAvgsMap(int agvCount)
    {
        var avgsMap = new Dictionary<int, (int? avg, long? transportTime)>();
        for (int i = 1; i <= agvCount; i++)
        {
            avgsMap.Add(i, (null, null));
        }

        return avgsMap;
    }

    public static Dictionary<int, long?> GenerateAvgLastTimeMap(int agvCount)
    {
        var avgLastTimeMap = new Dictionary<int, long?>();
        for (int i = 1; i <= agvCount; i++)
        {
            avgLastTimeMap.Add(i, null);
        }
        return avgLastTimeMap;
    }
}