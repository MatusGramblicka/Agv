using ConsoleApp1.Contracts.Input;
using ConsoleApp1.Core.Interfaces;

namespace ConsoleApp1.Core;

public class AvgDistributor : IAvgDistributor
{
    /// <summary>
    /// Prepare agv map with distribution with last transportation time
    /// </summary>
    /// <param name="groupTaktTimes"></param>
    /// <param name="avgsMap"></param>
    /// <param name="transportTimesMapCalculator"></param>
    /// <param name="avgLastTimeMap"></param>
    /// <returns>Distribution of agvs</returns>
    public Dictionary<int, int?> FillAvgsMap(IEnumerable<IGrouping<double, TaktTime>> groupTaktTimes,
        Dictionary<int, (int? avg, long? transportTime)> avgsMap, List<long> transportTimesMapCalculator,
        Dictionary<int, long?> avgLastTimeMap)
    {
        var index = 1;

        foreach (var groupTaktTime in groupTaktTimes)
        {
            Console.WriteLine(groupTaktTime.Key);

            var avgIndex = 1;
            foreach (var _ in groupTaktTime)
            {
                avgsMap[index] = (avgIndex, transportTimesMapCalculator[index - 1]);
                if (avgLastTimeMap[avgIndex].HasValue)
                {
                    while (avgsMap[index].transportTime < avgLastTimeMap[avgIndex].Value)
                    {
                        avgIndex++;
                    }

                    avgLastTimeMap[avgIndex] = avgsMap[index].transportTime;

                    avgIndex++;
                    index++;

                    continue;
                }

                avgLastTimeMap[avgIndex] = avgsMap[index].transportTime;

                avgIndex++;
                index++;
            }
        }

        return avgsMap.Select(a => (a.Key, a.Value.avg)).ToDictionary();
    }
}