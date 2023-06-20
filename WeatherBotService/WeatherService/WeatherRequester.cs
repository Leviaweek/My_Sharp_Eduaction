using System.Text.Json;
using Microsoft.Extensions.Options;
namespace WeatherBotService.WeatherService;

public class WeatherRequester: IWeatherRequester
{
    private readonly ILogger<WeatherRequester> _logger;
    private readonly WeatherConfigOptions _appConfig;
    private const string _url = "http://api.openweathermap.org/data/2.5/weather?units=metric&q={0}&appid={1}";
    public WeatherRequester(ILogger<WeatherRequester> logger, IOptions<WeatherConfigOptions> appConfig)
    {
        _logger = logger;
        _appConfig = appConfig.Value;
    }
    public async Task<WeatherResponse> GetWeatherAsync()
    {
        var url = string.Format(_url, _appConfig.WeatherQuery, _appConfig.WeatherApiKey);
        _logger.LogDebug("Requesting weather from {url}", url);
        using var client = new HttpClient();
        var response = await client.GetStringAsync(url);
        var weatherResponse = JsonSerializer.Deserialize<WeatherResponse>(response);
        if (weatherResponse is null)
        {
            _logger.LogError("Failed to deserialize weather response");
            throw new NullReferenceException("Failed to deserialize weather response");
        }
        _logger.LogDebug("Received weather response: {weatherResponse}", weatherResponse);
        return weatherResponse;
    }
}
