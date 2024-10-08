using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankSystem.Domain.Models;

namespace BankSystem.App.Services
{
    public class BankService
    {
        private static List<Person> _blackList = new List<Person>();

        public static void CalculateOwnerSalary(object owner, int profit, int expenses, int ownerCount)
        {
            if (ownerCount <= 0) throw new ArgumentException("Количество владельцев должно быть больше 0.");

            if (owner is Employee employee)
            {
                int salary = (profit - expenses) / ownerCount;
                employee.Salary = salary;
            }
            else throw new ArgumentException("Владелец должен быть сотрудником банка.");
        }

        public static Employee ConvertClientToEmployee(Client client, string position, decimal salary, string contract)
        {
            Employee employee = new Employee(client.FirstName, client.LastName, client.DateOfBirth, position, salary, client.PhoneNumber, contract);
            return employee;
        }

        public static void AddBonus(Person person, decimal bonus)
        {
            if (person is Employee employee)
            {
                employee.Salary += bonus;
            }
            else
            {
                throw new ArgumentException("Бонус может быть добавлен только сотруднику или клиенту.");
            }
        }

        public static void AddToBlackList<T>(T person) where T : Person
        {
            if (!_blackList.Contains(person))
            {
                _blackList.Add(person);
            }
        }

        public static bool IsPersonInBlackList<T>(T person) where T : Person
        {
            return _blackList.Contains(person);
        }
    }
}
