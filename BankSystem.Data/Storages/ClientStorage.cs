using System;
using System.Collections.Generic;
using System.Linq;
using BankSystem.Domain.Models;
using BankSystem.App.Interfaces;
using BankSystem.App.Exceptions;

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
            if (_clients.ContainsKey(client))
            {
                _clients[client] = _clients[client];
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
            return new List<Account>();
        }

        public List<Client> GetClients(int pageNumber, int pageSize, out int totalClients)
        {
            totalClients = _clients.Count;
            return _clients.Keys
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }
    }
}
