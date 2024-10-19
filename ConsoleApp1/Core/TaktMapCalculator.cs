using ConsoleApp1.Contracts.Input;
using ConsoleApp1.Contracts.Output;
using ConsoleApp1.Core.Interfaces;

namespace ConsoleApp1.Core;

public class TaktMapCalculator : ITaktMapCalculator
{
    public List<TransportOrderMidProduct> CalulateTaktMapCalculator(List<TaktTime> taktTimes, List<Distance> distances,
        Dictionary<int, (int? avg, long? transportTime)> avgsMap)
    {
        var transportOrders = new List<TransportOrderMidProduct>();

        long timeTransportFromPickupToDepo = 0;

        foreach (var (time, i) in taktTimes.Select((time, i) => (time, i)))
        {
            var distFromDepoToLine = distances.GetDistance(0, time.From);
            var timeFromDepoToLine = distFromDepoToLine.GetTransportTime();

            TransportOrderMidProduct transportOrderDepoToLine;

            if (i > 0)
            {
                transportOrderDepoToLine = new TransportOrderMidProduct
                {
                    Time = transportOrders.Last().Time + timeTransportFromPickupToDepo,
                    From = 0,
                    To = time.From,
                    Agv = avgsMap[i + 1].avg ?? 1,
                    Takt = i,
                };
            }
            else
            {
                transportOrderDepoToLine = new TransportOrderMidProduct
                {
                    Time = taktTimes.First().Time - timeFromDepoToLine,
                    From = 0,
                    To = time.From,
                    Agv = avgsMap[i + 1].avg ?? 1,
                    Takt = i
                };
            }

            var distFromLineToPickup = distances.GetDistance(time.From, time.To);
            var timeFromLineToPickup = distFromLineToPickup.GetTransportTime();

            var transportOrderFromLineToPickup = new TransportOrderMidProduct
            {
                Time = transportOrderDepoToLine.Time + timeFromDepoToLine,
                From = time.From,
                To = time.To,
                Agv = avgsMap[i + 1].avg ?? 1,
                Takt = i,
                MidTime = transportOrderDepoToLine.Time + timeFromDepoToLine
            };

            var distFromPickupToDepo = distances.GetDistance(time.To, 0);
            timeTransportFromPickupToDepo = distFromPickupToDepo.GetTransportTime();

            var transportOrderFromPickupToDepo = new TransportOrderMidProduct
            {
                Time = transportOrderFromLineToPickup.Time + timeFromLineToPickup,
                From = time.To,
                To = 0,
                Agv = avgsMap[i + 1].avg ?? 1,
                Takt = i
            };

            transportOrders.Add(transportOrderDepoToLine);
            transportOrders.Add(transportOrderFromLineToPickup);
            transportOrders.Add(transportOrderFromPickupToDepo);
        }

        return transportOrders;
    }

    public List<long> TransportTimesMapCalculator(List<TaktTime> taktTimes, List<Distance> distances,
        bool startBefore = false)
    {
        var lastTimesOfTransport = new List<long>();

        long timeTransportFromPickupToDepo;

        foreach (var time in taktTimes)
        {
            var distFromDepoToLine = distances.GetDistance(0, time.From);
            var timeFromDepoToLine = distFromDepoToLine.GetTransportTime();
            
            if (!startBefore)
            {
               // todo do not subtract
            }
            else
            {
                timeFromDepoToLine = time.Time - timeFromDepoToLine;
            }

            var distFromLineToPickup = distances.GetDistance(time.From, time.To);
            var timeFromLineToPickup = distFromLineToPickup.GetTransportTime();
            
            var distFromPickupToDepo = distances.GetDistance(time.To, 0);
            timeTransportFromPickupToDepo = distFromPickupToDepo.GetTransportTime();
            
            lastTimesOfTransport.Add(timeFromDepoToLine + timeFromLineToPickup +
                                     timeTransportFromPickupToDepo);
        }

        return lastTimesOfTransport;
    }
}