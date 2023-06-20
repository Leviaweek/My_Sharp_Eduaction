using System.Text.Json.Serialization;

namespace WeatherBotService.Models;

[Serializable]
public record class TelegramBotResponseChat
{
    [JsonPropertyName("id")]
    public required int Id { get; set; }
}