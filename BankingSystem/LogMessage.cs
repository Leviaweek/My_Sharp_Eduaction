using System.Text.Json.Serialization;

namespace BankingSystem;

public class LogMessage
{
    public LogMessage(string author, string logLevel, string action, string message)
    {
        Author = author;
        LogLevel = logLevel;
        Action = action;
        Message = message;
        Date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
    }
    [JsonPropertyName("date")]
    public string Date { get; }
    [JsonPropertyName("author")]
    public string Author { get; }
    [JsonPropertyName("logLevel")]
    public string LogLevel { get; }
    [JsonPropertyName("action")]
    public string Action { get; }
    [JsonPropertyName("message")]
    public string Message { get; }
    public override string ToString() => $"Date: [{Date}] \nAuthor: {Author}\nLogLevel: {LogLevel}\nAction: {Action}\nMessage: {Message}";
}
