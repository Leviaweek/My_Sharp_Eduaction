using Questioncls;
using Testcls;
namespace Generator;

///<summary>
///static class <c>QuestionGenerator</c> generate tests all levels.
///</summary>
public static class QuestionGenerator
{
	/// <summary>
    /// This method generate test with parameters and return Question
    /// </summary>
    /// <param name="min">minimal generated number.</param>
	/// <param name="max">maximal generated number.</param>
	/// <param name="operations">all available operations to random choice.</param>
    public static Questions Generate(int min, int max, List<Operations> operations)
    {
        Random rnd = new();
        double firstNumber = rnd.Next(min, max);
        Operations chooseOperation = operations[rnd.Next(0, operations.Count)];
        double secondNumber = rnd.Next(min, max);
        double result = 0;
        string operation = "";
        switch(chooseOperation)
        {
            case Operations.Adding:
                result = firstNumber + secondNumber;
                operation = "+";
                break;
            case Operations.Substraction:
                result = firstNumber - secondNumber;
                operation = "-";
                break;
            case Operations.Multiplication:
                result = firstNumber * secondNumber;
                operation = "*";
                break;
            case Operations.Division:
                result = firstNumber / secondNumber;
                result = Math.Round(result, 2);
                operation = "/";
                break;
            case Operations.Exponentiation:
                result = Math.Pow(firstNumber, secondNumber);
                operation = "**";
                break;
        }
        return new Questions($"{firstNumber} {operation} {secondNumber}", result);
    }
}
