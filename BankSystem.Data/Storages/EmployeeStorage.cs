using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BankSystem.Domain.Models;

namespace BankSystem.Data.Storages
{
    public class EmployeeStorage
    {
        private List<Employee> _employees = new List<Employee>();

        public void AddEmployee(Employee employee)
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

        public Employee GetYoungestEmployee()
        {
            if (_employees.Count == 0) return null;
            return _employees.OrderBy(c => c.DateOfBirth).Last();
        }

        public Employee GetOldestEmployee()
        {
            if (_employees.Count == 0) return null;
            return _employees.OrderBy(c => c.DateOfBirth).First();
        }

        public int GetAverageAge()
        {
            if (_employees.Count == 0) return 0;

            int averageAge = _employees.Sum(c => DateTime.Now.Year - c.DateOfBirth.Year);
            return averageAge / _employees.Count;
        }
    }
}
