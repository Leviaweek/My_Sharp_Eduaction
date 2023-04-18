namespace PacmanGame.Properties;
public readonly record struct Lives(int Value)
{
    public static Lives operator -- (Lives a) => new(a.Value - 1);
    public static Lives operator ++ (Lives a) => new(a.Value + 1);
    public override string ToString() => Value.ToString();
}