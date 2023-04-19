using System.Text.Json;
using System.IO;
namespace BankingSystem;

public class Logger
{
    private readonly string _fileName;
    public Logger()
    {
        _fileName = "banking.log";
    }
    public void Write(LogMessage logMessage)
    {
        if (!File.Exists(_fileName))
        {
            File.Create(_fileName).Close();
        }
        var json = JsonSerializer.Serialize(logMessage);
        using var writer = new StreamWriter(_fileName, append: true);
        writer.WriteLine(json);
    }
}
