using System.Text.Json.Serialization;

namespace WeatherBotService.Models;

[Serializable]
public record class WeatherMainInfo
{
    [JsonPropertyName("main")] public required string Main { get; set; }
}
