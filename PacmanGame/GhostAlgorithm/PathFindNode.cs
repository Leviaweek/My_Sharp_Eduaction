namespace PacmanGame.GhostAlgorithm;

/// <summary>
/// class <c>PathNode</c> is the path node object.
/// </summary>
public class PathNode : IComparable<PathNode>
{
    /// <summary>
    /// Field <c>Position</c> is the position of the path node.
    /// </summary>
    public Position Position;

    /// <summary>
    /// Field <c>G</c> is the path node cost.
    /// </summary>
    public int G { get; set; }

    /// <summary>
    /// Field <c>H</c> is the path node heuristic.
    /// </summary>
    private int H { get; }

    /// <summary>
    /// Property <c>F</c> is the path node total cost.
    /// </summary>
    private int F => G + H;

    /// <summary>
    /// Field <c>Parent</c> is the path node parent.
    /// </summary>
    public PathNode? Parent;

    /// <summary>
    /// Constructor <c>PathNode</c> is the constructor for the path node object.
    /// </summary>
    /// <param name="pos">The position of the path node.</param>
    /// <param name="g">The path node cost.</param>
    /// <param name="h">The path node heuristic.</param>
    /// <param name="parent">The path node parent.</param>
    public PathNode(Position pos, int g, int h, PathNode? parent)
    {
        Position = pos;
        G = g;
        H = h;
        Parent = parent;
    }

    public int CompareTo(PathNode? other) => F.CompareTo(other?.F);
}