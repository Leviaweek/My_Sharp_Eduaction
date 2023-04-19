namespace PacmanGame.Objects;

/// <summary>
/// class <c>BigDot</c> is the big dot object.
/// </summary>
public class BigDot: GameObject
{
    /// <summary>
    /// Property  <c>Symbol</c> is big dot Symbol on the map.
    /// </summary>
    public override char Symbol { get; private protected set; } = '◎';
}