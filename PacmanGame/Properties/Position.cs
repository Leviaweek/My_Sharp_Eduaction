namespace PacmanGame.Properties;

/// <summary>
/// readonly struct <c>Position</c> is the position of the game.
/// </summary>
public readonly struct Position
{

    /// <summary>
    /// Property <c>X</c> is the X coordinate of the position.
    /// </summary>
    public int X { get; }

    /// <summary>
    /// Property <c>Y</c> is the Y coordinate of the position.
    /// </summary>
    public int Y { get; }

    /// <summary>
    /// Property <c>Z</c> is the Z coordinate of the position.
    /// </summary>
    public int Z { get; }

    /// <summary>
    /// Constructor <c>Position</c> creates a new position.
    /// </summary>
    /// <param name="x">X coordinate of the position.</param>
    /// <param name="y">Y coordinate of the position.</param>
    /// <param name="z">Z coordinate of the position.</param>
    public Position(int x, int y, int z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    /// <summary>
    /// Property <c>Zero</c> is the zero position.
    /// </summary>
    public static readonly Position Zero = new (0, 0, 0);

    /// <summary>
    /// Property <c>Up</c> is the up position of the position relative to the player position.
    /// </summary>
    public static readonly Position Up = new (0, -1, 0);

    /// <summary>
    /// Property <c>Right</c> is the right position relative to the player position.
    /// </summary>
    public static readonly Position Right = new (1, 0, 0);
    public static Position operator + (Position a, Position b) => new(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
    public static Position operator - (Position a) => new(-a.X, -a.Y, -a.Z);
    public static Position operator - (Position a, Position b) => new(a.X - b.X, a.Y - b.Y, a.Z - b.Z);

    /// <summary>
    /// Method <c>Equals</c> checks if two positions are equal.
    /// </summary>
    /// <param name="a">First position.</param>
    /// <param name="b">Second position.</param>
    public static bool ZLessEq(Position a, Position b) => (a.X == b.X && a.Y == b.Y);

    public override string ToString() => $"({X}, {Y}, {Z})";
}