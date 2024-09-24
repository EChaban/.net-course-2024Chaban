using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankSystem.Domain.Models;

namespace BankSystem.App.Services
{
    public class BankService
    {
        public static void CalculateOwnerSalary(object owner, int profit, int expenses, int ownerCount)
        {
            if (ownerCount <= 0) throw new ArgumentException("Количество владельцев должно быть больше 0.");

            // Неявное приведение типа: проверяем, является ли owner экземпляром Employee
            // Если да, то переменная employee будет иметь тип Employee
            if (owner is Employee employee)
            {
                int salary = (profit - expenses) / ownerCount;
                employee.Salary = salary;  // Неявное приведение типа int к decimal
            }
            else throw new ArgumentException("Владелец должен быть сотрудником банка.");
        }

        public static Employee ConvertClientToEmployee(Client client, string position, decimal salary, string phoneNumber,string contract)
        {
            // Создание сотрудника, используя данные клиента и указанную должность и зарплату.
            Employee employee = new Employee(client.FirstName, client.LastName, client.DateOfBirth, position, salary, phoneNumber, contract);
            return employee;
        }
    }
}

