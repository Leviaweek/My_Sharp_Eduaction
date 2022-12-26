using Generator;
namespace Testcls;

///<summary>
///Enum <c>Operations</c> contains all used operations.
///</summary>
public enum Operations
{
    Adding,
    Substraction,
    Multiplication,
    Division,
    Exponentiation,
}

///<summary>
///Abstract class <c>Test</c> basis for all kinds of tests.
///</summary>
public abstract class Test
{
    /// <value>Field <c>operations</c> contain all operations types that will be used in the test.</value>
    protected List<Operations> operations;
    /// <value>Property<c>Min</c> is minimal used number for test.</value>
    protected int Min {get; init; }
    /// <value>Property<c>Max</c> is maximal used number for test.</value>
    protected int Max {get; init; }
    /// <value>Property<c>Name</c> contains test level name.</value>
    protected string Name {get; init; }
    /// <value>Property<c>TestsNumber</c> is number of tests.</value>
    protected int TestsNumber {get; init; }

    /// <summary>
    /// This constructor set all fields and properties
    /// </summary>
    protected Test()
    {
        Min = 0;
        Max = 0;
        Name = "";
        TestsNumber = 0;
        operations = new List<Operations> {};
    }

    /// <summary>
    /// This method is test main menu with questions and user answers
    /// </summary>
    public void TestMenu()
    {
        var trueAnswers = 0;
        for(var i = 1; i <= TestsNumber; i++)
        {
            var question = QuestionGenerator.Generate(Min, Max, operations);
            Console.WriteLine(question.ToString());
            var  userAnswer = DoubleInput();
            if(question.CheckAnswer(userAnswer))
            {
                trueAnswers += 1;
                Console.WriteLine("Done!");
            }
            else
                Console.WriteLine("Wrong!");
        }
        Console.WriteLine($"You have {trueAnswers} true answers from {TestsNumber}");
    }

    /// <summary>
    /// This method asks for user input, try convert him for double and return
    /// </summary>
    public static double DoubleInput()
    {
        while(true)
        {
            Console.Write(">>> ");
            var userAnswerStr = Console.ReadLine();
            if(double.TryParse(userAnswerStr, out var userAnswer))
                return userAnswer;
            else
                Console.WriteLine("Input only numbers");
        }
    }
}
