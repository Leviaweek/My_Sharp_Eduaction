namespace PacmanGame;

/// <summary>
/// class <c>Game</c> is the main class of the game.
/// </summary>
public class Game
{
    /// <summary>
    /// Field <c>_tokenSource</c>Token source for canceling the game.
    /// </summary>
    private readonly CancellationTokenSource _tokenSource = new();
    /// <summary>
    /// Property <c>CurrentMap</c> is the current map of the game.
    /// </summary>
    private Map? CurrentMap { get; set; }
    /// <summary>
    /// Field <c>_gameUI</c> is the game UI.
    /// </summary>
    private readonly GameUI _gameUI = new();

    /// <summary>
    ///     Load map from file.
    /// </summary>
    /// <param name="path">Path to the map file.</param>
    public async Task Load(string path)
    {
        Console.CancelKeyPress += OnConsoleCancel;
        CurrentMap = await MapLoader.ReadMap(path);
    }

    /// <summary>
    ///     Start the game.
    /// </summary>
    public async Task Start()
    {
        _ = PacmanControlsAsync(_tokenSource.Token);
        await Loop(_tokenSource.Token);
    }


    /// <summary>
    ///     Cancel the game.
    /// </summary>
    /// <param name="sender">Sender of the event.</param>
    /// <param name="args">Arguments of the event.</param>
    private void OnConsoleCancel(object? sender, ConsoleCancelEventArgs args)
    {
        if (CurrentMap is not null)
            Console.SetCursorPosition(0, Console.CursorTop + CurrentMap.Height);
    }

    /// <summary>
    ///     Loop of the game.
    /// </summary>
    /// <param name="cancellationToken">Token for canceling the game.</param>
    private async Task Loop(CancellationToken cancellationToken = default)
    {
        if (CurrentMap?.PacmanPlayer is null)
            return;

        Console.CursorVisible = false;

        while (true)
        {
            CurrentMap.MoveAll();
            _gameUI.PrintScoreBoard(CurrentMap.PacmanPlayer.MyLives, CurrentMap.PacmanPlayer.Score);
            _gameUI.PrintMap(CurrentMap);
            if (GameUI.IsGameOver(CurrentMap.State))
                {
                    _tokenSource.Cancel();
                    return;
                }
            await Task.Delay(5, cancellationToken);
        }
    }

    /// <summary>
    ///     Controls of the game.
    /// </summary>
    /// <param name="cancellationToken">Token for canceling the game.</param>
    private async Task PacmanControlsAsync(CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            if (Console.KeyAvailable && CurrentMap?.PacmanPlayer is not null)
            {
                var pressedKey = Console.ReadKey(true);
                CurrentMap.PacmanPlayer?.ChangeDirection(pressedKey.Key);
            }

            await Task.Delay(5, cancellationToken);
        }
    }
}