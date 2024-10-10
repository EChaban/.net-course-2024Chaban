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

        public List<Employee> Get(Func<Employee, bool> filter)
        {
            return _employees.Where(filter).ToList();
        }

        public Employee? GetYoungestEmployee()
        {
            return _employees.Count == 0 ? null : _employees.MaxBy(c => c.DateOfBirth);
        }

        public Employee? GetOldestEmployee()
        {
            return _employees.Count == 0 ? null : _employees.MinBy(c => c.DateOfBirth);
        }

        public int GetAverageAge()
        {
            if (_employees.Count == 0) return 0;

            int totalAge = _employees.Sum(e => UtilityMethods.CalculateAge(e.DateOfBirth));

            return totalAge / _employees.Count;
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
