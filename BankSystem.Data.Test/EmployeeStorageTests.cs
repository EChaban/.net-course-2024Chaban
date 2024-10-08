using System;
using System.Linq;
using BankSystem.App.Services;
using BankSystem.Data.Storages;
using BankSystem.Domain.Models;
using Xunit;

namespace BankSystem.App.Tests
{
    public class EmployeeStorageTests
    {
        [Fact]
        public void AddEmployee_ShouldIncreaseEmployeeCount()
        {
            // Arrange
            var storage = new EmployeeStorage();
            var employee = new Employee("Иван", "Иванов", new DateTime(1990, 1, 1), "Должность", 500, "Телефон", "Контракт");

            // Act
            storage.Add(employee);

            // Assert
            Assert.Equal(1, storage.Count());
        }

        [Fact]
        public void AddEmployeeList_ShouldIncreaseEmployeeCount()
        {
            // Arrange
            var storage = new EmployeeStorage();
            var employees = TestDataGenerator.GenerateEmployeeList();

            // Act
            storage.AddEmployeeList(employees);

            // Assert
            Assert.Equal(1000, storage.Count());
        }

        [Fact]
        public void GetYoungestEmployee_ShouldReturnYoungestEmployee()
        {
            // Arrange
            var storage = new EmployeeStorage();
            storage.AddEmployeeList(TestDataGenerator.GenerateEmployeeList());

            var expectedYoungestEmployee = new Employee("Иван", "Иванов", new DateTime(2007, 1, 1), "Должность", 500, "Телефон", "Контракт");
            storage.Add(expectedYoungestEmployee);

            // Act
            var youngestEmployee = storage.GetYoungestEmployee();

            // Assert
            Assert.Equal(expectedYoungestEmployee, youngestEmployee);
        }

        [Fact]
        public void GetOldestEmployee_ShouldReturnOldestEmployee()
        {
            // Arrange
            var storage = new EmployeeStorage();
            storage.AddEmployeeList(TestDataGenerator.GenerateEmployeeList());

            var expectedOldestEmployee = new Employee("Иван", "Иванов", new DateTime(1944, 1, 1), "Должность", 500, "Телефон", "Контракт");
            storage.Add(expectedOldestEmployee);

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
            var employee1 = new Employee("Иван", "Иванов", new DateTime(1984, 1, 1), "Должность", 500, "Телефон", "Контракт"); // 40 лет
            var employee2 = new Employee("Петр", "Петров", new DateTime(1994, 1, 1), "Должность", 500, "Телефон", "Контракт"); // 30 лет
            var employee3 = new Employee("Сидор", "Сидоров", new DateTime(2004, 1, 1), "Должность", 500, "Телефон", "Контракт"); // 20 лет

            storage.Add(employee1);
            storage.Add(employee2);
            storage.Add(employee3);

            int expectedAverageAge = 30; // (40 + 30 + 20) / 3 = 30 лет

            // Act
            var actualAverageAge = storage.GetAverageAge();

            // Assert
            Assert.Equal(expectedAverageAge, actualAverageAge);
        }
    }
}
