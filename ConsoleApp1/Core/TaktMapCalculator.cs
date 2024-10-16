using ConsoleApp1.Contracts.Input;
using ConsoleApp1.Contracts.Output;

namespace ConsoleApp1.Core;

public class TaktMapCalculator
{
    public List<TransportOrderMidProduct> CalulateTaktMapCalculator(List<TaktTime> taktTimes, List<Distance> distances)
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
                    Agv = 1,
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
                    Agv = 1,
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
                Agv = 1,
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
                Agv = 1,
                Takt = i
            };

            transportOrders.Add(transportOrderDepoToLine);
            transportOrders.Add(transportOrderFromLineToPickup);
            transportOrders.Add(transportOrderFromPickupToDepo);
        }

        return transportOrders;
    }
}