using Testcls;
namespace Arithmetic.Questions;
///<summary>
///Class <c>Hard</c> with hard test level
///</summary>
public class Hard: Test
{
	/// <summary>
    /// This contstructor assigns Min range, max range, level name, number of tests and available operations.
    /// </summary>
    public Hard()
    {
        Min = 1;
        Max = 1000;
        Name = "Hard";
        TestsNumber = 20;
        operations = new List<Operations> {Operations.Adding, Operations.Substraction, Operations.Multiplication, Operations.Division};
    }
}
