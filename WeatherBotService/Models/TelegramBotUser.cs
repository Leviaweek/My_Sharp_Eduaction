using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherBotService.Models;

public class TelegramBotUser : IBotUser
{
    public int ChatId { get; }
    public string Location { get; }
    public string Language { get; }
    public TelegramBotUser(int chatId, string location, string language)
    {
        ChatId = chatId;
        Location = location;
        Language = language;
    }
}
