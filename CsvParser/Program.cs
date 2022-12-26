namespace CsvParse;

internal static class Program
{
    public static void Main()
    {
        var newBase = new CsvParser();
        newBase.CreateBase();
        newBase.PrintAllStudents();
        newBase.SaveSortedResult();
    }
}
