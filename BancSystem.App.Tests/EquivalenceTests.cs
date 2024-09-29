using BankSystem.App.Services;
using BankSystem.Domain.Models;
using Xunit;

namespace BankSystem.App.Tests
{
    public class EquivalenceTests
    {
        [Fact]
        public void GetHashCodeNecessityPositivTest()
        {
            // Arrange
            var clientAccounts = TestDataGenerator.GenerateClientAccounts();
            var client = clientAccounts.Keys.First();
            var account = clientAccounts[client];

            // Act
            var accountHashCode = account.GetHashCode();

            // Assert
            Assert.NotNull(account);
            Assert.IsType<Account>(account);

            // Проверяем, что хэш-коды аккаунта валидные (не равны 0)
            Assert.True(accountHashCode != 0, "HashCode аккаунта не должен быть равен 0.");
        }

        [Fact]
        public void GenerateClientWithSeveralAccounts_HashCodesAreValidAndAccountsNotEmpty()
        {
            // Arrange
            var clientAccounts = TestDataGenerator.GenerateClientWithSeveralAccounts();
            var client = clientAccounts.Keys.First();
            var accounts = clientAccounts[client];

            // Act
            var accountHashCodes = accounts.Select(acc => acc.GetHashCode()).ToList();

            // Assert
            Assert.NotNull(accounts);
            Assert.NotEmpty(accounts);

            // Проверяем, что у каждого аккаунта валидный хэш-код
            foreach (var hashCode in accountHashCodes)
            {
                Assert.True(hashCode != 0, "HashCode аккаунта не должен быть равен 0.");
            }

            // Проверяем, что хэш-коды уникальны для каждого счета
            Assert.Equal(accountHashCodes.Count, accountHashCodes.Distinct().Count());
        }

        [Fact]
        public void GetHashCodeNecessityPositivTestList()
        {
            // Arrange
            var employeeList = TestDataGenerator.GenerateEmployeeList();
            var employee = employeeList.First();

            // Act
            var hashCode = employee.GetHashCode();

            // Assert
            Assert.NotNull(employee);
            Assert.IsType<Employee>(employee);
            Assert.True(hashCode != 0);
        }
    }
}
