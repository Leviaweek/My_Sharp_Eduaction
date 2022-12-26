using System;
namespace ChatBot
{
	public static class Program
	{
	    private const string BotName = "ChatBot C#";
		private const int BirthYear = 2022;
	    
	    private static void YearGuessing()
		{
		    Console.WriteLine("Let me guess your age.");
			Console.WriteLine("Enter remainders of dividing your age by 3, 5 and 7.");

			var remainder3 = PromptNumber("Please, write your reminder by 3");
			var remainder5 = PromptNumber("Please, write your remainder by 5");
			var remainder7 = PromptNumber("Please, write you remainder by 7");

			var age = YearCalc(remainder3, remainder5, remainder7);
            Console.WriteLine($"Your age is {age}");
		}
		
		private static int YearCalc(int remainder3, int remainder5, int remainder7)
		{
		    return (remainder3 * 70 + remainder5 * 21 + remainder7 * 15) % 105;
		}

		private static int PromptNumber(string text)
		{
		    Console.WriteLine(text);
		    while (true)
		    {
		        Console.Write(">>> ");
		        var numberStr = Console.ReadLine();
		        var numParsing = int.TryParse(numberStr, out int number);
		        if(numParsing)
		            return number;
		        else
		            Console.WriteLine("Incorrect input");
		    }
		}

		private static void Counting()
		{
		    var countNumber = PromptNumber("Now I will prove to you that I can count to any number you want.");

		    for(var i = 0; i <= countNumber; i++)
		        Console.WriteLine($"{i}!");

		    Console.WriteLine("Done!");
		}
		
		private static void Test()
		{
		    Console.WriteLine("Let's test your programming knowledge.");
		    Console.WriteLine("What is the best programming language?:");
		    Console.WriteLine("1. Python.\n2. C++.\n3. C#.\n4. English.");
		    while(true)
		    {
		        Console.Write(">>> ");
		        var userResponse = Console.ReadLine();
		        
		        if(userResponse == "3")
		        {
		            Console.WriteLine("Yes, it's C#");
		            break;
		        }
		        
		        else
		            Console.WriteLine("False");
		    }
		}
		
		private static void Greeting()
		{
		    Console.Write("Please, remind my your name.\n>>> ");
		    var userName = Console.ReadLine();
			Console.WriteLine($"What a great name you have, {userName}!");
		}

		public static void Main()
		{
			Console.WriteLine($"Hello! My name is {BotName}.\nI was created in {BirthYear}.");
			
			Greeting();

			YearGuessing();

            Counting();

            Test();
            
            Console.WriteLine("Congratulations, have a nice day!");
		}
	}
}