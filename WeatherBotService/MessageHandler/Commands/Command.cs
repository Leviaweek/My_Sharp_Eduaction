namespace WeatherBotService.MessageHandler.Commands;

public abstract class Command
{
    protected IDataBase _dataBase;

    public Command(IDataBase dataBase)
    {
        _dataBase = dataBase;
    }
    public abstract Task ExecuteAsync(TelegramBotResponseResult result);
    public abstract bool Check(TelegramBotResponseResult result);
}
