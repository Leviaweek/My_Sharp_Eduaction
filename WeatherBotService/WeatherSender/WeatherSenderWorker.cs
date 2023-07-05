namespace WeatherBotService.WeatherSender;

public class WeatherSenderWorker : BackgroundService
{
    private readonly ILogger<WeatherSenderWorker> _logger;
    private readonly IBotApi _telegramBotApi;
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly ISender _weatherSender;

    public WeatherSenderWorker(ILogger<WeatherSenderWorker> logger, IBotApi telegramBotApi, IServiceScopeFactory serviceScopeFactory, ISender weatherSender)
    {
        _logger = logger;
        _telegramBotApi = telegramBotApi;
        _serviceScopeFactory = serviceScopeFactory;
        _weatherSender = weatherSender;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await using var scope = _serviceScopeFactory.CreateAsyncScope();
        await using var database = scope.ServiceProvider.GetRequiredService<IDataBase<TelegramBotUser>>();
        await database.OpenConnectionAsync(stoppingToken);
        var periodicTimer = new PeriodicTimer(TimeSpan.FromMinutes(1));
        while (!stoppingToken.IsCancellationRequested)
        {
            var allUsers = await database.GetAllUsersAsync(stoppingToken);
            _logger.LogInformation("Succesfull");
            foreach (var user in allUsers)
            {
                await _weatherSender.SendAsync(_telegramBotApi, user.ChatId, stoppingToken);
            }
            _logger.LogInformation("Message at {time} is succesfull", DateTimeOffset.Now);
            await periodicTimer.WaitForNextTickAsync(stoppingToken);
        }
    }
}