using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherBotService.MessageHandler.Commands;

public class UnregisterChatCommand : Command
{
    private readonly string _commandText = "/unregister_chat";
    private readonly IBotApi _telegramBotApi;
    public UnregisterChatCommand(IDataBase dataBase, IBotApi telegramBotApi) : base(dataBase)
    {
        _telegramBotApi = telegramBotApi;
    }
    public override bool Check(TelegramBotResponseResult result)
    {
        return result.Message.Text.StartsWith(_commandText);
    }
    public async override Task ExecuteAsync(TelegramBotResponseResult result)
    {
        IBotUser user = new TelegramBotUser(result.Message.Chat.Id, "Dnipro,709930,UA", "en");
        if (!await _dataBase.CheckUserAsync(user))
        {
            await _telegramBotApi.SendMessageAsync(result.Message.Chat.Id, "You are not registered");
            return;
        }
        await _dataBase.DeleteUserAsync(user);
        await _telegramBotApi.SendMessageAsync(result.Message.Chat.Id, "You are unregistered");
    }
}
