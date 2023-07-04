namespace WeatherBotService.WeatherSender;

public class WeatherSenderWorker : BackgroundService
{
    private readonly ILogger<WeatherSenderWorker> _logger;
    private readonly IBotApi _telegramBotApi;
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly IDataBase<TelegramBotUser> _dataBase;

    public WeatherSenderWorker(ILogger<WeatherSenderWorker> logger, IBotApi telegramBotApi, IServiceScopeFactory serviceScopeFactory, IDataBase<TelegramBotUser> dataBase)
    {
        _logger = logger;
        _telegramBotApi = telegramBotApi;
        _serviceScopeFactory = serviceScopeFactory;
        _dataBase = dataBase;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await _dataBase.OpenConnectionAsync(stoppingToken);
        await _dataBase.CreateTableAsync(stoppingToken);
        var periodicTimer = new PeriodicTimer(TimeSpan.FromMinutes(1));
        while (!stoppingToken.IsCancellationRequested)
        {
            await using var scope = _serviceScopeFactory.CreateAsyncScope();
            var serviceProvider = scope.ServiceProvider;
            var weatherSender = serviceProvider.GetRequiredService<ISender>();
            var allUsers = await _dataBase.GetAllUsersAsync(stoppingToken);
            _logger.LogInformation("Succesfull");
            foreach (var user in allUsers)
            {
                await weatherSender.SendAsync(_telegramBotApi, user.ChatId, stoppingToken);
            }
            _logger.LogInformation("Message at {time} is succesfull", DateTimeOffset.Now);
            await periodicTimer.WaitForNextTickAsync(stoppingToken);
        }
        await _dataBase.CloseConnectionAsync(stoppingToken);
    }
}