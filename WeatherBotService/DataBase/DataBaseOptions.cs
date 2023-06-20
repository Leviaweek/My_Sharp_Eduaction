using System.ComponentModel.DataAnnotations;
namespace WeatherBotService.DataBase;

public class DataBaseOptions
{
    public const string OptionsName = "DataBaseOptions";
    [Required]
    public string FileName { get; set; } = string.Empty;
}
