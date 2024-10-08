using BankSystem.Domain.Models;
using BankSystem.App.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BankSystem.App.Services
{
    public class ClientService
    {
        private readonly ClientStorage _clientStorage;

        public ClientService(ClientStorage clientStorage)
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
            _clientStorage.AddClient(client, new List<Account> { defaultAccount });
        }

        public void AddAdditionalAccount(Client client, Account account)
        {
            if (!_clientStorage.ClientExists(client))
                throw new ClientValidationException("Client does not exist.");

            _clientStorage.AddAccountToClient(client, account);
        }

        public void EditAccount(Client client, Account oldAccount, Account newAccount)
        {
            if (!_clientStorage.ClientExists(client))
                throw new ClientValidationException("Client does not exist.");

            _clientStorage.EditClientAccount(client, oldAccount, newAccount);
        }

        public IEnumerable<Client> GetClients(string? fullName = null, string? phoneNumber = null, string? passportNumber = null, DateTime? dateOfBirthFrom = null, DateTime? dateOfBirthTo = null)
        {
            return _clientStorage.GetClients(fullName, phoneNumber, passportNumber, dateOfBirthFrom, dateOfBirthTo);
        }
    }
}
