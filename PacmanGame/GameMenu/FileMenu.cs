namespace PacmanGame.GameMenu;

public class FileMenu
{
    private int _linePrinted;

    public string ChoosePath()
    {
        Console.WriteLine("File is in path with game?");
        _linePrinted += 1;
        Console.CursorVisible = false;
        var userChoose = true;
        bool? userContinue = null;
        while (true)
        {
            if (userChoose)
            {
                Console.WriteLine("Yes < ");
                Console.WriteLine("No    ");
            }
            else
            {
                Console.WriteLine("Yes    ");
                Console.WriteLine("No < ");
            }

            var pressedKey = Console.ReadKey();
            switch (pressedKey.Key)
            {
                case ConsoleKey.UpArrow or ConsoleKey.DownArrow:
                    userChoose = !userChoose;
                    break;
                case ConsoleKey.Enter:
                    userContinue = userChoose;
                    break;
            }

            if (userContinue != null)
                break;
            Console.SetCursorPosition(0, Console.CursorTop - 2);
        }

        _linePrinted += 2;
        Console.CursorVisible = true;

        string? mapPath;
        if (userContinue == true)
            mapPath = Directory.GetCurrentDirectory();
        else
        {
            Console.WriteLine("Enter path to map:");
            mapPath = Console.ReadLine();
            _linePrinted += 2;
        }

        Console.WriteLine("Enter file name:");
        var fileName = Console.ReadLine();
        _linePrinted += 2;
        Console.SetCursorPosition(0, Console.CursorTop - _linePrinted);
        if (string.IsNullOrEmpty(mapPath) || string.IsNullOrEmpty(fileName))
        {
            Console.CursorVisible = true;
            Console.SetCursorPosition(0, Console.CursorTop + _linePrinted);
            throw new InvalidMapException("File not found. Exiting...");
        }

        return Path.Join(mapPath, fileName);
    }
}