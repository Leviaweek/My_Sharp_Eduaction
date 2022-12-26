namespace CsvParse;

///<summary>
///Class <c>Group</c> is a group students.
///</summary>
public class Group
{
    /// <value>
    /// Property <c>Name</c> is a group name.
    /// </value>
    public string Name {get; init; }

    /// <summary>
    /// Field <c>students</c> contain all students in group.
    /// </summary>
    public List<Student> students = new();

    /// <summary>
    /// Field <c>maxNameLength</c> is max name length value for console table.
    /// </summary>
    public int maxNameLength = 0;

    /// <summary>
    /// Field <c>maxCountLength</c> is max student number length value for console table.
    /// </summary>
    public int maxCountLength = 0;

    /// <summary>
    /// Field <c>maxRateLength</c> is max student rate length value for console table.
    /// </summary>
    public int maxRateLength = 0;

    /// <summary>
    /// Constructor initializes group and set group name
    /// </summary>
    /// <param name="name">Group name.</param>
    public Group(string name)
    {
        Name = name;
    }
}