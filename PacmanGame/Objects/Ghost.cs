namespace PacmanGame.Objects;

public class Ghost : Player
{


    private protected override TimeSpan MoveInterval => TimeSpan.FromMilliseconds(280);
    public override char Symbol { get; private protected set; } = '*';

    private protected override void Death()
    {
        if (State == PlayerState.FreezeTime)
            return;
        Location = SpawnPosition;
        State = PlayerState.FreezeTime;
    }

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