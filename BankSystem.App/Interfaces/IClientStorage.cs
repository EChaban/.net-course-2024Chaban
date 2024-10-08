using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankSystem.Domain.Models;

namespace BankSystem.App.Interfaces
{
    public interface IClientStorage : IStorage<Client>
    {
        void AddAccount(Client client, Account account);
        void UpdateAccount(Client client, Account oldAccount, Account newAccount);
        void DeleteAccount(Client client, Account account);
        List<Account> GetAccounts(Client client);
        List<Client> GetClients(int pageNumber, int pageSize, out int totalClients);
    }

}
