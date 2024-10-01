using System;
using System.Linq;
using BankSystem.Data.Storages;
using BankSystem.App.Services;
using BankSystem.Domain.Models;
using Xunit;

namespace BankSystem.App.Tests
{
    public class EmployeeStorageTests
    {
        [Fact]
        public void GetYoungestEmployee_ShouldReturnYoungestEmployee()
        {
            // Arrange
            var storage = new EmployeeStorage();
            storage.AddEmployeeList(TestDataGenerator.GenerateEmployeeList());

            var expectedYoungestEmployee = new Employee("Иван", "Иванов", new DateTime(2007, 1, 1), "Должность", 500, "Телефон", "Контракт");
            storage.AddEmployee(expectedYoungestEmployee);

            // Act
            var youngestEmployee = storage.GetYoungestEmployee();

            // Assert
            Assert.Equal(1001, storage.Count()); // Проверяем, работают ли оба метода добавления клиентов в хранилище
            Assert.Equal(expectedYoungestEmployee, youngestEmployee);
        }

        [Fact]
        public void GetOldestEmployee_ShouldReturnOldestEmployee()
        {
            // Arrange
            var storage = new EmployeeStorage();
            storage.AddEmployeeList(TestDataGenerator.GenerateEmployeeList());

            var expectedOldestEmployee = new Employee("Иван", "Иванов", new DateTime(1944, 1, 1), "Должность", 500, "Телефон", "Контракт");
            storage.AddEmployee(expectedOldestEmployee);

            // Act
            var oldestEmployee = storage.GetOldestEmployee();

            // Assert
            Assert.Equal(expectedOldestEmployee, oldestEmployee);
        }

        [Fact]
        public void GetAverageAge_ShouldReturnCorrectAverageAge()
        {
            // Arrange
            var storage = new EmployeeStorage();
            int expectedAverageAge = 40;
            for (int i = 1; i < 21; i++)
            {
                Employee youngerEmployee = new Employee("Имя", "Фамилия", new DateTime(DateTime.Now.Year - expectedAverageAge + i, 1, 1), "Должность", 500, "Телефон", "Контракт");
                storage.AddEmployee(youngerEmployee);
                Employee olderEmployee = new Employee("Имя", "Фамилия", new DateTime(DateTime.Now.Year - expectedAverageAge - i, 1, 1), "Должность", 500, "Телефон", "Контракт");
                storage.AddEmployee(olderEmployee);
            }

            // Act
            var averageAge = storage.GetAverageAge();

            // Assert
            Assert.Equal(expectedAverageAge, averageAge);
        }
    }
}