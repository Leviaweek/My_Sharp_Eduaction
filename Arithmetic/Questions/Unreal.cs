using Testcls;
namespace Arithmetic.Questions;
///<summary>
///Class <c>Unreal</c> with unreal test level
///</summary>
public class Unreal: Test
{
	/// <summary>
    /// This constructor assigns Min range, max range, level name, number of tests and available operations.
    /// </summary>
    public Unreal()
    {
        Min = 2;
        Max = 10;
        Name = "Unreal";
        TestsNumber = 25;
        operations = new List<Operations> {Operations.Exponentiation};
    }
}
