using System;
using System.Collections.Generic;
using System.Linq;
using BankSystem.Domain.Models;
using BankSystem.App.Interfaces;

namespace BankSystem.Data.Storages
{
    public class EmployeeStorage : IEmployeeStorage
    {
        private List<Employee> _employees = new List<Employee>();

        public void Add(Employee employee)
        {
            _employees.Add(employee);
        }

        public void AddEmployeeList(List<Employee> employees)
        {
            _employees.AddRange(employees);
        }

        public int Count()
        {
            return _employees.Count;
        }

        public Employee? GetYoungestEmployee()
        {
            if (_employees.Count == 0) return null;
            return _employees.MaxBy(c => c.DateOfBirth);
        }

        public Employee? GetOldestEmployee()
        {
            if (_employees.Count == 0) return null;
            return _employees.MinBy(c => c.DateOfBirth);
        }

        public int GetAverageAge()
        {
            if (_employees.Count == 0) return 0;

            int totalAge = _employees.Sum(e => UtilityMethods.CalculateAge(e.DateOfBirth));

            return totalAge / _employees.Count;
        }

        public IEnumerable<Employee> GetEmployees(string? fullName = null, string? phoneNumber = null, string? position = null, DateTime? dateOfBirthFrom = null, DateTime? dateOfBirthTo = null)
        {
            return _employees.Where(e =>
                (string.IsNullOrEmpty(fullName) || $"{e.FirstName} {e.LastName}".Contains(fullName)) &&
                (string.IsNullOrEmpty(phoneNumber) || e.PhoneNumber == phoneNumber) &&
                (string.IsNullOrEmpty(position) || e.Position == position) &&
                (!dateOfBirthFrom.HasValue || e.DateOfBirth >= dateOfBirthFrom.Value) &&
                (!dateOfBirthTo.HasValue || e.DateOfBirth <= dateOfBirthTo.Value));
        }

        public List<Employee> Get(Func<Employee, bool> filter)
        {
            return _employees.Where(filter).ToList();
        }

        public void Update(Employee employee)
        {
            var index = _employees.FindIndex(e => e.PhoneNumber == employee.PhoneNumber);
            if (index != -1)
            {
                _employees[index] = employee;
            }
        }

        public void Delete(Employee employee)
        {
            _employees.Remove(employee);
        }
    }
}
