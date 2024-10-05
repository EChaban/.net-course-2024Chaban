using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BankSystem.Domain.Models;

namespace BankSystem.Data.Storages
{
    public class ClientStorage
    {
        private Dictionary<Client, List<Account>> _clients = new Dictionary<Client, List<Account>>();

        public void AddClient(Client client, List<Account> accounts)
        {
            _clients.Add(client, accounts);
        }

        public void AddClientList(Dictionary<Client, List<Account>> accounts)
        {
            foreach (var account in accounts)
            {
                _clients.Add(account.Key, account.Value);
            }
        }

        public int Count()
        {
            return _clients.Count;
        }

        public Client GetYoungestClient()
        {
            if (_clients.Count == 0) return null;
            return _clients.Keys.MaxBy(c => c.DateOfBirth);
        }

        public Client GetOldestClient()
        {
            if (_clients.Count == 0) return null;
            return _clients.Keys.MinBy(c => c.DateOfBirth);
        }

        public int GetAverageAge()
        {
            if (_clients.Count == 0) return 0;

            int totalAge = _clients.Keys.Sum(c => UtilityMethods.CalculateAge(c.DateOfBirth));

            return totalAge / _clients.Count;
        }

        public bool ClientExists(Client client)
        {
            return _clients.ContainsKey(client);
        }

        public void AddAccountToClient(Client client, Account account)
        {
            if (_clients.ContainsKey(client))
            {
                _clients[client].Add(account);
            }
        }

        public void EditClientAccount(Client client, Account oldAccount, Account newAccount)
        {
            if (_clients.ContainsKey(client))
            {
                var accounts = _clients[client];
                var index = accounts.IndexOf(oldAccount);
                if (index != -1)
                {
                    accounts[index] = newAccount;
                }
            }
        }

        public List<Account> GetAccountsByClient(Client client)
        {
            if (_clients.TryGetValue(client, out var accounts))
            {
                return accounts;
            }
            return new List<Account>();
        }

        public IEnumerable<Client> GetClients(string fullName = null, string phoneNumber = null, string passportNumber = null, DateTime? dateOfBirthFrom = null, DateTime? dateOfBirthTo = null)
        {
            return _clients.Keys.Where(c =>
                (string.IsNullOrEmpty(fullName) || $"{c.FirstName} {c.LastName}".Contains(fullName)) &&
                (string.IsNullOrEmpty(phoneNumber) || c.PhoneNumber == phoneNumber) &&
                (string.IsNullOrEmpty(passportNumber) || c.PassportNumber == passportNumber) &&
                (!dateOfBirthFrom.HasValue || c.DateOfBirth >= dateOfBirthFrom.Value) &&
                (!dateOfBirthTo.HasValue || c.DateOfBirth <= dateOfBirthTo.Value));
        }
    }
}
