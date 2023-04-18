using System.Windows.Input;

namespace PacmanGame.Objects;

public class Pacman : Player
{
    public Pacman() 
    {
        Score = new ScoreBoard();
    }

    public override char Symbol { get; private protected set; } = '@';
    public Lives MyLives = new(3);
    public ScoreBoard Score = new();

    private readonly TimeSpan _superTime = new(0, 0, 0, 5, 0);
    private DateTime _endSuperTime = new();

    private protected override TimeSpan MoveInterval => TimeSpan.FromMilliseconds(250);
    private ConsoleKey _direction;

    public void ChangeDirection(ConsoleKey key) => _direction = key;


    private protected override void Death()
    {
        if (State == PlayerState.FreezeTime)
            return;
        if (MyLives.Value == 0)
        {
            State = PlayerState.Dead;
            Symbol = 'X';
            return;
        }

        MyLives--;
        Location = SpawnPosition;
    }

    private void NextStep(Position loc)
    {
        switch (State)
        {
            case PlayerState.Normal or PlayerState.Super:
                var position = Location + loc;
                var obj = MapObject.MapObjects.Find(x => Position.ZLessEq(position, x.Location));
                switch (obj)
                {
                    case Dot dot:
                        MapObject.Destroy(dot);
                        Score += 100;
                        break;
                    case Wall wall:
                        return;
                    case Ghost ghost:
                        if (State == PlayerState.Super)
                        {
                            Kill(ghost);
                            break;
                        }

                        Death();
                        return;
                    case BigDot bigDot:
                        MapObject.Destroy(bigDot);
                        Score += 200;
                        State = PlayerState.Super;
                        _endSuperTime = DateTime.Now.Add(_superTime);
                        break;
                    default:
                        break;
                }

                Location = position;
                _lastMove = DateTime.Now;
                break;
        }
    }

    private void Up() => NextStep(Position.Up);
    private void Down() => NextStep(-Position.Up);
    private void Left() => NextStep(-Position.Right);
    private void Right() => NextStep(Position.Right);

    private protected override void CheckState()
    {
        switch (State)
        {
            case PlayerState.FreezeTime:
                if (_lastMove == null)
                    _lastMove = DateTime.Now;
                if (DateTime.Now.Subtract((DateTime)_lastMove!) < _freezeTime)
                {
                    if (Symbol == '@')
                        Symbol = ' ';
                    else
                        Symbol = '@';
                    return;
                }

                State = PlayerState.Normal;
                Symbol = '@';
                break;
            case PlayerState.Dead:
                Symbol = 'X';
                break;
            case PlayerState.Super:
                if (DateTime.Now > _endSuperTime)
                {
                    State = PlayerState.Normal;
                    Symbol = '@';
                    break;
                }

                if (Symbol == '@')
                    Symbol = 'O';
                else
                    Symbol = '@';
                break;
        }
    }

    private protected override void Update()
    {
        CheckState();
        if (State != PlayerState.FreezeTime && DateTime.Now.Subtract((DateTime)_lastMove!) < MoveInterval)
            return;
        switch (_direction)
        {
            case ConsoleKey.UpArrow or ConsoleKey.W:
                Up();
                break;
            case ConsoleKey.DownArrow or ConsoleKey.S:
                Down();
                break;
            case ConsoleKey.LeftArrow or ConsoleKey.A:
                Left();
                break;
            case ConsoleKey.RightArrow or ConsoleKey.D:
                Right();
                break;
            default:
                break;
        }
    }
}