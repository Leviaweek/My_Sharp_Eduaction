var builder = Host.CreateDefaultBuilder(args);
builder.ConfigureServices((context, services) =>
{
    services.AddHostedService<WeatherWorker>();
    services.AddHostedService<WeatherSenderWorker>();
    services.AddHostedService<MessageHandlerWorker>();
    services.AddOptions<TelegramBotOptions>()
    .Bind(context.Configuration.GetSection(TelegramBotOptions.OptionsName))
    .ValidateOnStart();
    services.AddOptions<WeatherConfigOptions>()
    .Bind(context.Configuration.GetSection(WeatherConfigOptions.OptionsName))
    .ValidateOnStart();
    services.AddOptions<DataBaseOptions>()
    .Bind(context.Configuration.GetSection(DataBaseOptions.OptionsName))
    .ValidateOnStart();
    services.AddScoped<IWeatherRequester, WeatherRequester>();
    services.AddSingleton<IMessageHandler, MessageHandlerWorker>();
    services.AddSingleton<IBotApi, TelegramBotApi>();
    services.AddScoped<ISender, WeatherSender>();
    services.AddSingleton<IDataBase<TelegramBotUser>, DataBase>();
});
var app = builder.Build();
await app.RunAsync();
