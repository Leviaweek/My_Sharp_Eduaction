namespace WeatherBotService.WeatherSender;

public interface ISender
{
    public Task SendAsync(IBotApi botApi, int chatId, CancellationToken cancellationToken = default);
}
