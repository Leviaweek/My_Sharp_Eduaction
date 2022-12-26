using System.Text;
namespace Creditcls.Types;

public class Differencial: Credit
{
    public float Principal {get; init; }
    public int Periods {get; init; }
    public float Interest {get; init; }
    public int differencial;
    public int overpayment;
    public StringBuilder log = new("=========================\n");

    public Differencial(float? principal, int? periods, float? interest)
    {
        Principal = principal!.Value;
        Periods = periods!.Value;
        Interest = interest!.Value;
    }

    public override void Calculate()
    {
        int sum = 0;
        for(var i = 1; i <= Periods; i++)
        {
            differencial = (int)Math.Ceiling((Principal / Periods) + Interest * (Principal - ((Principal * (i - 1) / Periods))));
            sum += differencial;
            Console.WriteLine($"Month {i}: payment is {differencial}");
            log.Append($"Month {i}: payment is {differencial}\n");
        }
        overpayment = sum - (int)Math.Ceiling(Principal);
        Console.WriteLine($"Overpayment = {overpayment}");
        log.Append($"Overpayment = {overpayment}\n");
        log.Append("=========================\n");
        SaveResult();
    }

    public override void SaveResult()
    {
        using StreamWriter writer = new("result.txt", true);
        writer.WriteLine(DateTime.Now);
        writer.WriteLine(log);
        Console.WriteLine("Result was saved!");
    }
}