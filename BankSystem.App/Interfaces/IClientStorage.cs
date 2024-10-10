using BankSystem.Domain.Models;

namespace BankSystem.App.Interfaces
{
    public interface IClientStorage : IStorage<Client>
    {
        void AddAccount(Client client, Account account);
        void UpdateAccount(Client client, Account oldAccount, Account newAccount);
        void DeleteAccount(Client client, Account account);
        List<Account> GetAccounts(Client client);
    }
}