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
            Assert.Equal(1001, storage.Count()); // Проверяем, работают ли оба метода добавления клиентов в хранилище
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
            int expectedAverageAge = 40;
            for (int i = 1; i < 21; i++)
            {
                Client youngerClient = new Client("Имя", "Фамилия", new DateTime(DateTime.Now.Year - expectedAverageAge + i, 1, 1), 100+i, 100, "Телефон", "Контракт");
                storage.AddClient(youngerClient);
                Client olderClient = new Client("Имя", "Фамилия", new DateTime(DateTime.Now.Year - expectedAverageAge - i, 1, 1), 100-i, 100, "Телефон", "Контракт");
                storage.AddClient(olderClient);
            }

            // Act
            var averageAge = storage.GetAverageAge();

            // Assert
            Assert.Equal(expectedAverageAge, averageAge);
        }
    }
}