using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BankSystem.App.Services;
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
    }
}
