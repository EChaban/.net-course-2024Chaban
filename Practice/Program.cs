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
            // Второе дз
            Console.WriteLine("Второе дз:");

            List<Client> clientList = TestDataGenerator.GenerateClientList();
            Dictionary<string, Client> clientDictionary = TestDataGenerator.GenerateClientDictionary();
            List<Employee> employeeList = TestDataGenerator.GenerateEmployeeList();
            Dictionary<string, Employee> employeeDictionary = TestDataGenerator.GenerateEmployeeDictionary();


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
            var clientFromList = clientList.Find(c => c.PhoneNumber.Contains(searchPhoneNumber));
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

            // Поиск сотрудника с минимальной заработной платой в СПИСКЕ
            stopwatch.Restart();
            var employeeWithMinSalary = employeeList.MinBy(e => e.Salary);
            stopwatch.Stop();
            Console.WriteLine($"\nСотрудник с минимальной зарплатой: {employeeWithMinSalary.FirstName} {employeeWithMinSalary.LastName}, Зарплата: {employeeWithMinSalary.Salary}");
            Console.WriteLine($"Поиск сотрудника в СПИСКЕ с минимальной заработной платой занял: {stopwatch.Elapsed}");

            // Поиск сотрудника с минимальной заработной платой в СЛОВАРЕ
            stopwatch.Restart();
            var employeeWithMinSalaryDictionary = employeeDictionary.Values.MinBy(e => e.Salary);
            stopwatch.Stop();
            Console.WriteLine($"\nСотрудник с минимальной зарплатой: {employeeWithMinSalaryDictionary.FirstName} {employeeWithMinSalaryDictionary.LastName}, Зарплата: {employeeWithMinSalaryDictionary.Salary}");
            Console.WriteLine($"Поиск сотрудника в СЛОВАРЕ с минимальной заработной платой занял: {stopwatch.Elapsed}");




            // Первое дз
            Console.WriteLine("\n\nПервое дз:");

            // Добавление кодировки UTF-8, для отображения символов валюты ($, €, ₽)
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            // Пример метода обновляющего сущность валюты 
            Currency currency = new Currency("USD", '$');
            Console.WriteLine("Initial currency: " + currency);
            UpdateCurrency(ref currency, "EUR", '€'); // Изменяем значение валюты на EUR
            Console.WriteLine($"Updated Currency: {currency}");

            // Пример обновления контракта сотрудника
            Employee employee = new Employee("Eugene", "Chaban", new DateTime(1994, 07, 20), "Developer", 1000, "+37377711111", "Work hardest");
            Console.WriteLine($"Updated employee contract: {employee.Contract}{currency.Symbol}");
            UpdateEmployeeContract(employee);
            Console.WriteLine($"Updated Employee Contract: {employee.Contract}{currency.Symbol}");

            // Пример расчета зарплаты владельцев
            Employee owner = new Employee("Ivan", "Gel", new DateTime(1985, 01, 01), "CEO", 10000, "+37377777777", "Like a boss");
            int profit = 100000; // Пример прибыли
            int expenses = 50000; // Пример расходов
            int ownersCount = 2; // Количество владельцев

            BankService.CalculateOwnerSalary(owner, profit, expenses, ownersCount);
            Console.WriteLine($"Salary of {owner.FirstName} {owner.LastName} is {owner.Salary}{currency.Symbol}");

            // Преобразование клиента банка в сотрудника
            Client client = new Client("Johnny", "Bravo", new DateTime(1997, 07, 7), 001, 100, "+37377712345", "The oldest client");

            var newEmployee = BankService.ConvertClientToEmployee(client, "CFO", 10000, "The new CFO");
            Console.WriteLine(newEmployee);
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