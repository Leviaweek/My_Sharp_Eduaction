namespace PacmanGame.GhostAlgorithm;

public class PathNode : IComparable<PathNode>
{
    public Position Position;
    public int G { get; set; }
    private int H { get; }
    private int F => G + H;

    public PathNode? Parent;

    public PathNode(Position pos, int g, int h, PathNode? parent)
    {
        Position = pos;
        G = g;
        H = h;
        Parent = parent;
    }

    public int CompareTo(PathNode? other) => F.CompareTo(other?.F);
}