namespace PacmanGame.Objects;

public abstract class Player : GameObject
{
    public Position SpawnPosition { get; private set; } = Position.Zero;
    public PlayerState State { get; private protected set; } = PlayerState.FreezeTime;
    private protected abstract void Update();
    private protected readonly TimeSpan _freezeTime = new(0, 0, 0, 2, 0);
    private protected DateTime? _lastMove = null;
    private protected abstract TimeSpan MoveInterval { get; }

    private protected static void Kill(Player player) => player.Death();

    public override void ApplyPosition(Position position)
    {
        base.ApplyPosition(position);
        SpawnPosition = position;
    }

    public override void ApplyMap(Map mapObject)
    {
        base.ApplyMap(mapObject);
        mapObject.MoveEvent += Update;
    }

    private protected abstract void Death();
    private protected abstract void CheckState();
}