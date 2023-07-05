namespace WeatherBotService.MessageHandler.Commands;

public class RegisterChatCommand : Command
{
    private readonly string _commandText = "/register_chat";
    private readonly IBotApi _telegramBotApi;
    public RegisterChatCommand(IBotApi telegramBotApi)
    {
        _telegramBotApi = telegramBotApi;
    }
    public async override Task ExecuteAsync(TelegramBotResponseResult result, IDataBase<TelegramBotUser> dataBase)
    {
        var user = new TelegramBotUser(result.Message.Chat.Id, "Dnipro,709930,UA", "en");
        if (await dataBase.CheckUserAsync(user))
        {
            await _telegramBotApi.SendMessageAsync(result.Message.Chat.Id, "You are alredy registered");
            return;
        }
        await dataBase.InsertUserAsync(user);
        await _telegramBotApi.SendMessageAsync(result.Message.Chat.Id, "You are registered");
    }
    public override bool Check(TelegramBotResponseResult result)
    {
        return result.Message.Text.StartsWith(_commandText);
    }
}
