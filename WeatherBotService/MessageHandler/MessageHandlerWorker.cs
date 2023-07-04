using System.Net;
using System.Text.Json;
using Microsoft.Extensions.Options;
using WeatherBotService.MessageHandler.Commands;

namespace WeatherBotService.MessageHandler;

public class MessageHandlerWorker: BackgroundService, IMessageHandler
{
    private readonly ILogger<MessageHandlerWorker> _logger;
    private readonly IBotApi _telegramBotApi;
    private readonly TelegramBotOptions _options;
    private const string Url = "https://api.telegram.org/bot{0}/{1}?offset={2}";
    private int _offset;
    private readonly List<Command> _commands;
    public MessageHandlerWorker(ILogger<MessageHandlerWorker> logger, IBotApi telegramBotApi, IOptions<TelegramBotOptions> options, IDataBase<TelegramBotUser> database)
    {
        _logger = logger;
        _telegramBotApi = telegramBotApi;
        _options = options.Value;
        _offset = 0;
        _commands = new()
        {
            new RegisterChatCommand(database, _telegramBotApi),
            new UnregisterChatCommand(database, _telegramBotApi)
        };
    }
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            await CheckChatsAsync(stoppingToken);
            await Task.Delay(500, stoppingToken);
        }
        _logger.LogDebug("MessageHandlerWorker is stopping");
    }
    public async Task CheckChatsAsync(CancellationToken cancellationToken)
    {
        using var httpClient = new HttpClient();
        while (!cancellationToken.IsCancellationRequested)
        {
            var apiUrl = string.Format(Url, _options.Token, "getUpdates", _offset);
            var response = await httpClient.GetAsync(apiUrl, cancellationToken);
            var json = await response.Content.ReadAsStringAsync(cancellationToken);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                _logger.LogError("TelegramBotApi.CheckChats: {json}", json);
                return;
            }
            var telegramBotResponse = JsonSerializer.Deserialize<TelegramBotResponse>(json) ?? throw new ArgumentNullException(nameof(json), "TelegramBotApi.CheckChats: json is null");
            if (telegramBotResponse.Result.Count == 0)
            {
                return;
            }
            await HandleResponseAsync(telegramBotResponse);
            _offset = telegramBotResponse.Result.Max(x => x.UpdateId) + 1;
            _logger.LogInformation("success");
        }
    }
    public async Task HandleResponseAsync(TelegramBotResponse telegramBotResponse)
    {
        foreach (var result in telegramBotResponse.Result)
            foreach (var command in _commands)
                if (command.Check(result))
                    await command.ExecuteAsync(result);
    }
}
