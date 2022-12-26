namespace Creditcls.Types;

public class Annuity: Credit
{   
    public float Principal {get; init; }
    public float Payment {get; init; }
    public int Periods {get; init; }
    public float Interest {get; init; }
    public int annuity;
    public int overpayment;

    public Annuity(float? principal, float? payment, int? periods, float? interest)
    {
        if(principal == null)
        {
            Payment = payment!.Value;
            Periods = periods!.Value;
            Interest = interest!.Value;
            Principal = -1;
        }
        else if(payment == null)
        {
            Payment = -1;
            Periods = periods!.Value;
            Interest = interest!.Value;
            Principal = principal!.Value;
        }
    }

    public override void Calculate()
    {
        if(Principal == -1)
        {   
            annuity = (int)Math.Ceiling((Payment) /
            ((Interest * Math.Pow(1 + Interest, Periods)) /
            (Math.Pow(1 + Interest, Periods) - 1)));
            overpayment = (int)Math.Ceiling((Payment * Periods) - annuity);
            Console.WriteLine($"Your loan principal = {annuity}!");
            Console.WriteLine($"Overpayment = {overpayment}");
        }
        else if(Payment == -1)
        {
            annuity = (int)Math.Ceiling((Principal) *
            ((Interest * Math.Pow(1 + Interest, Periods)) /
            (Math.Pow(1 + Interest, Periods) - 1)));
            overpayment = (int)Math.Ceiling(annuity - (Payment * Periods));
            Console.WriteLine($"Your annuity payment = {annuity}!");
            Console.WriteLine($"Overpayment = {overpayment}");
        }
        SaveResult();
    }
    public override void SaveResult()
    {
        using StreamWriter writer = new("result.txt", true);
        writer.WriteLine(DateTime.Now);
        writer.WriteLine("=========================");
        if (Principal == -1)
            writer.WriteLine($"Your loan principal = {annuity}!");
        else if (Payment == -1)
            writer.WriteLine($"Your annuity payment = {annuity}!");
        writer.WriteLine($"Overpayment = {overpayment}");
        writer.WriteLine("=========================\n");
        writer.Close();
        Console.WriteLine("Result was saved!");
    }
}