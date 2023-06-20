namespace WeatherBotService.WeatherSender;

public class WeatherSenderWorker : BackgroundService
{
    private readonly ILogger<WeatherSenderWorker> _logger;
    private readonly IBotApi _telegramBotApi;
    private readonly ISender _weatherSender;
    private readonly IDataBase _dataBase;

    public WeatherSenderWorker(ILogger<WeatherSenderWorker> logger, IBotApi telegramBotApi, ISender weatherSender, IDataBase dataBase)
    {
        _logger = logger;
        _telegramBotApi = telegramBotApi;
        _weatherSender = weatherSender;
        _dataBase = dataBase;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await _dataBase.OpenConnectionAsync();
        await _dataBase.CreateTableAsync();
        while (!stoppingToken.IsCancellationRequested)
        {
            var periodicTimer = new PeriodicTimer(TimeSpan.FromMinutes(1));
            var allUsers = await _dataBase.GetAllUsersAsync();
            _logger.LogInformation("Succesfull");
            foreach (var user in allUsers)
            {
                await _weatherSender.SendAsync(_telegramBotApi, user);
            }
            _logger.LogInformation("Message at {time} is succesfull", DateTimeOffset.Now);
            await periodicTimer.WaitForNextTickAsync(stoppingToken);
        }
    }
}