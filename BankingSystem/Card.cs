using System.Text.Json.Serialization;
namespace BankingSystem;

public sealed class Card
{

    public Card(string number, int pin, int balance)
    {
        Number = number;
        Pin = pin;
        Balance = balance;
    }
    [JsonPropertyName("number")]
    public string Number { get; set; }
    [JsonPropertyName("balance")]
    public int Balance { get; private set;}
    [JsonPropertyName("pin")]
    public int Pin { get; }

    public static bool CheckPin(Card card1, int pin) => card1.Pin == pin;
    public void AddIncome(int income) => Balance += income;
    public static void Transfer(Card card1, Card card2, int amount)
    {
        card1.Balance -= amount;
        card2.Balance += amount;
    }
    public override string ToString() => $"Number: {Number}\nBalance: {Balance}";
    public static bool operator ==(Card? a, Card? b) => a?.Number == b?.Number && a?.Pin == b?.Pin && a?.Balance == b?.Balance;
    public static bool operator !=(Card? a, Card? b) => a?.Number != b?.Number || a?.Pin != b?.Pin || a?.Balance != b?.Balance;
    public override bool Equals(object? obj) => obj is Card card && Number == card.Number && Pin == card.Pin && Balance == card.Balance;
    public override int GetHashCode() => HashCode.Combine(Number, Pin, Balance);
}