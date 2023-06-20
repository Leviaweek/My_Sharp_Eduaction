using System.Text.Json.Serialization;

namespace WeatherBotService.Models;

[Serializable]
public record class TelegramBotResponse
{
    [JsonPropertyName("ok")]
    public required bool Ok { get; set; }
    [JsonPropertyName("result")]
    public required List<TelegramBotResponseResult> Result { get; set; }
}
