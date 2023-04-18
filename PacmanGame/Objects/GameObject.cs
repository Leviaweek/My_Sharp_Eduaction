namespace PacmanGame.Objects;

/// <summary>
/// class <c>GameObject</c> is the base class for all objects in the game.
/// </summary>
public abstract class GameObject
{
    ///<summary>
    /// Property <c>Location</c> is the location of the object.
    ///</summary>
    public Position Location { get; set; } = Position.Zero;

    ///<summary>
    /// Property <c>MapObject</c> is the map of the object.
    ///</summary>
    private Map? _map;

    ///<summary>
    /// Property <c>MapObject</c> is the map of the object.
    ///</summary>
    protected Map MapObject => _map ?? throw new NullReferenceException("Map is not set");

    ///<summary>
    /// Method <c>ApplyMap</c> applies the map to the object.
    ///</summary>
    public virtual void ApplyMap(Map mapObject) => _map = mapObject;

    ///<summary>
    /// Method <c>ApplyPosition</c> applies the position to the object.
    ///</summary>
    public virtual void ApplyPosition(Position position) => Location = position;

    ///<summary>
    /// Property  <c>Symbol</c> is GameObjects Symbol on the map.
    ///</summary>
    public abstract char Symbol { get; private protected set; }

    public override string ToString()
    {
        return $"{Symbol} : {Location}";
    }
}