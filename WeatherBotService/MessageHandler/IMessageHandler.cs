using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherBotService.MessageHandler;

public interface IMessageHandler
{
    public Task CheckChatsAsync(CancellationToken stoppingToken);
    public Task HandleResponseAsync(TelegramBotResponse telegramBotResponse);
}
