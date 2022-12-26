using System;
namespace DinnerParty
{
    class HelloWorld
    {
        private static string[] allFriends = Array.Empty<string>();
        private static double[] sumForAllFriends = Array.Empty<double>();
        
        private static void PrintAllFriendsAndSum()
        {
            for(var i = 0; i < allFriends?.Length; i++)
                Console.WriteLine($"{allFriends[i]} : {sumForAllFriends[i]}");
        }
        
        private static double[] CalculateSumForFriends(bool wantLuckyFriend, double totalAmount)
        {
            var sum = new double[allFriends.Length];
            if(wantLuckyFriend)
            {
                var randomFriend = ChoiceRandomFriend();

                for(var i = 0; i < allFriends.Length; i++)
                {

                    if(i == randomFriend)
                        continue;

                    sum[i] = totalAmount / (allFriends.Length - 1);
                }
            }
            
            else
                for(var i = 0; i < allFriends.Length; i++)
                    sum[i] = totalAmount / allFriends.Length;
                    
            return sum;
        }
        
        private static int ChoiceRandomFriend()
        {
            var rnd = new Random();
            var rndFrnd = rnd.Next(0, allFriends.Length);
            return rndFrnd;
        }
        
        private static bool UserLuckyFriendChoice()
        {
            Console.WriteLine("Do you want have lucky friend?(yes or no)");
            while(true)
            {
                Console.Write(">>> ");
                var userInput = Console.ReadLine()!;
                if(userInput.ToLower() == "yes")
                    return true;
                else if(userInput.ToLower() == "no")
                    return false;
                else
                    Console.WriteLine("Incorrect input");
            }
        }
        
        private static void PrintAllFriends()
        {
            foreach(var val in allFriends)
                Console.WriteLine(val);
        }

        private static string[] InputAllFriends(int numberOfFriends)
        {
            var frndList = new string[numberOfFriends];

            for(var i = 0; i < numberOfFriends; i++)
            {
                Console.Write(">>> ");
                frndList[i] = Console.ReadLine()!;
            }
            
            return frndList;
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
        
        private static void Main()
        {
            Console.WriteLine("\t\tDinnertParty\n");
            
            Console.WriteLine("It's your private DinnerParty");
            
            int numberOfFriends = PromptNumber("Write number of your friends(with you): ");
            
            if(numberOfFriends <= 1)
            {
                Console.WriteLine("Oh, you are so lonely :(");
                return;
            }
            
            Console.WriteLine("Input names your friends: ");
            allFriends = InputAllFriends(numberOfFriends);
            
            PrintAllFriends();
            
            var totalAmountInt = PromptNumber("Print total amount: ");
            var totalAmount = Convert.ToDouble(totalAmountInt);

            bool wantLuckyFriend = UserLuckyFriendChoice();
            
            sumForAllFriends = CalculateSumForFriends(wantLuckyFriend, totalAmount);
            
            PrintAllFriendsAndSum();
            
            Console.WriteLine("Goodbye");
        }

    }
}

