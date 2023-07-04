namespace WeatherBotService.TelegramBotApi;

public interface IBotApi
{
    public Task SendMessageAsync(int chatId, string message, CancellationToken cancellationToken = default);
}
