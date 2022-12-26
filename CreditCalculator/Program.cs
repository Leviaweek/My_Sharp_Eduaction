using Creditcls;
using Creditcls.Types;
namespace CreditCalculator;

enum CreditType
{
    Annuity = 1,
    Differencial = 2,
}

internal static class Program
{
    public static CreditType? creditType;
    public static float? principal;
    public static float? payment;
    public static int? periods;
    public static float? interest;

    public static void ParametersSetting(string[] args)
    {
        for (int i = 0; i < args.Length; i++)
        {
            switch(args[i])
            {
                case "--diff":
                    creditType = CreditType.Differencial;
                    continue;
                case "--annuity":
                    creditType = CreditType.Annuity;
                    continue;
                case "--principal":
                    if(float.TryParse(args[i+1], out float prin))
                        principal = prin;
                    i++;
                    continue;
                case "--payment":
                    if(float.TryParse(args[i+1], out float pay))
                        payment = pay;
                    i++;
                    continue;
                case "--periods":
                    if(int.TryParse(args[i+1], out int per))
                        periods = per;
                    i++;
                    continue;
                case "--interest":
                    if(float.TryParse(args[i+1], out float inter))
                        interest = inter / (12 * 100);
                    i++;
                    continue;
            }
        }
    }
    public static void Main(string[] args)
    {
        ParametersSetting(args);
        Credit credit;
        switch(creditType)
        {
            case CreditType.Annuity when(((principal != null && principal > 0)
                                        || (payment != null && payment > 0))
                                        && periods != null || periods > 0
                                        && interest != null || interest >= 0):
                credit = new Annuity(principal, payment, periods, interest);
                break;
            case CreditType.Differencial when(principal != null && principal > 0
                                            && payment == null
                                            && periods != null && periods > 0
                                            && interest != null && interest >= 0):
                credit = new Differencial(principal, periods, interest);
                break;
            default:
                Console.WriteLine("Incorrect parameters");
                return;
        }
        credit.Calculate();
    }
}