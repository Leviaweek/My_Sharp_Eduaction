using System.Text.Json;
namespace WeatherBotService.WeatherSender;

public class WeatherSender: ISender
{
    private readonly ILogger<WeatherSender> _logger;
    public WeatherSender(ILogger<WeatherSender> logger)
    {
        _logger = logger;
    }

    public async Task SendAsync(IBotApi botApi, IBotUser user)
    {
        if (!File.Exists("weather.json"))
        {
            _logger.LogError("Error sending weather: weather.json not found");
            return;
        }
        var text = await File.ReadAllTextAsync("weather.json");
        var weather = JsonSerializer.Deserialize<WeatherJson>(text) ?? throw new ArgumentNullException(text);
        var messageText = $"Main info: {weather.MainInfo}\n"+
            $"Temperature: {weather.Temp}\n"+
            $"Humidity: {weather.Humidity}\n"+
            $"Wind speed: {weather.WindSpeed}\n"+
            $"Ground level: {weather.GrndLevel}\n" +
            $"Date and time: {weather.DateTime}";
        await botApi.SendMessageAsync(user.ChatId, messageText);
        _logger.LogInformation("Message sended");
    }
}
