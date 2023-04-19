using System.IO;
using System.Collections.Generic;
using System.Text.Json;
namespace BankingSystem
{
    public sealed class DataBase
    {
        private readonly string _fileName;
        public DataBase()
        {
            _fileName = "accounts.json";
        }

        public List<Account> GetAccounts()
        {
            using var sr = new StreamReader(_fileName);
            string json = sr.ReadToEnd();
            if (json == "")
            {
                return new List<Account>();
            }
            List<Account>? accounts = JsonSerializer.Deserialize<List<Account>>(json);
            return accounts ?? new List<Account>();
        }
        public void AddChanges(params Account[] accounts)
        {
            List<Account> allAccounts = GetAccounts();
            for (int i = 0; i < allAccounts.Count; i++)
            {
                for (int j = 0; j < accounts.Length; j++)
                {
                    if (allAccounts[i].Card.Number == accounts[j].Card.Number)
                    {
                        allAccounts[i] = accounts[j];
                    }
                }
            }
            string json = JsonSerializer.Serialize(accounts);
            File.WriteAllText(_fileName, json);
        }
        public void Add(params Account[] accounts)
        {
            if (!File.Exists(_fileName))
            {
                File.Create(_fileName).Close();
            }
            List<Account> allAccounts = GetAccounts();
            for (int i = 0; i < accounts.Length; i++)
            {
                if (!allAccounts.Contains(accounts[i]))
                {
                    allAccounts.Add(accounts[i]);
                    continue;
                }
                throw new InvalidAccountException("Account already exists");
            }
            string json = JsonSerializer.Serialize(accounts);
            File.WriteAllText(_fileName, json);
        }
        public void Remove(Account account)
        {
            List<Account> accounts = GetAccounts();
            Console.WriteLine(account);
            _ = accounts.Remove(account);
            string json = JsonSerializer.Serialize(accounts);
            File.WriteAllText(_fileName, json);
        }
        public Account? Find(string number)
        {
            List<Account> accounts = GetAccounts();
            foreach (Account account in accounts)
            {
                if (account.Card.Number == number)
                {
                    return account;
                }
            }
            return null;
        }
    }
}