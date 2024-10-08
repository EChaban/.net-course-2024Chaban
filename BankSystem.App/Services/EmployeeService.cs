using BankSystem.Domain.Models;
using BankSystem.App.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BankSystem.App.Services
{
    public class EmployeeService
    {
        private readonly EmployeeStorage _employeeStorage;

        public EmployeeService(EmployeeStorage employeeStorage)
        {
            _employeeStorage = employeeStorage;
        }

        public void AddEmployee(Employee employee)
        {
            ValidateEmployee(employee);
            _employeeStorage.AddEmployee(employee);
        }

        public void EditEmployee(Employee oldEmployee, Employee newEmployee)
        {
            ValidateEmployee(newEmployee);
            _employeeStorage.EditEmployee(oldEmployee, newEmployee);
        }

        public IEnumerable<Employee> GetEmployees(string? fullName = null, string? phoneNumber = null, string? position = null, DateTime? dateOfBirthFrom = null, DateTime? dateOfBirthTo = null)
        {
            return _employeeStorage.GetEmployees(fullName, phoneNumber, position, dateOfBirthFrom, dateOfBirthTo);
        }

        private void ValidateEmployee(Employee employee)
        {
            if (string.IsNullOrEmpty(employee.FirstName) || string.IsNullOrEmpty(employee.LastName))
                throw new EmployeeValidationException("Employee must have a full name.");

            if (string.IsNullOrEmpty(employee.Position))
                throw new EmployeeValidationException("Employee must have a position.");

            if (employee.DateOfBirth.AddYears(18) > DateTime.Now)
                throw new EmployeeValidationException("Employee must be at least 18 years old.");
        }
    }
}
