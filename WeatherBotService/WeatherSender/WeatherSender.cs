using System.Text.Json;
namespace WeatherBotService.WeatherSender;

public class WeatherSender: ISender
{
    private readonly ILogger<WeatherSender> _logger;
    public WeatherSender(ILogger<WeatherSender> logger)
    {
        _logger = logger;
    }

    public async Task SendAsync(IBotApi botApi, int chatId, CancellationToken cancellationToken = default)
    {
        if (!File.Exists("weather.json"))
        {
            _logger.LogError("Error sending weather: weather.json not found");
            return;
        }
        var text = await File.ReadAllTextAsync(WeatherWorker.WeatherFileName, cancellationToken);
        var weather = JsonSerializer.Deserialize<WeatherJson>(text) ?? throw new ArgumentNullException(text);
        var messageText = $"""
            Main info: {weather.MainInfo}
            Temperature: {weather.Temp}
            Humidity: {weather.Humidity}
            Wind speed: {weather.WindSpeed}
            Ground level: {weather.GrndLevel}
            Date and time: {weather.DateTime:yyyy-MM-ddTHH:mm:ssZ}
            """;
        await botApi.SendMessageAsync(chatId, messageText, cancellationToken);
        _logger.LogInformation("Message sended");
    }
}
