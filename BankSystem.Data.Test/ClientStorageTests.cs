using System;
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

            // Act
            storage.AddClient(client);

            // Assert
            Assert.Equal(1, storage.Count());
        }

        [Fact]
        public void AddClientList_ShouldIncreaseClientCount()
        {
            // Arrange
            var storage = new ClientStorage();
            var clients = TestDataGenerator.GenerateClientList();

            // Act
            storage.AddClientList(clients);

            // Assert
            Assert.Equal(1000, storage.Count());
        }

        [Fact]
        public void GetYoungestClient_ShouldReturnYoungestClient()
        {
            // Arrange
            var storage = new ClientStorage();
            storage.AddClientList(TestDataGenerator.GenerateClientList());

            var expectedYoungestClient = new Client("Иван", "Иванов", new DateTime(2007, 1, 1), 001, 100, "Телефон", "Контракт");
            storage.AddClient(expectedYoungestClient);

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
            storage.AddClientList(TestDataGenerator.GenerateClientList());

            var expectedOldestClient = new Client("Иван", "Иванов", new DateTime(1944, 1, 1), 001, 100, "Телефон", "Контракт");
            storage.AddClient(expectedOldestClient);

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
            var client1 = new Client("Иван", "Иванов", new DateTime(1984, 1, 1), 001, 100, "Телефон", "Контракт"); // 40 года
            var client2 = new Client("Петр", "Петров", new DateTime(1994, 1, 1), 002, 100, "Телефон", "Контракт"); // 30 года
            var client3 = new Client("Сидор", "Сидоров", new DateTime(2004, 1, 1), 003, 100, "Телефон", "Контракт"); // 20 года

            storage.AddClient(client1);
            storage.AddClient(client2);
            storage.AddClient(client3);

            int expectedAverageAge = 30; // (40 + 30 + 20) / 3 = 30 года

            // Act
            var actualAverageAge = storage.GetAverageAge();

            // Assert
            Assert.Equal(expectedAverageAge, actualAverageAge);
        }
    }
}