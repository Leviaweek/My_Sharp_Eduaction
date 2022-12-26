using CoffeeTypes;
namespace CoffeeTask
{
	enum MachineState
	{
		WaitingForAction,
		UserChooseCoffee,
        WaitingForWater,
        WaitingForMilk,
        WaitingForBeans,
        WaitingForCups,
	}
    public class CoffeeMachine 
    {
        private MachineState State {get;set;}
        public int Water {get;set;}
        public int Milk {get;set;}
        public int Beans {get;set;}
        public int Cups {get;set;}
        public int Money {get;set;}
        private readonly List<Coffee> _coffies = new();
        
        public CoffeeMachine()
        {
        	State = MachineState.WaitingForAction;
        	Water = 400;
        	Milk = 540;
        	Beans = 120;
        	Cups = 9;
        	Money = 550;
        	_coffies.Add(new Coffee("Espresso", 250, 0, 16, 1, 4));
        	_coffies.Add(new Coffee("Latte", 350, 75, 20, 1, 7));
            _coffies.Add(new Coffee("Cappucino", 200, 100, 12, 1, 6));
            MachinePriner();
        }
        public void MachinePriner()
        {
            switch(State)
            {
            case MachineState.WaitingForAction:
                Console.WriteLine("Write action (buy, fill, take, remaining, exit):");
                break;
            case MachineState.UserChooseCoffee:
                Console.WriteLine("What do you want to buy? 1 - espresso, 2 - latte, 3 - cappuccino, back – to main menu:");
                break;
            case MachineState.WaitingForWater:
                Console.WriteLine("Write water amount:");
                break;
            case MachineState.WaitingForMilk:
                Console.WriteLine("Write milk amount:");
                break;
            case MachineState.WaitingForBeans:
                Console.WriteLine("Write beans amount:");
                break;
            case MachineState.WaitingForCups:
                Console.WriteLine("Write cups amount:");
                break;
            }
        }

        private void CoffeeCreating(Coffee CoffeeType)
        {
            Console.WriteLine($"One {CoffeeType.CoffeeName}");
            if(CoffeeType.Water > Water)
            {
                Console.WriteLine("I don't have water");
                return;
            }
            else if(CoffeeType.Milk > Milk)
            {
                Console.WriteLine("I don't have milk");
                return;
            }
            else if(CoffeeType.Beans > Beans)
            {
                Console.WriteLine("I don't have beans");
                return;
            }
            else if(CoffeeType.Cups > Cups)
            {
                Console.WriteLine("I don't have cups");
                return;
            }
            Water -= CoffeeType.Water;
            Milk -= CoffeeType.Milk;
            Beans -= CoffeeType.Beans;
            Cups -= CoffeeType.Cups;
            Money -= CoffeeType.Money;
            State = MachineState.WaitingForAction;
            Console.WriteLine("Done!");
            Console.WriteLine($"Your {CoffeeType.CoffeeName}");
        }

        private void Remaining()
        {
            Console.WriteLine($"{Water} of water");
            Console.WriteLine($"{Milk} of milk");
            Console.WriteLine($"{Beans} of beans");
            Console.WriteLine($"{Cups} of cups");
            Console.WriteLine($"{Money} of money");
        }

        private void Take(string userInput)
        {
            var inputCheck = int.TryParse(userInput, out int userInputNumber);
            if(!inputCheck || userInputNumber < 0)
            {
                Console.WriteLine("Input only positive numbers!");
                return;
            }
            switch(State)
            {
                case MachineState.WaitingForWater:
                    State = MachineState.WaitingForMilk;
                    Water += userInputNumber;
                    break;
                case MachineState.WaitingForMilk:
                    State = MachineState.WaitingForBeans;
                    Milk += userInputNumber;
                    break;
                case MachineState.WaitingForBeans:
                    State = MachineState.WaitingForCups;
                    Beans += userInputNumber;
                    break;
                case MachineState.WaitingForCups:
                    State = MachineState.WaitingForAction;
                    Cups += userInputNumber;
                    break;
            }
        }

        public void MachineMenu(string userInput)
        {
            switch(State)
            {
                case MachineState.WaitingForAction:
                    switch(userInput)
                    {
                        case "buy":
                            State = MachineState.UserChooseCoffee;
                            break;
                        case "fill":
                            State = MachineState.WaitingForWater;
                            break;
                        case "take":
                            Console.WriteLine($"I gave you {Money}$");
                            Money = 0;
                            break;
                        case "remaining":
                            Remaining();
                            break;
                        default:
                            Console.WriteLine("Unknown command");
                            break;
                    }
                    break;

                case MachineState.UserChooseCoffee:

                    if(userInput == "back")
                    {
                        State = MachineState.WaitingForAction;
                        break;
                    }

                    _ = int.TryParse(userInput, out int userInputNumber);
                    switch(userInputNumber)
                    {
                        case > 0 and < 4:
                            CoffeeCreating(_coffies[userInputNumber-1]);
                            break;
                        default:
                            Console.WriteLine("Unknown Coffee Type");
                            break;
                    }
                    break;
                
                case MachineState.WaitingForWater or MachineState.WaitingForBeans or MachineState.WaitingForMilk or MachineState.WaitingForCups:
                    Take(userInput);
                    break;
            }
            MachinePriner();
        }
    }
}