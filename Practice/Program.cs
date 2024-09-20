using System;
using BankSystem.Domain.Models;
using BankSystem.App.Services;

namespace Practice
{
    class Program
    {
        static void Main(string[] args)
        {
            // Добавление кодировки UTF-8, для отображения символов валюты ($, €, ₽)
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            // Пример метода обновляющего сущность валюты 
            Currency currency = new Currency("USD", '$');
            UpdateCurrency(ref currency, "EUR", '€'); // Изменяем значение валюты на EUR
            Console.WriteLine($"Updated Currency: {currency}");

            // Пример обновления контракта сотрудника
            Employee employee = new Employee("Eugene", "Chaban", new DateTime(1994, 07, 20), "Developer", 1000, "Work hardest");
            UpdateEmployeeContract(employee);
            Console.WriteLine($"Updated Employee Contract: {employee.Contract}{currency.Symbol}");

            // Пример расчета зарплаты владельцев
            Employee owner = new Employee("Ivan", "Gel", new DateTime(1985, 01, 01), "CEO", 10000, "Like a boss");
            int profit = 100000; // Пример прибыли
            int expenses = 50000; // Пример расходов
            int ownersCount = 2; // Количество владельцев

            BankService.CalculateOwnerSalary(owner, profit, expenses, ownersCount);
            Console.WriteLine($"Salary of {owner.FirstName} {owner.LastName} is {owner.Salary}{currency.Symbol}");

            // Преобразование клиента банка в сотрудника
            Client client = new Client("Johnny", "Bravo", new DateTime(1997, 07, 7), 001, 100, "The oldest client");

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