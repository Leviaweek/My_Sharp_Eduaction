namespace WeatherBotService.MessageHandler.Commands;

public abstract class Command
{
    public abstract Task ExecuteAsync(TelegramBotResponseResult result, IDataBase<TelegramBotUser> dataBase);
    public abstract bool Check(TelegramBotResponseResult result);
}
