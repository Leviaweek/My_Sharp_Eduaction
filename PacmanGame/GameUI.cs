using PacmanGame.Objects;

namespace PacmanGame;

/// <summary>
/// class <c>GameUI</c> is the UI of the game.
/// </summary>
public class GameUI
{
    /// <summary>
    /// Field <c>_linePrinted</c> is the number of lines printed.
    /// </summary>
    private int _linePrinted;

    /// <summary>
    ///     Print the map.
    /// </summary>
    /// <param name="map">Map to print.</param>
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

    /// <summary>
    ///     Print the score board.
    /// </summary>
    /// <param name="lives">Lives of the player.</param>
    /// <param name="score">Score of the player.</param>
    public void PrintScoreBoard(Lives lives, ScoreBoard score)
    {
		Console.SetCursorPosition(0, Console.CursorTop - _linePrinted);
        _linePrinted = 0;
        Console.WriteLine($"Lives: {lives} Score: {score}".PadRight(Console.WindowWidth - 1));
        _linePrinted++;
    }
    
    /// <summary>
    ///     Check if the game is over.
    /// </summary>
    /// <param name="state">State of the game.</param>
    public static bool IsGameOver(GameState state)
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