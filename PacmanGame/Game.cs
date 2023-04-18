namespace PacmanGame;

public class Game
{
    private readonly CancellationTokenSource _tokenSource = new();
    private Map? CurrentMap { get; set; }
    private readonly GameUI _gameUI = new();

    public async Task Load(string path)
    {
        Console.CancelKeyPress += OnConsoleCancel;
        CurrentMap = await MapLoader.ReadMap(path);
    }

    public async Task Start()
    {
        _ = PacmanControlsAsync(_tokenSource.Token);
        await Loop(_tokenSource.Token);
    }


    private void OnConsoleCancel(object? sender, ConsoleCancelEventArgs args)
    {
        if (CurrentMap is not null)
            Console.SetCursorPosition(0, Console.CursorTop + CurrentMap.Height);
    }

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
            if (_gameUI.IsGameOver(CurrentMap.State))
                {
                    _tokenSource.Cancel();
                    return;
                }
            await Task.Delay(5, cancellationToken);
        }
    }

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