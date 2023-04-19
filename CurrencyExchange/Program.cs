namespace CurrencyExchange;

public static class Program
{
    public async static Task Main()
    {
        var currencyExchange = new CurrencyExchange();
        try
        {
            await currencyExchange.ExchangeMenu();
        }
        catch (InvalidCurrencyException e)
        {
            Console.WriteLine(e.Message);
        }
        catch (HttpRequestException)
        {
            Console.WriteLine("Invalid url");
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
        finally
        {
            Console.WriteLine("Bye!");
        }
    }
}