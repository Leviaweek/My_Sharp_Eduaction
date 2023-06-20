using System.Text.Json.Serialization;

namespace WeatherBotService.Models;

[Serializable]
public record class TelegramBotResponseResult
{
    [JsonPropertyName("update_id")]
    public required int UpdateId { get; set; }
    [JsonPropertyName("message")]
    public required TelegramBotResponseMessage Message { get; set; }
}
