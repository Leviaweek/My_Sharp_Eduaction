namespace PacmanGame.Objects;

/// <summary>
/// class <c>Map</c> is the game map object.
/// </summary>
public class Map
{
    /// <summary>
    /// Property <c>MapObjects</c> is the list of all objects on the map.
    /// </summary>
    public readonly List<GameObject> MapObjects;

    /// <summary>
    /// Property <c>Width</c> is the width of the map.
    /// </summary>
    public int Width { get; }

    /// <summary>
    /// Property <c>Height</c> is the height of the map.
    /// </summary>
    public int Height { get; }

    /// <summary>
    /// Property <c>MoveDelegate</c> is the delegate for the move event.
    /// </summary>
    public delegate void MoveDelegate();

    /// <summary>
    /// Property <c>MoveEvent</c> is the move event.
    /// </summary>
    public event MoveDelegate? MoveEvent;

    /// <summary>
    /// Property <c>State</c> is the state of the game.
    /// </summary>
    public GameState State { get; private set; } = GameState.Game;

    /// <summary>
    /// Property <c>PacmanPlayer</c> is the Pacman player.
    /// </summary>
    public Pacman PacmanPlayer { get; }

    /// <summary>
    /// Constructor <c>Map</c> is the constructor for the map object.
    /// </summary>
    /// <param name="mapObjects">The list of all objects on the map.</param>
    /// <param name="width">The width of the map.</param>
    /// <param name="height">The height of the map.</param>
    public Map(IEnumerable<GameObject> mapObjects, int width, int height)
    {
        MapObjects = mapObjects.OrderBy(x => x.Location.Z).ToList();
        foreach (var obj in MapObjects)
            obj.ApplyMap(this);

        Width = width;
        Height = height;

        PacmanPlayer = MapObjects.OfType<Pacman>().First();
    }

    /// <summary>
    /// Method <c>MoveAll</c> calls the move event.
    /// </summary>
    public void MoveAll()
    {
        MoveEvent?.Invoke();
        CheckState();
    }

    /// <summary>
    /// Method <c>CheckState</c> checks the state of the game.
    /// </summary>
    private void CheckState()
    {
        if (!MapObjects.Any(x => x is Dot))
            State = GameState.PacmanWin;
        else if (PacmanPlayer!.State == PlayerState.Dead)
            State = GameState.PacmanLose;
    }

    /// <summary>
    /// Method <c>Destroy</c> removes an object from the map.
    /// </summary>
    /// <param name="obj">The object to remove.</param>
    public void Destroy(GameObject obj) => MapObjects.Remove(obj);

}