namespace PacmanGame.Properties;

/// <summary>
/// readonly struct <c>ScoreBoard</c> is the score board of the game.
/// </summary>
/// <param name="Value">Value of the score board.</param>
public readonly record struct Lives(int Value)
{
    public static Lives operator -- (Lives a) => new(a.Value - 1);
    public static Lives operator ++ (Lives a) => new(a.Value + 1);
    public override string ToString() => Value.ToString();
}