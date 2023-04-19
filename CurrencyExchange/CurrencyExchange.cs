using System.Text.Json;
using System.Net;
using System.Net.Http.Json;
namespace CurrencyExchange;

public class CurrencyExchange
{
    private readonly HttpClient _httpClient;
    private string _url = "";
    private string _mainCurrency = "";

    public CurrencyExchange()
    {
        _httpClient = new();
        _httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla FIrefox 5.4");
    }
    public async Task ExchangeMenu()
    {
        GetStartCurrency();
        while (true)
        {
            Console.Write("Input currency to exchange: ");
            var currency = Console.ReadLine()!.ToLower();
            if (currency == "" || currency == _mainCurrency)
            {
                throw new InvalidCurrencyException("Invalid currency");
            }

            var responseStr = await _httpClient.GetStringAsync(_url);

            var response = JsonSerializer.Deserialize<Dictionary<string, Currency>>(responseStr);
            if (response == null)
            {
                throw new InvalidCurrencyException("Invalid currency or url");
            }
            if (!response.TryGetValue(currency, out var currencyObj))
            {
                throw new InvalidCurrencyException("Invalid currency");
            }
            Console.Write($"Input amount of {_mainCurrency}: ");
            if (!double.TryParse(Console.ReadLine(), out var amount))
            {
                throw new InvalidCurrencyException("Invalid amount");
            }
            Console.WriteLine($"You can exchange {amount * currencyObj.Rate} {currency}");
        }
    }
    private void GetStartCurrency()
    {
        Console.Write("Input start currency: ");
        _mainCurrency = Console.ReadLine()!.ToLower();
        _url = $"http://www.floatrates.com/daily/{_mainCurrency}.json";
    }
}