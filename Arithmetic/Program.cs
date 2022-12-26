using Testcls;
using Arithmetic.Questions;
namespace ArithmeticTest;

public static class Program
{
    public static void Main()
    {
        while(true)
        {
            Console.WriteLine("Choose level:");
            Console.WriteLine("1. Easy (simple operations with numbers 1-10)");
            Console.WriteLine("2. Normal (simple operations with numbers 1 - 100");
            Console.WriteLine("3. Hard (mixed operations with numbers 1 - 1000)");
            Console.WriteLine("4. Unreal(squares of numbers from 2 to 10)");
            Console.WriteLine("0. Exit");
            Console.WriteLine("(If the operation is division, then use rounding up to two numbers after the point for float numbers use dot)");
            Console.Write(">>> ");
            string userInput = Console.ReadLine()!;
            Test test;
            switch(userInput.Trim())
            {
                case "1":
                    test = new Easy();
                    break;
                case "2":
                    test = new Medium();
                    break;
                case "3":
                    test = new Hard();
                    break;
                case "4":
                    test = new Unreal();
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Incorrect input");
                    continue;
            }
            test.TestMenu();
        }
    }
}