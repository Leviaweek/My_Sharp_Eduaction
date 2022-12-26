using Testcls;
namespace Arithmetic.Questions;
///<summary>
///Class <c>Medium</c> with medium test level
///</summary>
public class Medium: Test
{
	/// <summary>
    /// This contstructor assigns Min range, max range, level name, number of tests and available operations.
    /// </summary>
    public Medium()
    {
        Min = 1;
        Max = 100;
        Name = "Medium";
        TestsNumber = 15;
        operations = new List<Operations> {Operations.Adding, Operations.Substraction, Operations.Multiplication, Operations.Division};
    }
}
