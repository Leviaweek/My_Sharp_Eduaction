using System.Text.Json.Serialization;
using System.Globalization;

namespace WeatherBotService.Models;

[Serializable]
public record class WeatherJson
{
    [JsonPropertyName("mainInfo")]
    public required string MainInfo { get; set; }
    [JsonPropertyName("temp")]
    public required string Temp { get; set; }
    [JsonPropertyName("humidity")]
    public required string Humidity { get; set; }
    [JsonPropertyName("grnd_level")]
    public required string GrndLevel { get; set; }
    [JsonPropertyName("windSpeed")]
    public required string WindSpeed { get; set; }
    [JsonPropertyName("dateTime")]
    public required DateTimeOffset DateTime { get; set; }

    public static WeatherJson ConvertFrom(WeatherResponse weatherResponse)
    {
        var dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(weatherResponse.DateTime);

        return new WeatherJson
        {
            MainInfo = weatherResponse.Weather[0].Main,
            Temp = weatherResponse.Main.Temp.ToString(CultureInfo.InvariantCulture),
            Humidity = weatherResponse.Main.Humidity.ToString(CultureInfo.InvariantCulture),
            GrndLevel = weatherResponse.Main.GrndLevel.ToString(CultureInfo.InvariantCulture),
            WindSpeed = weatherResponse.Wind.Speed.ToString(CultureInfo.InvariantCulture),
            DateTime = dateTimeOffset
        };
    }
}
