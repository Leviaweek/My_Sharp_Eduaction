namespace PacmanGame.Objects;

/// <summary>
/// class <c>Wall</c> is the wall object.
/// </summary>
public class Wall : GameObject
{
    /// <summary>
    /// Property <c>Symbol</c> is the wall symbol on the map.
    /// </summary>
    public override char Symbol { get; private protected set; } = '#';
}