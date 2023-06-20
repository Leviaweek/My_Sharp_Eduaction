using System.ComponentModel.DataAnnotations;
namespace WeatherBotService.TelegramBotApi;

public class TelegramBotOptions
{
    public const string OptionsName = "TelegramBot";
    [Required]
    [StringLength(100, MinimumLength = 6)]
    public string Token { get; set; } = "";
}
