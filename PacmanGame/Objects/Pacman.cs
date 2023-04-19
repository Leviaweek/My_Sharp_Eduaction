using System.Windows.Input;

namespace PacmanGame.Objects;

/// <summary>
/// class <c>Pacman</c> is the Pacman object.
/// </summary>
public class Pacman : Player
{
    /// <summary>
    /// Constructor <c>Pacman</c> is the constructor for the Pacman object.
    /// </summary>
    public Pacman() 
    {
        Score = new ScoreBoard();
    }

    /// <summary>
    /// Property <c>Symbol</c> is the Pacman symbol on the map.
    /// </summary>
    public override char Symbol { get; private protected set; } = '@';

    /// <summary>
    /// Field <c>MyLives</c> is the number of lives the Pacman has.
    /// </summary>
    public Lives MyLives = new(3);

    /// <summary>
    /// Field <c>Score</c> is the score of the Pacman.
    /// </summary>
    public ScoreBoard Score = new();

    /// <summary>
    /// readonly field <c>_superTime</c> is the time the Pacman is super.
    /// </summary>
    private readonly TimeSpan _superTime = new(0, 0, 0, 5, 0);

    /// <summary>
    /// field <c>_endSuperTime</c> is the end time the Pacman is super.
    /// </summary>
    private DateTime _endSuperTime = new();

    /// <summary>
    /// Property <c>MoveInterval</c> is the time between moves.
    /// </summary>
    private protected override TimeSpan MoveInterval => TimeSpan.FromMilliseconds(250);

    /// <summary>
    /// Field <c>_direction</c> is the direction the Pacman is moving.
    /// </summary>
    private ConsoleKey _direction;

    /// <summary>
    /// Method <c>ChangeDirection</c> is the method that is called when the Pacman changes direction.
    /// </summary>
    /// <param name="key">The key that was pressed.</param>
    public void ChangeDirection(ConsoleKey key) => _direction = key;

    /// <summary>
    /// Method <c>Death</c> is the method that is called when the Pacman dies.
    /// </summary>
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

    /// <summary>
    /// Method <c>NextStep</c> is the method that is called when the Pacman moves.
    /// </summary>
    /// <param name="loc">The location the Pacman is moving to.</param>
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

    /// <summary>
    /// Method <c>Up</c> is the method that is called when the Pacman moves up.
    /// </summary>
    private void Up() => NextStep(Position.Up);

    /// <summary>
    /// Method <c>Down</c> is the method that is called when the Pacman moves down.
    /// </summary>
    private void Down() => NextStep(-Position.Up);

    /// <summary>
    /// Method <c>Left</c> is the method that is called when the Pacman moves left.
    /// </summary>
    private void Left() => NextStep(-Position.Right);

    /// <summary>
    /// Method <c>Right</c> is the method that is called when the Pacman moves right.
    /// </summary>
    private void Right() => NextStep(Position.Right);

    /// <summary>
    /// Method <c>CheckState</c> is the method that is called when the Pacman checks its state.
    /// </summary>
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

    /// <summary>
    /// Method <c>Update</c> is the method that is called when the Pacman updates.
    /// </summary>
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