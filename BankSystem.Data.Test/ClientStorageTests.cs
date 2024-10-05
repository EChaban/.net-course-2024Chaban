using System;
using System.Collections.Generic;
using System.Linq;
using BankSystem.Data.Storages;
using BankSystem.App.Services;
using BankSystem.Domain.Models;
using Xunit;

namespace BankSystem.App.Tests
{
    public class ClientStorageTests
    {
        [Fact]
        public void AddClient_ShouldIncreaseClientCount()
        {
            // Arrange
            var storage = new ClientStorage();
            var client = new Client("Иван", "Иванов", new DateTime(1990, 1, 1), 001, 100, "Телефон", "Контракт");
            var accounts = new List<Account> { new Account(new Currency("RUB", '₽'), 1000) };

            // Act
            storage.AddClient(client, accounts);

            // Assert
            Assert.Equal(1, storage.Count());
        }

        [Fact]
        public void AddClientList_ShouldIncreaseClientCount()
        {
            // Arrange
            var storage = new ClientStorage();
            var clients = TestDataGenerator.GenerateClientWithSeveralAccounts();

            // Act
            storage.AddClientList(clients);

            // Assert
            Assert.Equal(clients.Count, storage.Count());
        }

        [Fact]
        public void GetYoungestClient_ShouldReturnYoungestClient()
        {
            // Arrange
            var storage = new ClientStorage();
            storage.AddClientList(TestDataGenerator.GenerateClientWithSeveralAccounts());

            var expectedYoungestClient = new Client("Иван", "Иванов", new DateTime(2007, 1, 1), 001, 100, "Телефон", "Контракт");
            var accounts = new List<Account> { new Account(new Currency("RUB", '₽'), 1000) };
            storage.AddClient(expectedYoungestClient, accounts);

            // Act
            var youngestClient = storage.GetYoungestClient();

            // Assert
            Assert.Equal(expectedYoungestClient, youngestClient);
        }

        [Fact]
        public void GetOldestClient_ShouldReturnOldestClient()
        {
            // Arrange
            var storage = new ClientStorage();
            storage.AddClientList(TestDataGenerator.GenerateClientWithSeveralAccounts());

            var expectedOldestClient = new Client("Иван", "Иванов", new DateTime(1944, 1, 1), 001, 100, "Телефон", "Контракт");
            var accounts = new List<Account> { new Account(new Currency("RUB", '₽'), 1000) };
            storage.AddClient(expectedOldestClient, accounts);

            // Act
            var oldestClient = storage.GetOldestClient();

            // Assert
            Assert.Equal(expectedOldestClient, oldestClient);
        }

        [Fact]
        public void GetAverageAge_ShouldReturnCorrectAverageAge()
        {
            // Arrange
            var storage = new ClientStorage();
            var client1 = new Client("Иван", "Иванов", new DateTime(1984, 1, 1), 001, 100, "Телефон", "Контракт"); // 40 лет
            var client2 = new Client("Петр", "Петров", new DateTime(1994, 1, 1), 002, 100, "Телефон", "Контракт"); // 30 лет
            var client3 = new Client("Сидор", "Сидоров", new DateTime(2004, 1, 1), 003, 100, "Телефон", "Контракт"); // 20 лет

            var accounts = new List<Account> { new Account(new Currency("RUB", '₽'), 1000) };

            storage.AddClient(client1, accounts);
            storage.AddClient(client2, accounts);
            storage.AddClient(client3, accounts);

            int expectedAverageAge = 30; // (40 + 30 + 20) / 3 = 30 лет

            // Act
            var actualAverageAge = storage.GetAverageAge();

            // Assert
            Assert.Equal(expectedAverageAge, actualAverageAge);
        }
    }
}
