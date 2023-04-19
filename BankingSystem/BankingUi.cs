namespace BankingSystem;

public sealed class BankingUi
{
    private readonly DataBase _dataBase;
    private UiState _state;
    private Account? _account;
    public BankingUi()
    {
        _dataBase = new DataBase();
        _state = UiState.NotLogged;
        _account = null;
        Logger = new Logger();
    }
    public Logger Logger { get; }

    public void Menu()
    {
        Console.CancelKeyPress += (sender, args) =>
        {
            var logMessage = new LogMessage("System", "Info", "Exiting", "Exit");
            Logger.Write(logMessage);
            _state = UiState.Exit;
        };
        while (true)
        {
            switch (_state)
            {
                case UiState.NotLogged:
                    NotLoggedMenu();
                    break;
                case UiState.Logged:
                    LoggedMenu();
                    break;
                case UiState.Exit:
                    return;
            }
        }
    }
    private void NotLoggedMenu()
    {
        Console.WriteLine("1. Create account");
        Console.WriteLine("2. Log into account");
        Console.WriteLine("0. Exit");
        Console.Write("Input number: ");
        switch (Console.ReadLine())
        {
            case "1":
                CreateAccount();
                break;
            case "2":
                LogIntoAccount();
                break;
            case "0":
                var logMessage = new LogMessage("System", "Info", "Exiting", "Exit");
                Logger.Write(logMessage);
                _state = UiState.Exit;
                return;
            default:
                Console.WriteLine("Unknown command");
                break;
        }
    }
    private void LoggedMenu()
    {
        Console.WriteLine("1. Balance");
        Console.WriteLine("2. Add income");
        Console.WriteLine("3. Do transfer");
        Console.WriteLine("4. Close account");
        Console.WriteLine("5. Log out");
        Console.WriteLine("0. Exit");
        Console.Write("Input number: ");
        switch (Console.ReadLine())
        {
            case "1":
                Console.WriteLine($"Balance: {_account!.Card.Balance}");
                var logMessage = new LogMessage(_account.Name, "Info", "GetBalance", $"Balance: {_account.Card.Balance}");
                Logger.Write(logMessage);
                break;
            case "2":
                AddIncome();
                break;
            case "3":
                DoTransfer();
                break;
            case "4":
                CloseAccount();
                break;
            case "5":
                _state = UiState.NotLogged;
                _account = null;
                break;
            case "0":
                _state = UiState.Exit;
                break;
            default:
                Console.WriteLine("Unknown command");
                break;
        }
    }
    private void CloseAccount()
    {
        _dataBase.Remove(_account!);
        var logMessage = new LogMessage(_account!.Name, "Info", "CloseAccount", "Account was closed");
        Logger.Write(logMessage);
        _state = UiState.NotLogged;
        _account = null;
        Console.WriteLine("The account has been closed!");
    }
    private void DoTransfer()
    {
        Console.WriteLine("Transfer");
        Console.Write("Input card number: ");
        var cardNumber = Console.ReadLine();
        if (cardNumber == _account!.Card.Number)
        {
            Console.WriteLine("You can't transfer money to the same account!");
            var ErrorLogMessage = new LogMessage(_account!.Name, "Error", "DoTransfer", "You can't transfer money to the same account");
            Logger.Write(ErrorLogMessage);
            return;
        }
        var account = _dataBase.Find(cardNumber!);
        if (account == null)
        {
            var ErrorLogMessage = new LogMessage(_account!.Name, "Error", "DoTransfer", "Such a card does not exist");
            Console.WriteLine(ErrorLogMessage.ToString());
            Logger.Write(ErrorLogMessage);
            return;
        }
        var transfer = InputIncome();
        if (_account!.Card.Balance < transfer)
        {
            Console.WriteLine("Not enough money!");
            var ErrorLogMessage = new LogMessage(_account.Name, "Error", "DoTransfer", "Not enough money");
            Logger.Write(ErrorLogMessage);
            return;
        }
        Card.Transfer(_account.Card, account.Card, transfer);
        _dataBase.AddChanges(_account, account!);
        var logMessage = new LogMessage(_account.Name, "Info", "DoTransfer", $"Transfer: {transfer}");
        Logger.Write(logMessage);
        Console.WriteLine("Success!");
    }
    private void AddIncome()
    {
        var income = InputIncome();
        _account!.Card.AddIncome(income);
        _dataBase.AddChanges(_account!);
        var logMessage = new LogMessage(_account.Name, "Info", "AddIncome", $"Income: {income}");
        Logger.Write(logMessage);
        Console.WriteLine("Income was added!");
    }
    private int InputIncome()
    {
        while (true)
        {
            Console.WriteLine("Enter income:");
            if (!int.TryParse(Console.ReadLine(), out int income))
            {
                var logMessage = new LogMessage(
                    author: "System",
                    logLevel: "Error",
                    action: "InputIncome",
                    message: "Wrong income"
                    );
                Console.WriteLine(logMessage.ToString());
                Logger.Write(logMessage);
                continue;
            }
            return income;
        }
    }
    private void CreateAccount()
    {
        Console.Write("Input name: ");
        var name = Console.ReadLine();
        if (string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name))
        {
            var ErrorLogMessage = new LogMessage("System", "Error", "CreateAccount", "Name can't be empty");
            Console.WriteLine(ErrorLogMessage.ToString());
            Logger.Write(ErrorLogMessage);
            return;
        }
        string cardNumber;
        while (true)
        {
            cardNumber = CreateCardNumber();
            var account = _dataBase.Find(cardNumber);
            if (account != null)
                continue;
            break;
        }
        var pin = CreatePin();
        Console.WriteLine($"Your card number: {cardNumber}");
        Console.WriteLine($"Your card PIN: {pin}");
        var logMessage = new LogMessage(
            author: name,
            logLevel: "Info",
            action: "CreateAccount",
            message: $"Account created: Name: {name}\n{cardNumber}\nPin: {pin}"
            );
        Logger.Write(logMessage);
        _account = new Account(
                        card: new Card(cardNumber, pin, 0),
                        name: name
                        );
        _dataBase.Add(_account);
        Console.WriteLine("Your card has been created");
    }
    private static string CreateCardNumber()
    {
        string cardNumber = "400000";
        Random random = new();
        for (int i = 0; i < 9; i++)
        {
            cardNumber += random.Next(0, 10).ToString();
        }
        cardNumber += LuhnAlgorithm(cardNumber);
        return cardNumber;
    }
    private static int CreatePin()
    {
        return new Random().Next(1000, 10000);
    }
    private static string LuhnAlgorithm(string cardNumber)
    {
        int sum = 0;
        for (int i = 0; i < cardNumber.Length; i++)
        {
            int digit = int.Parse(cardNumber[i].ToString());
            if (i % 2 == 0)
            {
                digit *= 2;
                if (digit > 9)
                {
                    digit -= 9;
                }
            }
            sum += digit;
        }
        return (10 - sum % 10).ToString();
    }
    private static bool CheckLuhnAlgorithm(string cardNumber)
    {
        int sum = 0;
        for (int i = 0; i < cardNumber.Length; i++)
        {
            int digit = int.Parse(cardNumber[i].ToString());
            if (i % 2 == 0)
            {
                digit *= 2;
                if (digit > 9)
                {
                    digit -= 9;
                }
            }
            sum += digit;
        }
        return sum % 10 == 0;
    }
    private Account? InputAccount()
    {
        Console.Write("Enter name: ");
        var name = Console.ReadLine();
        if (string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name))
        {
            var logMessage = new LogMessage("Guest", "Error", "LogIntoAccount", "Name can't be empty");
            Console.WriteLine(logMessage.ToString());
            Logger.Write(logMessage);
            return null;
        }
        Console.Write("Enter card number: ");
        var cardNumber = Console.ReadLine();
        if (cardNumber == null || cardNumber.Length != 16 || !CheckLuhnAlgorithm(cardNumber))
        {
            var logMessage = new LogMessage(name, "Error", "LogIntoAccount", "Wrong card number");
            Console.WriteLine(logMessage.ToString());
            Logger.Write(logMessage);
            return null;
        }
        Console.Write("Enter PIN: ");
        if (!int.TryParse(Console.ReadLine(), out int pin))
        {
            var logMessage = new LogMessage(name, "Error", "LogIntoAccount", "Wrong PIN");
            Console.WriteLine(logMessage.ToString());
            Logger.Write(logMessage);
            return null;
        }
        var account = _dataBase.Find(cardNumber);
        if (account is null)
        {
            var logMessage = new LogMessage(name, "Error", "LogIntoAccount", "User not in database");
            Console.WriteLine(logMessage.ToString());
            Logger.Write(logMessage);
            return null;
        }
        if (!Card.CheckPin(account.Card, pin))
        {
            var logMessage = new LogMessage(name, "Error", "LogIntoAccount", "Invalid PIN");
            Console.WriteLine(logMessage.ToString());
            Logger.Write(logMessage);
            return null;
        }
        return account;
    }
    private void LogIntoAccount()
    {
        var account = InputAccount();
        if (account is null)
        {
            return;
        }
        _account = account;
        Console.WriteLine("You have successfully logged in!");
        var logMessage = new LogMessage(account.Name, "Info", "LogIntoAccount", "User logged in");
        Logger.Write(logMessage);
        _state = UiState.Logged;
    }
}