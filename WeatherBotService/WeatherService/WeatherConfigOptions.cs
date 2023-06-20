using System.ComponentModel.DataAnnotations;

namespace WeatherBotService.WeatherService;

public class WeatherConfigOptions
{
    public const string OptionsName = "WeatherConfig";
    [Required]
    public string WeatherApiKey { get; set; } = string.Empty;
    [Required]
    public string WeatherQuery { get; set; } = string.Empty;
}