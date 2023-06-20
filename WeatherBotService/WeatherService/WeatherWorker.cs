using System.Text.Json;
namespace WeatherBotService.WeatherService;

public class WeatherWorker : BackgroundService
{
    private readonly ILogger<WeatherWorker> _logger;
    private readonly IWeatherRequester _weatherRequest;

    public WeatherWorker(ILogger<WeatherWorker> logger, IWeatherRequester weatherRequest)
    {
        _logger = logger;
        _weatherRequest = weatherRequest;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var periodicTimer = new PeriodicTimer(TimeSpan.FromMinutes(25));
            var weather = await _weatherRequest.GetWeatherAsync();
            _logger.LogInformation("{weather}", weather);
            var weatherJson = WeatherJson.ConvertFrom(weather);
            File.WriteAllText("weather.json", JsonSerializer.Serialize(weatherJson));
            await periodicTimer.WaitForNextTickAsync(stoppingToken);
        }
    }
}
