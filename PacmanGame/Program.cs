namespace PacmanGame;
using System.Text;
internal static class Program
{
    public static async Task Main()
    {
        Console.OutputEncoding = Encoding.UTF8;
		Console.Clear();
        
        var fileMenu = new FileMenu();
        var path = fileMenu.ChoosePath();
        
        var pacman = new Game();
        try
        {
            await pacman.Load(path);
            await pacman.Start();
        }
        catch (InvalidMapException e)
        {
            Console.Clear();
            Console.WriteLine(e.Message);
        }
        
        Console.WriteLine("Press Enter key to exit...".PadRight(Console.WindowWidth - 1));
        Console.CursorVisible = true;
        while (true)
        {
            if (!Console.KeyAvailable) continue;
            var key = Console.ReadKey(true).Key;
            if (key == ConsoleKey.Enter)
                return;
        }
    }
}