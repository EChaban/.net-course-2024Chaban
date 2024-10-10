using BankSystem.Domain.Models;
using BankSystem.App.Exceptions;
using BankSystem.App.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BankSystem.App.Services
{
    public class EmployeeService
    {
        private readonly IEmployeeStorage _employeeStorage;

        public EmployeeService(IEmployeeStorage employeeStorage)
        {
            _employeeStorage = employeeStorage;
        }

        public void AddEmployee(Employee employee)
        {
            ValidateEmployee(employee);
            _employeeStorage.Add(employee);
        }

        public void EditEmployee(Employee employee)
        {
            ValidateEmployee(employee);
            _employeeStorage.Update(employee);
        }

        public IEnumerable<Employee> GetEmployees(string? fullName = null, string? phoneNumber = null, string? position = null, DateTime? dateOfBirthFrom = null, DateTime? dateOfBirthTo = null)
        {
            return _employeeStorage.Get(e =>
                (string.IsNullOrEmpty(fullName) || $"{e.FirstName} {e.LastName}".Contains(fullName)) &&
                (string.IsNullOrEmpty(phoneNumber) || e.PhoneNumber == phoneNumber) &&
                (string.IsNullOrEmpty(position) || e.Position == position) &&
                (!dateOfBirthFrom.HasValue || e.DateOfBirth >= dateOfBirthFrom.Value) &&
                (!dateOfBirthTo.HasValue || e.DateOfBirth <= dateOfBirthTo.Value)
            );
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
