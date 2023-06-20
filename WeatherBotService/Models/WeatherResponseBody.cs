using System.Text.Json.Serialization;

namespace WeatherBotService.Models;
[Serializable]
public record class WeatherResponseBody
{
    [JsonPropertyName("temp")]    
    public required double Temp { get; set; }
    [JsonPropertyName("humidity")]
    public required int Humidity { get; set; }
    [JsonPropertyName("grnd_level")]
    public required int GrndLevel { get; set; }
}
