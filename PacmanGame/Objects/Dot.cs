namespace PacmanGame.Objects;

/// <summary>
/// class <c>Dot</c> is the dot object.
/// </summary>
public class Dot: GameObject
{
    /// <summary>
    /// Property  <c>Symbol</c> is dot Symbol on the map.
    /// </summary>
    public override char Symbol { get; private protected set; } = '•';
}