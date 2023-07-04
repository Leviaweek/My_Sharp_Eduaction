namespace WeatherBotService.MessageHandler.Commands;

public abstract class Command
{
    protected IDataBase<TelegramBotUser> _dataBase;

    public Command(IDataBase<TelegramBotUser> dataBase)
    {
        _dataBase = dataBase;
    }
    public abstract Task ExecuteAsync(TelegramBotResponseResult result);
    public abstract bool Check(TelegramBotResponseResult result);
}
