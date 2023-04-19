namespace PacmanGame.Objects;

/// <summary>
/// struct <c>ScoreBoard</c> is the score board object.
/// </summary>
public struct ScoreBoard
{
    /// <summary>
    /// Property <c>Value</c> is the value of the score board.
    /// </summary>
    public int Value { get; set; }

    /// <summary>
    /// Constructor <c>ScoreBoard</c> is the constructor for the score board object.
    /// </summary>
    public ScoreBoard(int value = 0) => Value = value;

    public override string ToString() => Value.ToString();
    public static ScoreBoard operator + (ScoreBoard a, int b) => new(a.Value + b);
}