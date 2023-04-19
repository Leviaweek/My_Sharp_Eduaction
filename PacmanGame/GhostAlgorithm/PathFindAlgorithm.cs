namespace PacmanGame.GhostAlgorithm;

/// <summary>
/// class <c>PathFinding</c> is the path finding algorithm.
/// </summary>
public class Pathfinding
{
    /// <summary>
    /// Field <c>_map</c> is the map of the path finding algorithm.
    /// </summary>
    private int[,]? _map;

    /// <summary>
    /// Method <c>CreateMap</c> is the method to create the map matrix of the path finding algorithm.
    /// </summary>
    /// <param name="mapObjects">The map objects.</param>
    /// <param name="width">The width of the map.</param>
    /// <param name="height">The height of the map.</param>
    public void CreateMap(IEnumerable<GameObject> mapObjects, int width, int height)
    {
        _map = new int[height, width];

        foreach (var obj in mapObjects.OfType<Wall>().Select(x => x.Location))
        {
            _map[obj.Y, obj.X] = 1;
        }
    }

    /// <summary>
    /// Method <c>FindPath</c> is the method to find the path from the start position to the end position.
    /// </summary>
    /// <param name="start">The start position.</param>
    /// <param name="end">The end position.</param>
    public List<Position> FindPath(Position start, Position end)
    {
        List<Position> path = new();
        List<PathNode> openList = new();
        List<PathNode> closedList = new();

        PathNode startNode = new(start, 0, Heuristic(start, end), null);
        openList.Add(startNode);

        while (openList.Count > 0)
        {
            var currentNode = openList[0];
            openList.RemoveAt(0);

            if (Position.ZLessEq(currentNode.Position, end))
            {
                while (currentNode != null)
                {
                    path.Insert(0, currentNode.Position);
                    currentNode = currentNode.Parent;
                }

                break;
            }

            closedList.Add(currentNode);

            foreach (var neighbor in GetNeighbors(currentNode, end))
            {
                if (closedList.Contains(neighbor))
                    continue;

                var openNode = openList.FirstOrDefault(n => Position.ZLessEq(n.Position, neighbor.Position));
                if (openNode != null)
                {
                    if (neighbor.G >= openNode.G) continue;
                    openNode.G = neighbor.G;
                    openNode.Parent = neighbor.Parent;
                }
                else
                {
                    openList.Add(neighbor);
                }
            }

            openList.Sort();
        }

        return path;
    }

    /// <summary>
    /// Method <c>GetNeighbors</c> is the method to get the neighbors of the path node.
    /// </summary>
    /// <param name="node">The path node.</param>
    /// <param name="end">The end position.</param>
    private IEnumerable<PathNode> GetNeighbors(PathNode node, Position end)
    {
        var directions = new Position[]
        {
            new(0, 1, 0),
            new(0, -1, 0),
            new(-1, 0, 0),
            new(1, 0, 0)
        };

        foreach (var dir in directions)
        {
            var neighborPos = node.Position + dir;

            if (neighborPos.X < 0 || neighborPos.X >= _map!.GetLength(1) ||
                neighborPos.Y < 0 || neighborPos.Y >= _map!.GetLength(0) ||
                _map[neighborPos.Y, neighborPos.X] == 1)
                continue;


            var g = node.G + 1;
            var h = Heuristic(neighborPos, end);
            yield return new PathNode(neighborPos, g, h, node);
        }
    }

    /// <summary>
    /// Method <c>Heuristic</c> is the method to calculate the heuristic of the path node.
    /// </summary>
    /// <param name="a">The start position.</param>
    /// <param name="b">The end position.</param>
    private static int Heuristic(Position a, Position b) => Math.Abs(a.X - b.X) + Math.Abs(a.Y - b.Y);
}