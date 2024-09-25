using BankSystem.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bogus;

namespace BankSystem.App.Services
{
    public static class TestDataGenerator
    {
        // Метод для генерации СПИСКА случайных клиентов банка
        public static List<Client> GenerateClientList()
        {
            var faker = new Faker<Client>("ru")
                .RuleFor(c => c.FirstName, f => f.Name.FirstName())
                .RuleFor(c => c.LastName, f => f.Name.LastName())
                .RuleFor(c => c.DateOfBirth, f => f.Date.Past(52, DateTime.Now.AddYears(-18)))
                .RuleFor(c => c.ClientId, f => f.IndexFaker + 1)
                .RuleFor(c => c.AccountBalance, f => f.Finance.Amount(0, 1000000))
                .RuleFor(c => c.PhoneNumber, f => f.Phone.PhoneNumber("+373########"))
                .RuleFor(c => c.ContractInfo, (f, c) => $"Контракт заключен между \"Банком\" и {c.FirstName} {c.LastName} с целью предоставления банковских услуг. Начало действия контракта {DateTime.Now:dd.MM.yyyy}.");

            return faker.Generate(1000);
        }

        // Метод для генерации СЛОВАРЯ клиентов c номером телефона в качестве ключа
        public static Dictionary<string, Client> GenerateClientDictionary()
        {
            var clients = GenerateClientList();
            return clients.ToDictionary(c => c.PhoneNumber, c => c);
        }

        // Метод для генерации СПИСКА случайных сотрудников банка
        public static List<Employee> GenerateEmployeeList()
        {
            var positions = new[]
            {
                "Менеджер по продажам", "Менеджер по работе с клиентами", "Кредитный аналитик",
                "Кассир", "Руководитель филиала", "Финансовый консультант",
                "Специалист по управлению рисками", "Бухгалтер",
                "Специалист по сопровождению клиентов", "Инвестиционный консультант"
            };

            var faker = new Faker<Employee>("ru")
                .RuleFor(e => e.FirstName, f => f.Name.FirstName())
                .RuleFor(e => e.LastName, f => f.Name.LastName())
                .RuleFor(e => e.DateOfBirth, f => f.Date.Past(52, DateTime.Now.AddYears(-18))) 
                .RuleFor(e => e.Position, f => f.PickRandom(positions)) 
                .RuleFor(e => e.Salary, f => f.Random.Int(500, 5000)) 
                .RuleFor(e => e.PhoneNumber, f => f.Phone.PhoneNumber("+373########"))
                .RuleFor(e => e.Contract, (f, e) => $"Контракт заключен между \"Банком\" и {e.FirstName} {e.LastName} на должность \"{e.Position}\" с целью выполнения обязанностей с {DateTime.Now:dd.MM.yyyy}.");

            return faker.Generate(1000);
        }

        // Метод для генерации СЛОВАРЯ случайных сотрудников банка (ключ - телефон)
        public static Dictionary<string, Employee> GenerateEmployeeDictionary()
        {
            var employees = GenerateEmployeeList();
            return employees.ToDictionary(e => e.PhoneNumber, e => e);
        }
    }
}
