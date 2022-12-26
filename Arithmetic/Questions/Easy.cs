using Testcls;
namespace Arithmetic.Questions;
///<summary>
///Class <c>Easy</c> with easy test level
///</summary>
public class Easy: Test
{
	/// <summary>
    /// This contstructor assigns Min range, max range, level name, number of tests and available operations.
    /// </summary>
    public Easy()
    {
        Min = 1;
        Max = 10;
        Name = "Easy";
        TestsNumber = 10;
        operations = new List<Operations> {Operations.Adding, Operations.Substraction};
    }
}
