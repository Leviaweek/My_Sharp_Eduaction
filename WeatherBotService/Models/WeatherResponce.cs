using System.Text.Json.Serialization;

namespace WeatherBotService.Models;

[Serializable]
public record class WeatherResponse
{

    [JsonPropertyName("name")]
    public required string Name { get; set; }
    [JsonPropertyName("main")]
    public required WeatherResponseBody Main { get; set; }
    [JsonPropertyName("weather")]
    public required List<WeatherMainInfo> Weather { get; set; }
    [JsonPropertyName("wind")]
    public required WeatherWindInfo Wind { get; set; }
    [JsonPropertyName("dt")]
    public required long DateTime { get; set; }
}
