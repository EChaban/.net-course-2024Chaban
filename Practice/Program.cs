using System;
using BankSystem.Domain.Models;
using BankSystem.App.Services;
using System.Diagnostics;

namespace Practice
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Client> clientList = TestDataGenerator.GenerateClientList();
            Dictionary<string, Client> clientDictionary = TestDataGenerator.GenerateClientDictionary();
            List<Employee> employeeList = TestDataGenerator.GenerateEmployeeList();

            // Создаём тестого клиента которого будем использовать для поиска по номеру телефона
            Client testClient = new Client("Иван", "Иванов", new DateTime(1990, 1, 1), 199011, 100, "+37377999999", "Контракт");

            // Добавляем тестого клиента в коллекцию и словарь
            clientList.Add(testClient);
            clientDictionary.Add("+37377999999", testClient);

            // Замер времени поиска клиента по номеру телефона
            var stopwatch = new Stopwatch();
            string searchPhoneNumber = "+37377999999";

            // Замер времени поиска в СПИСКЕ
            stopwatch.Start();
            var clientFromList = clientList.Find(c => c.PhonNumber.Contains(searchPhoneNumber));
            stopwatch.Stop();
            Console.WriteLine($"Клиент {clientFromList.FirstName} {clientFromList.LastName} найден в СПИСКЕ, поиск занял: {stopwatch.Elapsed}");

            // Замер времени поиска в СЛОВАРЕ
            stopwatch.Restart();
            clientDictionary.TryGetValue(searchPhoneNumber, out var clientFromDictionary);
            stopwatch.Stop();
            Console.WriteLine($"Клиент {clientFromDictionary.FirstName} {clientFromDictionary.LastName} найден в СЛОВАРЕ, поиск занял: {stopwatch.Elapsed}");

            // Выборка клиентов из СПИСКА, возраст которых меньше 30 лет 
            int ageLimit = 30;
            DateTime now = DateTime.Now;
            stopwatch.Restart();
            var youngClients = clientList.FindAll(c => (now.Year - c.DateOfBirth.Year) < ageLimit);
            stopwatch.Stop();
            Console.WriteLine($"\nКоличество клиентов моложе {ageLimit} лет: {youngClients.Count}");
            Console.WriteLine($"Выборка клиентов моложе 30 в СПИСКЕ заняло: {stopwatch.Elapsed}");

            // Выборка клиентов из СЛОВАРЯ, возраст которых меньше 30 лет
            stopwatch.Restart();
            var youngClientsFromDictionary = clientDictionary.Values
                .Where(c => (now.Year - c.DateOfBirth.Year) < ageLimit)
                .ToList();
            stopwatch.Stop();
            Console.WriteLine($"Количество клиентов моложе {ageLimit} лет: {youngClientsFromDictionary.Count}");
            Console.WriteLine($"Выборка клиентов моложе 30 в СЛОВАРЕ заняло: {stopwatch.Elapsed}");

            // Поиск сотрудника с минимальной заработной платой
            var employeeWithMinSalary = employeeList.MinBy(e => e.Salary);
            stopwatch.Restart();
            Console.WriteLine($"\nСотрудник с минимальной зарплатой: {employeeWithMinSalary.FirstName} {employeeWithMinSalary.LastName}, Зарплата: {employeeWithMinSalary.Salary}");
            stopwatch.Stop();
            Console.WriteLine($"Поиск сотрудника с минимальной заработной платой занял: {stopwatch.Elapsed}");
        }

        // Метод, обновляющий контракт сотрудника
        static void UpdateEmployeeContract(Employee employee) // Получаем в метод копию ссылки на объект класса
        {
            employee.Contract = $"Employee {employee.FirstName} {employee.LastName} " +
                                $"has been assigned as {employee.Position} with a salary of {employee.Salary}";
        }

        // Метод, обновляющий значение валюты
        static void UpdateCurrency(ref Currency currency, string newCode, char newSymbol) // Получаем в метод ссылки на объект структуры с помощью ref
        {
            currency = new Currency(newCode, newSymbol);
        }
    }
}