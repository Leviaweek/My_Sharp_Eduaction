using System.Text.Json.Serialization;
namespace WeatherBotService.Models;

[Serializable]
public record class WeatherWindInfo
{
    [JsonPropertyName("speed")] public required double Speed { get; set; }
}
