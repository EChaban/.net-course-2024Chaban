using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankSystem.Domain.Models;

namespace BankSystem.App.Interfaces
{
    public interface IEmployeeStorage : IStorage<Employee>
    {
        void AddEmployeeList(List<Employee> employees);
        Employee? GetYoungestEmployee();
        Employee? GetOldestEmployee();
        int GetAverageAge();
        IEnumerable<Employee> GetEmployees(string? fullName = null, string? phoneNumber = null, string? position = null, DateTime? dateOfBirthFrom = null, DateTime? dateOfBirthTo = null);
    }
}

