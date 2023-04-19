namespace PacmanGame.Objects;

/// <summary>
/// class <c>Ghost</c> is the ghost object.
/// <summary>
public class Ghost : Player
{

    /// <summary>
    /// Field <c>MoveInterval</c> is the time between moves.
    private protected override TimeSpan MoveInterval => TimeSpan.FromMilliseconds(280);

    /// <summary>
    /// Property  <c>Symbol</c> is ghost Symbol on the map.
    public override char Symbol { get; private protected set; } = '*';

    /// <summary>
    /// Method <c>Death</c> is the method that is called when the ghost dies.
    /// </summary>
    private protected override void Death()
    {
        if (State == PlayerState.FreezeTime)
            return;
        Location = SpawnPosition;
        State = PlayerState.FreezeTime;
    }

    /// <summary>
    /// Method <c>NextStep</c> is the method that is called when the ghost moves.
    /// </summary>
    /// <param name="loc">The location to move to.</param>
    private void NextStep(Position loc)
    {
        switch (State)
        {
            case PlayerState.Normal or PlayerState.Super:
            {
                var position = Location + loc;
                var obj = MapObject.MapObjects.Find(x => Position.ZLessEq(position, x.Location));
                switch (obj)
                {
                    case Wall:
                        return;
                    case Pacman pacman:
                        if (pacman.State == PlayerState.Super)
                        {
                            Death();
                            return;
                        }

                        Kill(pacman);
                        return;
                }

                Location = position;
                _lastMove = DateTime.Now;
                break;
            }
        }
    }

    /// <summary>
    /// Method <c>CheckState</c> is the method that is called when the ghost checks its state.
    /// </summary>
    private protected override void CheckState()
    {
        switch (State)
        {
            case PlayerState.FreezeTime:
                _lastMove ??= DateTime.Now;
                
                if (DateTime.Now.Subtract((DateTime)_lastMove!) < _freezeTime)
                {
                    Symbol = Symbol == '*' ? ' ' : '*';
                    return;
                }

                State = PlayerState.Normal;
                Symbol = '*';
                break;
            case PlayerState.Normal:
                break;
            case PlayerState.Super:
                break;
            case PlayerState.Dead:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    /// <summary>
    /// Method <c>Update</c> is the method that is called when the ghost updates.
    /// </summary>
    private protected override void Update()
    {
        CheckState();
        if (State != PlayerState.FreezeTime && DateTime.Now.Subtract((DateTime)_lastMove!) < MoveInterval)
            return;
        Pathfinding pathfinding = new();
        pathfinding.CreateMap(MapObject.MapObjects, MapObject.Width, MapObject.Height);
        var path = pathfinding.FindPath(Location, MapObject.PacmanPlayer!.Location);
        if (path.Count == 0)
            return;
        var nextStep = path.Count == 1 ? path[0] : path[1];
        NextStep(nextStep - Location);
    }
}