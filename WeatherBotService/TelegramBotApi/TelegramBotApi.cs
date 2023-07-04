using Microsoft.Extensions.Options;
using System.Text.Json;
using System.Text;
using System.Net;

namespace WeatherBotService.TelegramBotApi;

public class TelegramBotApi : IBotApi
{
    private readonly TelegramBotOptions _options;
    private const string Url = "https://api.telegram.org/bot{0}/{1}";
    private readonly ILogger<TelegramBotApi> _logger;
    public TelegramBotApi(IOptions<TelegramBotOptions> options, ILogger<TelegramBotApi> logger)
    {
        _options = options.Value;
        _logger = logger;
    }

    public async Task SendMessageAsync(int chatId, string message, CancellationToken cancellationToken = default)
    {
        var apiUrl = string.Format(Url, _options.Token, "sendMessage");
        using var httpClient = new HttpClient();
        var json = JsonSerializer.Serialize(new TelegramBotRequest { ChatId = chatId, Text = message });
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await httpClient.PostAsync(apiUrl, content, cancellationToken);
        if (response.StatusCode != HttpStatusCode.OK)
        {
            _logger.LogError("Error sending message {message} to chat {chatId}", message, chatId);
            return;
        }
        _logger.LogInformation("Message sent to chat {chatId}", chatId);
    }
}
