namespace PacmanGame.Objects;

public abstract class GameObject
{
    public Position Location { get; set; } = Position.Zero;
    private Map? _map;
    protected Map MapObject => _map ?? throw new NullReferenceException("Map is not set");

    public virtual void ApplyMap(Map mapObject) => _map = mapObject;
    public virtual void ApplyPosition(Position position) => Location = position;
    public abstract char Symbol { get; private protected set; }

    public override string ToString()
    {
        return $"{Symbol} : {Location}";
    }
}