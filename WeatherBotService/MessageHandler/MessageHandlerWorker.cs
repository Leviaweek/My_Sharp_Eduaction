using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherBotService.MessageHandler;

public class MessageHandlerWorker: BackgroundService
{
    private readonly ILogger<MessageHandlerWorker> _logger;
    private readonly IMessageHandler _messageHandler;
    public MessageHandlerWorker(ILogger<MessageHandlerWorker> logger, IMessageHandler messageHandler)
    {
        _logger = logger;
        _messageHandler = messageHandler;
    }
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            await _messageHandler.CheckChatsAsync(stoppingToken);
            await Task.Delay(500, stoppingToken);
        }
        _logger.LogDebug("MessageHandlerWorker is stopping");
    }
}
