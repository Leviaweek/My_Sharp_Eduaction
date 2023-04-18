namespace PacmanGame.Objects;
public struct ScoreBoard
{
    public int Value { get; set; }
    public ScoreBoard(int value = 0) => Value = value;
    public override string ToString() => Value.ToString();
    public static ScoreBoard operator + (ScoreBoard a, int b) => new(a.Value + b);
}