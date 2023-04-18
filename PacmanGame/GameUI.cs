using PacmanGame.Objects;

namespace PacmanGame;

public class GameUI
{
    private int _linePrinted;
    public void PrintMap(Map map)
    {
        var mapText = new char[map.Height, map.Width];
        for (var i = 0; i < mapText.GetLength(0); i++)
        for (var j = 0; j < mapText.GetLength(1); j++)
            mapText[i, j] = ' ';

        foreach (var mapObj in map.MapObjects)
            mapText[mapObj.Location.Y, mapObj.Location.X] = mapObj.Symbol;

        for (var i = 0; i < mapText.GetLength(0); i++)
        {
            for (var j = 0; j < mapText.GetLength(1); j++)
                Console.Write(mapText[i, j]);
            Console.WriteLine();
        }
        _linePrinted += map.Height;
    }
    public void PrintScoreBoard(Lives lives, ScoreBoard score)
    {
		Console.SetCursorPosition(0, Console.CursorTop - _linePrinted);
        _linePrinted = 0;
        Console.WriteLine($"Lives: {lives.ToString()} Score: {score.ToString()}".PadRight(Console.WindowWidth - 1));
        _linePrinted++;
    }
    public bool IsGameOver(GameState state)
    {
        switch (state)
            {
                case GameState.PacmanWin:
                    Console.WriteLine("You win!".PadRight(Console.WindowWidth - 1));
                    return true;
                case GameState.PacmanLose:
                    Console.WriteLine("You lose!".PadRight(Console.WindowWidth - 1));
                    return true;
                default:
                    return false;
            }
    }
}