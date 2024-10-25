using ConsoleApp1.Contracts.Input;

namespace ConsoleApp1.Core.Interfaces;

public interface IAvgDistributor
{
    Dictionary<int, int?>  FillAvgsMap(IEnumerable<IGrouping<double, TaktTime>> groupTaktTimes,
        Dictionary<int, (int? avg, long? transportTime)> avgsMap, List<long> transportTimesMapCalculator,
        Dictionary<int, long?> avgLastTimeMap);
}