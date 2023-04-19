namespace PacmanGame.Objects;

/// <summary>
/// class <c>Player</c> is the player object.
/// </summary>
public abstract class Player : GameObject
{
    /// <summary>
    /// Property <c>SpawnPosition</c> is the spawn position of the player.
    /// </summary>
    public Position SpawnPosition { get; private set; } = Position.Zero;

    /// <summary>
    /// Property <c>State</c> is the state of the player.
    /// </summary>
    public PlayerState State { get; private protected set; } = PlayerState.FreezeTime;

    /// <summary>
    /// Property <c>Update</c> is the method that is called when the player updates.
    /// </summary>
    private protected abstract void Update();

    /// <summary>
    /// field <c>_freezeTime</c> is the time the player is frozen.
    /// </summary>
    private protected readonly TimeSpan _freezeTime = new(0, 0, 0, 2, 0);

    /// <summary>
    /// field <c>_lastMove</c> is the last time the player moved.
    /// </summary>
    private protected DateTime? _lastMove = null;

    /// <summary>
    /// Property <c>MoveInterval</c> is the interval between moves.
    /// </summary>
    private protected abstract TimeSpan MoveInterval { get; }

    /// <summary>
    /// Method <c>Kill</c> is the method that is called when the player dies.
    /// </summary>
    private protected static void Kill(Player player) => player.Death();

    /// <summary>
    /// Method <c>ApplyPosition</c> is the method that is called when the player is added to the map.
    /// </summary>
    public override void ApplyPosition(Position position)
    {
        base.ApplyPosition(position);
        SpawnPosition = position;
    }

    /// <summary>
    /// Method <c>ApplyMap</c> is the method that is called when the player is added to the map.
    /// </summary>
    public override void ApplyMap(Map mapObject)
    {
        base.ApplyMap(mapObject);
        mapObject.MoveEvent += Update;
    }

    /// <summary>
    /// Method <c>Death</c> is the method that is called when the player dies.
    /// </summary>
    private protected abstract void Death();

    /// <summary>
    /// Method <c>CheckState</c> is the method that is called when the player checks its state.
    /// </summary>
    private protected abstract void CheckState();
}