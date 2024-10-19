using ConsoleApp1.Contracts.Input;
using ConsoleApp1.Contracts.Output;

namespace ConsoleApp1.Core.Interfaces;

public interface ITaktMapCalculator
{
    List<TransportOrderMidProduct> CalulateTaktMapCalculator(List<TaktTime> taktTimes, List<Distance> distances,
        Dictionary<int, (int? avg, long? transportTime)> avgsMap);

    List<long> TransportTimesMapCalculator(List<TaktTime> taktTimes, List<Distance> distances,
        bool startBefore = false);
}