using System.Text.Json.Serialization;
namespace BankingSystem;
public sealed class Account
{
    public Account(Card card, string name)
    {
        Card = card;
        Name = name;

    }
    [JsonPropertyName("name")]
    public string Name { get; }
    [JsonPropertyName("card")]
    public Card Card { get; }

    public static bool operator ==(Account? a, Account? b) => a?.Card == b?.Card && a?.Name == b?.Name;
    public static bool operator !=(Account? a, Account? b) => a?.Card != b?.Card || a?.Name != b?.Name;
    public override bool Equals(object? obj) => obj is Account account && Card == account.Card && Name == account.Name;
    public override int GetHashCode() => HashCode.Combine(Card, Name);
    public override string ToString() => $"Name: {Name}\nCard: {Card}";
}