namespace WeatherBotService.WeatherSender;

public interface ISender
{
    public Task SendAsync(IBotApi botApi, IBotUser user);
}
