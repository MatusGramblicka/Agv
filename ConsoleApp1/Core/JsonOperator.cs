using Newtonsoft.Json;

namespace ConsoleApp1.Core;

public class JsonOperator<T> where T : class
{
    public T DeserializeJson(string jsonFileName)
    {
        return JsonConvert.DeserializeObject<T>(File.ReadAllText(jsonFileName)) ??
               throw new InvalidOperationException();
    }

    public void CreateOutput(T outData, string outJson)
    {
        if (string.IsNullOrEmpty(outJson))
            throw new Exception();
        
        var jsonString = System.Text.Json.JsonSerializer.Serialize(outData);
        File.WriteAllText(outJson, jsonString);
    }
}