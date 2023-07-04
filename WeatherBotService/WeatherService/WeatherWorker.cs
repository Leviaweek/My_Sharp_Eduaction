using System.Text.Json;
namespace WeatherBotService.WeatherService;

public class WeatherWorker : BackgroundService
{
    private readonly ILogger<WeatherWorker> _logger;
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public WeatherWorker(ILogger<WeatherWorker> logger, IServiceScopeFactory serviceScopeFactory)
    {
        _logger = logger;
        _serviceScopeFactory = serviceScopeFactory;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var periodicTimer = new PeriodicTimer(TimeSpan.FromMinutes(25));
        while (!stoppingToken.IsCancellationRequested)
        {
            await using var scope = _serviceScopeFactory.CreateAsyncScope();
            var serviceProvider = scope.ServiceProvider;
            var weatherRequester = serviceProvider.GetRequiredService<IWeatherRequester>();
            var weather = await weatherRequester.GetWeatherAsync(stoppingToken);
            _logger.LogInformation("{weather}", weather);
            var weatherJson = WeatherJson.ConvertFrom(weather);
            File.WriteAllText("weather.json", JsonSerializer.Serialize(weatherJson));
            await periodicTimer.WaitForNextTickAsync(stoppingToken);
        }
    }
}
