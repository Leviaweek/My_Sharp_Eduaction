namespace PacmanGame.GameMenu;

/// <summary>
/// class <c>MapLoader</c> is the map loader object.
/// </summary>
public static class MapLoader
{
    /// <summary>
    /// Method <c>ReadMap</c> is the method for reading the map.
    /// </summary>
    public static async Task<Map> ReadMap(string path)
    {
        if (!File.Exists(path))
            throw new InvalidMapException("Map file does not exist. Exiting...");

        using var file = new StreamReader(path);
        var width = 0;
        var height = 0;
        var gameObjects = new List<GameObject>();
        for (var y = 0;; y++)
        {
            var text = await file.ReadLineAsync();
            if (string.IsNullOrEmpty(text))
                break;

            if (width == 0)
                width = text.Length;
            else if (width != text.Length)
                throw new InvalidMapException("Map is not valid. Exiting...");

            height++;

            for (var x = 0; x < text.Length; x++)
            {
                if (text[x] == '0')
                    continue;
                GameObject obj = text[x] switch
                {
                    '1' => new Wall(),
                    '2' => new Pacman(),
                    '3' => new Ghost(),
                    '4' => new Dot(),
                    '5' => new BigDot(),
                    _ => throw new InvalidMapException("Map has invalid symbols. Exiting...")
                };
                var zPos = obj switch
                {
                    Wall => 1,
                    Pacman => 2,
                    Ghost => 3,
                    Dot => 1,
                    BigDot => 1,
                    _ => throw new IndexOutOfRangeException()
                };

                obj.ApplyPosition(new Position(x, y, zPos));
                var pacmanCount = gameObjects.Count(gameObject => gameObject is Pacman);
                if (pacmanCount > 1)
                    throw new InvalidMapException("Two pacmans... You read rules???");
                gameObjects.Add(obj);
            }
        }

        return new Map(gameObjects, width, height);
    }
}