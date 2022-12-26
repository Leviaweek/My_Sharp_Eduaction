namespace CsvParse;

/// <summary>
/// Class <c>Student</c> represents one student
/// </summary>
public class Student
{
    /// <value>
    /// Property <c>Name</c> is a student name.
    /// </value>
    public string Name {get; init; }

    /// <value>
    /// Property <c>Rate</c> is a student all rates list.
    /// </value>
    public List<double> Rate {get; init; }

    /// <value>
    /// Property <c>FullRating</c> is a sum of all student rates.
    /// </value>
    public double FullRating {get; init; }

    /// <value>
    /// Property <c>Group</c> is the name of the group to which the student belongs.
    /// </value>
    public string Group {get; init; }

    /// <summary>
    /// Constructor initializes all information about student from the table
    /// </summary>
    /// <param name="group">Group name.</param>
    /// <param name="name">Student name.</param>
    /// <param name="rate">All student rates.</param>
    /// <param name="fullRating">Sum of all student rates.</param>
    public Student(string group, string name, List<double> rate, double fullRating)
    {
        Group = group;
        Name = name;
        Rate = rate;
        FullRating = fullRating;
    }
}