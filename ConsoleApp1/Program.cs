using ConsoleApp1.Contracts.Input;
using ConsoleApp1.Contracts.Output;
using ConsoleApp1.Core;


var jsonOperatorInput = new JsonOperator<In>();

In inData;

try
{
    inData = jsonOperatorInput.DeserializeJson("in.json");
}
catch (InvalidOperationException ex)
{
    Console.WriteLine(ex.Message);
    throw;
}
catch (FileNotFoundException ex)
{
    Console.WriteLine($"File not found, {ex.Message}");
    throw;
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
    throw;
}

var agvCount = inData.AgvCount;
var taktTimes = inData.TaktTimes.OrderBy(t => t.Time).ToList();
var distances = inData.Distances;

var avgsMap = Generators.GenerateAvgsMap(agvCount);
var avgLastTimeMap = Generators.GenerateAvgLastTimeMap(agvCount);

var taktMapCalculator = new TaktMapCalculator();
var transportTimesMapCalculator = taktMapCalculator.TransportTimesMapCalculator(taktTimes, distances, true);

var groupTaktTimes = taktTimes.GroupBy(t => t.Time, new DeltaTaktComparer(AgvConstants.TaktDelta));

var avgDistributor = new AvgDistributor();
var avgsDistribution = avgDistributor.FillAvgsMap(groupTaktTimes, avgsMap, transportTimesMapCalculator, avgLastTimeMap);

var transportOrders = taktMapCalculator.CalulateTaktMapCalculator(taktTimes, distances, avgsDistribution);

var usedAgvCount = transportOrders.Select(t => t.Agv).Distinct().Count();
var totalTaktTime = taktTimes.Last().Time;
var totalDeliveryTime = transportOrders.Last().Time;
var penaltyTime = totalDeliveryTime - totalTaktTime;

var outData = new Out
{
    TransportOrders = transportOrders.Select(t => new TransportOrder
    {
        From = t.From,
        To = t.To,
        Agv = t.Agv,
        Time = t.Time
    }).ToList(),
    MinimumAgvCount = usedAgvCount,
    IdleTime = (agvCount - usedAgvCount) * totalTaktTime, // + used avg time in depo
    PenaltyTime = penaltyTime > 0 ? penaltyTime : 0
};

var jsonCreator = new JsonOperator<Out>();
try
{
    jsonCreator.CreateOutput(outData, "Out.json");
}
catch (Exception ex)
{
    Console.WriteLine($"Output json was not possible to create, {ex.Message}");
    throw;
}
