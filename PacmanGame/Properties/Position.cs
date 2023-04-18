namespace PacmanGame.Properties;

public readonly struct Position
{
    public int X { get; }
    public int Y { get; }
    public int Z { get; }
    public Position(int x, int y, int z)
    {
        X = x;
        Y = y;
        Z = z;
    }
    public static readonly Position Zero = new (0, 0, 0);
    public static readonly Position Up = new (0, -1, 0);
    public static readonly Position Right = new (1, 0, 0);
    public static Position operator + (Position a, Position b) => new(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
    public static Position operator - (Position a) => new(-a.X, -a.Y, -a.Z);
    public static Position operator - (Position a, Position b) => new(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
    public static bool ZLessEq(Position a, Position b) => (a.X == b.X && a.Y == b.Y);

    public override string ToString() => $"({X}, {Y}, {Z})";
}