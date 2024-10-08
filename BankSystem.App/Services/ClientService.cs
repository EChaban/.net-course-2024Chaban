using BankSystem.Domain.Models;
using BankSystem.App.Exceptions;
using BankSystem.App.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BankSystem.App.Services
{
    public class ClientService
    {
        private readonly IClientStorage _clientStorage;

        public ClientService(IClientStorage clientStorage)
        {
            _clientStorage = clientStorage;
        }

        public void AddClient(Client client)
        {
            if (client.DateOfBirth.AddYears(18) > DateTime.Now)
                throw new ClientValidationException("Client must be at least 18 years old.");

            if (string.IsNullOrEmpty(client.PassportNumber))
                throw new ClientValidationException("Client must have passport data.");

            var defaultAccount = new Account(new Currency("USD", '$'), 0);
            _clientStorage.Add(client);
            _clientStorage.AddAccount(client, defaultAccount);
        }

        public void AddAdditionalAccount(Client client, Account account)
        {
            if (!_clientStorage.Get(c => c.Equals(client)).Any())
                throw new ClientValidationException("Client does not exist.");

            _clientStorage.AddAccount(client, account);
        }

        public void EditAccount(Client client, Account oldAccount, Account newAccount)
        {
            if (!_clientStorage.Get(c => c.Equals(client)).Any())
                throw new ClientValidationException("Client does not exist.");

            _clientStorage.UpdateAccount(client, oldAccount, newAccount);
        }

        public IEnumerable<Client> GetClients(string? fullName = null, string? phoneNumber = null, string? passportNumber = null, DateTime? dateOfBirthFrom = null, DateTime? dateOfBirthTo = null)
        {
            return _clientStorage.Get(c =>
                (string.IsNullOrEmpty(fullName) || $"{c.FirstName} {c.LastName}".Contains(fullName)) &&
                (string.IsNullOrEmpty(phoneNumber) || c.PhoneNumber == phoneNumber) &&
                (string.IsNullOrEmpty(passportNumber) || c.PassportNumber == passportNumber) &&
                (!dateOfBirthFrom.HasValue || c.DateOfBirth >= dateOfBirthFrom.Value) &&
                (!dateOfBirthTo.HasValue || c.DateOfBirth <= dateOfBirthTo.Value));
        }

        public IEnumerable<Account> GetAccountsByClient(Client client)
        {
            if (!_clientStorage.Get(c => c.Equals(client)).Any())
                throw new ClientValidationException("Client does not exist.");

            return _clientStorage.GetAccounts(client);
        }

        public void UpdateClient(Client client)
        {
            if (!_clientStorage.Get(c => c.Equals(client)).Any())
                throw new ClientValidationException("Client does not exist.");

            _clientStorage.Update(client);
        }

        public IEnumerable<Client> GetClients(int pageNumber, int pageSize, out int totalClients)
        {
            return _clientStorage.GetClients(pageNumber, pageSize, out totalClients);
        }
    }
}
