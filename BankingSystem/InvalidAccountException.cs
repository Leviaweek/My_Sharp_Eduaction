namespace BankingSystem
{
    public class InvalidAccountException: Exception
    {
        public InvalidAccountException(string message) : base(message) {}
    }
}