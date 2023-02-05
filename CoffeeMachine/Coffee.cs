namespace CoffeeTypes
{
    public class Coffee 
    {   
        public string CoffeeName {get;set;}
        public int Water {get;set;}
        public int Milk {get;set;}
        public int Beans {get;set;}
        public int Cups {get;set;}
        public int Money {get;set;}
        public Coffee(string name, int water, int milk, int beans, int cups, int money)
        {
            CoffeeName = name;
        	Water = water;
        	Milk = milk;
        	Beans = beans;
        	Cups = cups;
        	Money = money;
        }
    }
}