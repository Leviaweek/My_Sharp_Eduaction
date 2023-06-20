using System.Text.Json.Serialization;

namespace WeatherBotService.Models;

[Serializable]
public record class TelegramBotRequest
{
    [JsonPropertyName("chat_id")]
    public required int ChatId { get; set; }
    [JsonPropertyName("text")]
    public required string Text { get; set; }
}
