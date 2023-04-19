namespace CurrencyExchange;

public class InvalidCurrencyException: Exception
{
    public InvalidCurrencyException(string message): base(message){}
}