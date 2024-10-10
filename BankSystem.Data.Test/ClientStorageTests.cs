using System;
using System.Collections.Generic;
using System.Linq;
using BankSystem.App.Services;
using BankSystem.Data;
using BankSystem.Data.Storages;
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
            var client = new Client("Иван", "Иванов", new DateTime(1990, 1, 1), 001, "Телефон", "Контракт", "1234567890");

            // Act
            storage.Add(client);

            // Assert
            Assert.Single(storage.Get(c => true));
        }

        [Fact]
        public void AddClientList_ShouldIncreaseClientCount()
        {
            // Arrange
            var storage = new ClientStorage();
            var clients = TestDataGenerator.GenerateClientWithSeveralAccounts();

            // Act
            foreach (var client in clients)
            {
                storage.Add(client.Key);
            }

            // Assert
            Assert.Equal(clients.Count, storage.Get(c => true).Count);
        }

        [Fact]
        public void GetYoungestClient_ShouldReturnYoungestClient()
        {
            // Arrange
            var storage = new ClientStorage();
            var clients = TestDataGenerator.GenerateClientWithSeveralAccounts();
            foreach (var client in clients)
            {
                storage.Add(client.Key);
            }

            var expectedYoungestClient = new Client("Иван", "Иванов", new DateTime(2007, 1, 1), 001, "Телефон", "Контракт", "1234567890");
            storage.Add(expectedYoungestClient);

            // Act
            var youngestClient = storage.Get(c => true).OrderByDescending(c => c.DateOfBirth).FirstOrDefault();

            // Assert
            Assert.Equal(expectedYoungestClient, youngestClient);
        }

        [Fact]
        public void GetOldestClient_ShouldReturnOldestClient()
        {
            // Arrange
            var storage = new ClientStorage();
            var clients = TestDataGenerator.GenerateClientWithSeveralAccounts();
            foreach (var client in clients)
            {
                storage.Add(client.Key);
            }

            var expectedOldestClient = new Client("Иван", "Иванов", new DateTime(1944, 1, 1), 001, "Телефон", "Контракт", "1234567890");
            storage.Add(expectedOldestClient);

            // Act
            var oldestClient = storage.Get(c => true).OrderBy(c => c.DateOfBirth).FirstOrDefault();

            // Assert
            Assert.Equal(expectedOldestClient, oldestClient);
        }

        [Fact]
        public void GetAverageAge_ShouldReturnCorrectAverageAge()
        {
            // Arrange
            var storage = new ClientStorage();
            var client1 = new Client("Иван", "Иванов", new DateTime(1984, 1, 1), 001, "Телефон", "Контракт", "1234567890"); // 40 лет
            var client2 = new Client("Петр", "Петров", new DateTime(1994, 1, 1), 002, "Телефон", "Контракт", "1234567891"); // 30 лет
            var client3 = new Client("Сидор", "Сидоров", new DateTime(2004, 1, 1), 003, "Телефон", "Контракт", "1234567892"); // 20 лет

            storage.Add(client1);
            storage.Add(client2);
            storage.Add(client3);

            int expectedAverageAge = 30; // (40 + 30 + 20) / 3 = 30 лет

            // Act
            var clients = storage.Get(c => true);
            int totalAge = clients.Sum(c => UtilityMethods.CalculateAge(c.DateOfBirth));
            int actualAverageAge = totalAge / clients.Count;

            // Assert
            Assert.Equal(expectedAverageAge, actualAverageAge);
        }
    }
}
