namespace WeatherBotService.MessageHandler.Commands;

public class RegisterChatCommand : Command
{
    private readonly string _commandText = "/register_chat";
    private readonly IBotApi _telegramBotApi;
    public RegisterChatCommand(IDataBase<TelegramBotUser> dataBase, IBotApi telegramBotApi) : base(dataBase)
    {
        _telegramBotApi = telegramBotApi;
    }
    public async override Task ExecuteAsync(TelegramBotResponseResult result)
    {
        var user = new TelegramBotUser(result.Message.Chat.Id, "Dnipro,709930,UA", "en");
        if (await _dataBase.CheckUserAsync(user))
        {
            await _telegramBotApi.SendMessageAsync(result.Message.Chat.Id, "You are alredy registered");
            return;
        }
        await _dataBase.InsertUserAsync(user);
        await _telegramBotApi.SendMessageAsync(result.Message.Chat.Id, "You are registered");
    }
    public override bool Check(TelegramBotResponseResult result)
    {
        return result.Message.Text.StartsWith(_commandText);
    }
}
