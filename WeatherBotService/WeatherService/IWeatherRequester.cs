namespace WeatherBotService.WeatherService;

public interface IWeatherRequester
{
    public Task<WeatherResponse> GetWeatherAsync();
}

