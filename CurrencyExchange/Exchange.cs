using System.Text.Json;
using System.Text.Json.Serialization;
namespace CurrencyExchange;

public class Currency
{
    [JsonPropertyName("name")]
    public string Name { get; }
    [JsonPropertyName("rate")]
    public double Rate { get; }

    public Currency(string name, double rate)
    {
        Name = name;
        Rate = rate;
    }

    public override string ToString()
    {
        return $"{Name} {Rate}";
    }
}