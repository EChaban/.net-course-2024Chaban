using System;
using BankSystem.Domain.Models;

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
            Console.WriteLine("Initial currency: " + currency);
            UpdateCurrency(ref currency, "EUR", '€'); // Изменяем значение валюты на EUR
            Console.WriteLine($"Updated Currency: {currency}");

            // Пример обновления контракта сотрудника
            Employee employee = new Employee("Eugene", "Chaban", new DateTime(1994, 07, 20), "Developer", 1000, "Work hardest");
            Console.WriteLine("Initial employee contract: " + employee.Contract);
            UpdateEmployeeContract(employee);
            Console.WriteLine($"Updated employee contract: {employee.Contract}{currency.Symbol}");
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