using BankSystem.App.Exceptions;
using BankSystem.App.Interfaces;
using BankSystem.Domain.Models;

namespace BankSystem.Data.Storages
{
    public class ClientStorage : IClientStorage
    {
        private Dictionary<Client, List<Account>> _clients = new Dictionary<Client, List<Account>>();

        public List<Client> Get(Func<Client, bool> filter)
        {
            return _clients.Keys.Where(filter).ToList();
        }

        public void Add(Client client)
        {
            _clients.Add(client, new List<Account>());
        }

        public void Update(Client client)
        {
            var existingClient = _clients.Keys.FirstOrDefault(c => c.PassportNumber == client.PassportNumber);
            if (existingClient != null)
            {
                var accounts = _clients[existingClient];
                _clients.Remove(existingClient);
                _clients.Add(client, accounts);
            }
            else
            {
                throw new ClientValidationException("Client does not exist.");
            }
        }

        public void Delete(Client client)
        {
            _clients.Remove(client);
        }

        public void AddAccount(Client client, Account account)
        {
            if (_clients.ContainsKey(client))
            {
                _clients[client].Add(account);
            }
        }

        public void UpdateAccount(Client client, Account oldAccount, Account newAccount)
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

        public void DeleteAccount(Client client, Account account)
        {
            if (_clients.ContainsKey(client))
            {
                _clients[client].Remove(account);
            }
        }

        public List<Account> GetAccounts(Client client)
        {
            if (_clients.ContainsKey(client))
            {
                return _clients[client];
            }
            throw new ClientValidationException("Client does not exist.");
        }
    }
}
