namespace WordsCount;

internal static class Program
{
    public static void Main()
    {
        var counter = new Count();
        counter.ReadFile("text.txt");
        counter.SaveSortedResult("result.txt");
    }
}