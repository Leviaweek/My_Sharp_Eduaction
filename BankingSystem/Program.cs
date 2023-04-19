using System.Text.Json;
namespace BankingSystem;

public static class Program
{
    public static void Main()
    {
        var ui = new BankingUi();
        try
        {
            ui.Menu();
        }
        catch (Exception e)
        {
            var logMessage = new LogMessage("System", "Main", $"Error: {e.GetType()}", e.Message);
            Console.WriteLine(logMessage);
            ui.Logger.Write(logMessage);
        }
    }
}