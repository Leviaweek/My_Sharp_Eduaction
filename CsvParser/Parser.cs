using System.Globalization;
using System.Text;
using System.IO;
namespace CsvParse;

///<summary>
///class <c>CsvParser</c> split, sort and save documents.
///</summary>
public class CsvParser
{
    /// <summary>
    /// Field <c>groups</c> contain all groups.
    /// </summary>
    public List<Group> groups = new();

    /// <summary>
    /// Field <c>students</c> contain all students.
    /// </summary>
    public List<Student> students = new();

    /// <summary>
    /// Const Field <c>ParseStart</c> indicate start parsing.
    /// </summary>
    public const int ParseStart = 6;

    /// <summary>
    /// Const Field <c>ParseEnd</c> indicate end parsing.
    /// </summary>
    public const int ParseEnd = 63;

    /// <summary>
    /// Sorting students into groups
    /// </summary>
    public void SortGroups()
    {
        foreach (var student in students)
        {
            var match = false;
            foreach (var group in groups)
            {
                if (group.Name == student.Group)
                {
                    group.students.Add(student);
                    match = true;
                }
            }
            if (!match)
            {
                groups.Add(new Group(student.Group));
                groups[^1].students.Add(student);
            }
        }
    }

    /// <summary>
    /// Sorting students into rating
    ///</summary>
    public void RateSort()
    {
        foreach (var group in groups)
            for (var i = 1; i < group.students.Count; i++)
                for (var j = 0; j < group.students.Count - i; j++)
                    if (group.students[j].FullRating < group.students[j+1].FullRating)
                        (group.students[j], group.students[j+1]) = (group.students[j+1], group.students[j]);
    }

    /// <summary>
    /// Table row split
    /// </summary>
    /// <param name="text">Table line read.</param>
    public static List<string> Splitter(string text)
    {
        const char Separator = ',';
        var fragments = new List<string>();
        var isQuote = false;
        var line = new StringBuilder();
        foreach (var symbol in text)
        {
            if (symbol == Separator && !isQuote)
            {
                fragments.Add(line.ToString().Trim());
                line.Clear();
            }
            else if (symbol == '"')
            {
                isQuote = !isQuote;
            }
            else
                line.Append(symbol);
            }
        return fragments;
    }

    /// <summary>
    /// Creating student rate
    /// </summary>
    /// <param name="rateStr">Splitted table line.</param>
    public static List<double> CreateRate(List<string> rateStr)
    {
        var rates = new List<double>();
        for (var i = 5; i < rateStr.Count - 1; i++)
        {
            if (i == 29)
                continue;
            var text = rateStr[i].Replace(',', '.');
            if (double.TryParse(text, NumberStyles.Any, CultureInfo.InvariantCulture, out var value))
                rates.Add(value);
        }
        return rates;
    }

    /// <summary>
    /// Sum all student rates
    /// </summary>
    /// <param name="rates">All user rates.</param>
    public static double CreateFullRate(List<double> rates)
    {
        var fullRate = 0d;
        foreach (var rate in rates)
        {
            fullRate += rate;
        }
        return Math.Round(fullRate, 2);
    }

    /// <summary>
    /// Initialization max name length, max count length and max rating length
    /// </summary>
    public void CreatingTableSizes()
    {
        foreach (var group in groups)
        {
            group.maxCountLength = group.students.Count.ToString().Length;
            foreach (var student in group.students)
            {
                if (student.Name.Length > group.maxNameLength)
                    group.maxNameLength = student.Name.Length;
                if (student.FullRating.ToString().Length > group.maxRateLength)
                    group.maxRateLength = student.FullRating.ToString().Length;
            }
        }
    }

    /// <summary>
    /// Read line, call split method, close file, call group sort, rate sort and creatig table sizes
    /// </summary>
    public void CreateBase()
    {
        using StreamReader file = new("Table.csv");
        for (var i = 0; i < ParseEnd; i++)
        {
            var text = file.ReadLine();
            if (text == null)
                break;
            if (i < ParseStart)
                continue;
            var result = Splitter(text);
            var rate = CreateRate(result);
            var student = new Student(result[1].Replace("_с", ""), result[2], rate, CreateFullRate(rate));
            students.Add(student);
        }
        file.Close();
        SortGroups();
        RateSort();
        CreatingTableSizes();
    }

    /// <summary>
    /// Print count, name and rate sorted students
    /// </summary>
    /// <param name="group">Group with students.</param>
    public static void PrintGroup(Group group)
    {
        var count = 1;
        var maxLength = group.maxCountLength + group.maxNameLength + group.maxRateLength + 6;

        Console.WriteLine(new string('▇', maxLength + 4));
        Console.WriteLine($"▇{"▇".PadLeft(maxLength + 3)}");
        Console.Write($"▇{group.Name.PadLeft(maxLength/2 + 2)}");
        if (maxLength % 2 == 1)
            Console.WriteLine($"{"▇".PadLeft(maxLength/2 + 2)}");
        else
            Console.WriteLine($"{"▇".PadLeft(maxLength/2 + 1)}");
        Console.WriteLine($"▇{"▇".PadLeft(maxLength + 3)}");
        Console.WriteLine(new string('▇', maxLength + 4));
            foreach (var student in group.students)
                {
                    Console.Write($"▇ {count.ToString().PadRight(group.maxCountLength)} ▇");
                    if (student.FullRating < 60)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write($" {student.Name.PadRight(group.maxNameLength)} ");
                        Console.ResetColor();
                        Console.Write("▇");
                    }
                    else
                        Console.Write($" {student.Name.PadRight(group.maxNameLength)} ▇");

                    Console.WriteLine($" {student.FullRating.ToString().PadRight(group.maxRateLength)} ▇");
                    count++;
                }
            Console.WriteLine($"▇{"▇".PadLeft(group.maxCountLength+3)}{"▇".PadLeft(group.maxNameLength+3)}{"▇".PadLeft(group.maxRateLength+3)}");
            Console.WriteLine(new string('▇', maxLength + 4));
            Console.WriteLine();
    }

    /// <summary>
    /// Creating folder "SortedStudentTable" and save files into groups
    /// </summary>
    public void SaveSortedResult()
    {
        var currentFolder = Directory.GetCurrentDirectory();
        Directory.CreateDirectory("SortedStudentTable");
        
        foreach (var group in groups)
        {
            var count = 1;
            using var writer = new StreamWriter(Path.Join(currentFolder, "SortedStudentTable", $"{group.Name}.csv"), false);
            writer.WriteLine($"\"Number\", \"Name\", \"Full Rate\"");
            foreach (var student in group.students)
            {
                writer.WriteLine($"\"{count}\",\"{student.Name}\",\"{student.FullRating}\"");
                count++;
            }
            writer.Close();
        }
    }

    /// <summary>
    /// Iterates group in groups list and call Print Group method
    /// </summary>
    public void PrintAllStudents()
    {
        foreach (var group in groups)
        {
            PrintGroup(group);
        }
    }
}
