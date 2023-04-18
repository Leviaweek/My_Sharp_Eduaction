namespace PacmanGame.Objects;

public class Map
{
    public readonly List<GameObject> MapObjects;
    public int Width { get; }
    public int Height { get; }

    public delegate void MoveDelegate();

    public event MoveDelegate? MoveEvent;
    public GameState State { get; private set; } = GameState.Game;
    public Pacman PacmanPlayer { get; }


    public Map(IEnumerable<GameObject> mapObjects, int width, int height)
    {
        MapObjects = mapObjects.OrderBy(x => x.Location.Z).ToList();
        foreach (var obj in MapObjects)
            obj.ApplyMap(this);

        Width = width;
        Height = height;

        PacmanPlayer = MapObjects.OfType<Pacman>().First();
    }

    public void MoveAll()
    {
        MoveEvent?.Invoke();
        CheckState();
    }

    private void CheckState()
    {
        if (!MapObjects.Any(x => x is Dot))
            State = GameState.PacmanWin;
        else if (PacmanPlayer!.State == PlayerState.Dead)
            State = GameState.PacmanLose;
    }

    public void Destroy(GameObject obj) => MapObjects.Remove(obj);

}