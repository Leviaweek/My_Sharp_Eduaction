using System.Text.Json.Serialization;

namespace WeatherBotService.Models;

[Serializable]
public record class TelegramBotResponseMessage
{
    [JsonPropertyName("chat")]
    public required TelegramBotResponseChat Chat { get; set; }
    [JsonPropertyName("text")]
    public required string Text { get; set; }
}
