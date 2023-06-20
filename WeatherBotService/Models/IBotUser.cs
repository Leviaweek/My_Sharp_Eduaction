using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherBotService.Models;

public interface IBotUser
{
    public int ChatId { get; }
    public string Location { get; }
    public string Language { get; }
}
