using CoffeeTask;
namespace HelloWold
{
    public static class Program 
    {         
        public static void Main()
        {
            var Machine = new CoffeeMachine();
            while(true)
            {
                Console.Write(">>> ");
                var userInput = Console.ReadLine()!.ToLower();
                if(userInput == "exit")
                    break;
                Machine.MachineMenu(userInput);
            }
            Console.WriteLine("Goodbye");
        }
    }
}