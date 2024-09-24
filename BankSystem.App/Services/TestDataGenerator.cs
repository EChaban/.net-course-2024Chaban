using BankSystem.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem.App.Services
{
    public static class TestDataGenerator
    {
        private static Random random = new Random();

        // Метод для генерации случайных имен
        private static string GenerateRandomName()
        {
            string[] names = { "Алексей", "Андрей", "Антон", "Артём", "Борис", "Виктор", "Владимир", "Вячеслав", "Денис", "Дмитрий", "Евгений", "Иван", "Игорь", "Кирилл", "Константин", "Леонид", "Максим", "Михаил", "Николай", "Олег", "Павел", "Роман", "Сергей", "Станислав", "Юрий" };
            return names[random.Next(names.Length)];
        }

        // Метод для генерации случайных фамилий
        private static string GenerateRandomLastName()
        {
            string[] lastNames = { "Алексеев", "Андреев", "Васильев", "Волков", "Егоров", "Иванов", "Козлов", "Кузнецов", "Лебедев", "Макаров", "Михайлов", "Морозов", "Новиков", "Николаев", "Павлов", "Петров", "Попов", "Семёнов", "Сидоров", "Смирнов", "Соколов", "Степанов", "Фёдоров", "Чернов", "Зайцев" };
            return lastNames[random.Next(lastNames.Length)];
        }

        // Метод для генерации случайной даты рождения
        private static DateTime GenerateRandomDateOfBirth()
        {
            int year = random.Next(1950, 2005);
            int month = random.Next(1, 13);
            int day = random.Next(1, 29);
            return new DateTime(year, month, day);
        }

        // Метод для генерации случайного номера телефона
        private static string GeneratePhoneNumber()
        {
            return "+373" + random.Next(77700000, 77999999).ToString();
        }

        // Метод для генерации случайных клиентов банка
        public static List<Client> GenerateClientList()
        {
            var clients = new List<Client>();
            string firstName; string lastName; DateTime startDate = DateTime.Now;
            for (int i = 0; i < 1000; i++)
            {
                var client = new Client(
                    firstName = GenerateRandomName(),
                    lastName = GenerateRandomLastName(),
                    GenerateRandomDateOfBirth(),
                    i + 1, // ClientId
                    random.Next(0, 1000000), // AccountBalance
                    GeneratePhoneNumber(),
                    $"Контракт заключен между \"Банком\" и {firstName} {lastName} с целью предоставления банковских услуг. Начало действия контракта {startDate.ToString("dd.MM.yyyy")}." 
                );
                clients.Add(client);
            }
            return clients;
        }

        // Метод для генерации словаря клиентов c номером телефона в качестве ключа
        public static Dictionary<string, Client> GenerateClientDictionary()
        {
            var clients = GenerateClientList();
            var clientDictionary = new Dictionary<string, Client>();
            for (int i = 0; i < 1000; i++)
            {
                clientDictionary[GeneratePhoneNumber()] = clients[i];
            }
            return clientDictionary;
        }

        // Метод для генерации случайных сотрудников банка
        public static List<Employee> GenerateEmployeeList()
        {
            string[] positions = { "Менеджер по продажам", "Менеджер по работе с клиентами", "Кредитный аналитик", "Кассир", "Руководитель филиала", "Финансовый консультант", "Специалист по управлению рисками", "Бухгалтер", "Специалист по сопровождению клиентов", "Инвестиционный консультант" };
            string firstName; string lastName; DateTime startDate = DateTime.Now; string position;

            var employees = new List<Employee>();
            for (int i = 0; i < 1000; i++)
            {
                var employee = new Employee(
                    firstName = GenerateRandomName(),
                    lastName = GenerateRandomLastName(),
                    GenerateRandomDateOfBirth(),
                    position = positions[random.Next(positions.Length)],
                    random.Next(500, 5000), // salary
                    GeneratePhoneNumber(),
                    $"Контракт заключен между \"Банком\" и {firstName} {lastName} на должность \"{position}\" с целью выполнения обязанностей в соответствии с внутренними нормативными актами Банка. Контракт вступает в силу с {startDate.ToString("dd.MM.yyyy")}" 
                );
                employees.Add(employee);
            }
            return employees;
        }
    }
}
