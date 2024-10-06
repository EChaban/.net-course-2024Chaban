using BankSystem.App.Exceptions;
using BankSystem.App.Services;
using BankSystem.Data.Storages;
using BankSystem.Domain.Models;
using Xunit;
using System;
using System.Linq;

public class EmployeeServiceTests
{
    private readonly EmployeeService _employeeService;
    private readonly EmployeeStorage _employeeStorage;

    public EmployeeServiceTests()
    {
        _employeeStorage = new EmployeeStorage();
        _employeeService = new EmployeeService(_employeeStorage);
    }

    [Fact]
    public void AddEmployee_ShouldThrowException_WhenEmployeeIsUnderage()
    {
        // Arrange
        var employee = new Employee { DateOfBirth = DateTime.Now.AddYears(-17) };

        // Act & Assert
        Assert.Throws<EmployeeValidationException>(() => _employeeService.AddEmployee(employee));
    }

    [Fact]
    public void AddEmployee_ShouldThrowException_WhenEmployeeHasNoFullName()
    {
        // Arrange
        var employee = new Employee { DateOfBirth = DateTime.Now.AddYears(-20), FirstName = "", LastName = "" };

        // Act & Assert
        Assert.Throws<EmployeeValidationException>(() => _employeeService.AddEmployee(employee));
    }

    [Fact]
    public void AddEmployee_ShouldThrowException_WhenEmployeeHasNoPosition()
    {
        // Arrange
        var employee = new Employee { DateOfBirth = DateTime.Now.AddYears(-20), FirstName = "John", LastName = "Doe", Position = "" };

        // Act & Assert
        Assert.Throws<EmployeeValidationException>(() => _employeeService.AddEmployee(employee));
    }

    [Fact]
    public void AddEmployee_ShouldAddEmployee_WhenValidEmployee()
    {
        // Arrange
        var employee = new Employee { DateOfBirth = DateTime.Now.AddYears(-20), FirstName = "John", LastName = "Doe", Position = "Manager" };

        // Act
        _employeeService.AddEmployee(employee);

        // Assert
        Assert.Equal(1, _employeeStorage.Count());
    }

    [Fact]
    public void EditEmployee_ShouldThrowException_WhenNewEmployeeIsInvalid()
    {
        // Arrange
        var oldEmployee = new Employee { DateOfBirth = DateTime.Now.AddYears(-20), FirstName = "John", LastName = "Doe", Position = "Manager" };
        var newEmployee = new Employee { DateOfBirth = DateTime.Now.AddYears(-17), FirstName = "Jane", LastName = "Doe", Position = "Manager" };
        _employeeService.AddEmployee(oldEmployee);

        // Act & Assert
        Assert.Throws<EmployeeValidationException>(() => _employeeService.EditEmployee(oldEmployee, newEmployee));
    }

    [Fact]
    public void EditEmployee_ShouldEditEmployee_WhenNewEmployeeIsValid()
    {
        // Arrange
        var oldEmployee = new Employee { DateOfBirth = DateTime.Now.AddYears(-20), FirstName = "John", LastName = "Doe", Position = "Manager" };
        var newEmployee = new Employee { DateOfBirth = DateTime.Now.AddYears(-25), FirstName = "Jane", LastName = "Doe", Position = "Director" };
        _employeeService.AddEmployee(oldEmployee);

        // Act
        _employeeService.EditEmployee(oldEmployee, newEmployee);

        // Assert
        var employees = _employeeStorage.GetEmployees();
        Assert.Contains(newEmployee, employees);
        Assert.DoesNotContain(oldEmployee, employees);
    }

    [Fact]
    public void GetEmployees_ShouldReturnEmployees_WhenEmployeesExist()
    {
        // Arrange
        var employee1 = new Employee { DateOfBirth = DateTime.Now.AddYears(-20), FirstName = "John", LastName = "Doe", Position = "Manager" };
        var employee2 = new Employee { DateOfBirth = DateTime.Now.AddYears(-25), FirstName = "Jane", LastName = "Doe", Position = "Director" };
        _employeeService.AddEmployee(employee1);
        _employeeService.AddEmployee(employee2);

        // Act
        var employees = _employeeService.GetEmployees();

        // Assert
        Assert.Contains(employee1, employees);
        Assert.Contains(employee2, employees);
    }

    [Fact]
    public void GetEmployees_ShouldReturnEmployees_WhenFilteredByFullName()
    {
        // Arrange
        var employee1 = new Employee { FirstName = "Иван", LastName = "Иванов", DateOfBirth = DateTime.Now.AddYears(-20), Position = "Manager" };
        var employee2 = new Employee { FirstName = "Мария", LastName = "Петрова", DateOfBirth = DateTime.Now.AddYears(-25), Position = "Director" };
        _employeeService.AddEmployee(employee1);
        _employeeService.AddEmployee(employee2);

        // Act
        var employees = _employeeService.GetEmployees(fullName: "Иван Иванов");

        // Assert
        Assert.Contains(employee1, employees);
        Assert.DoesNotContain(employee2, employees);
    }

    [Fact]
    public void GetEmployees_ShouldReturnEmployees_WhenFilteredByPhoneNumber()
    {
        // Arrange
        var employee1 = new Employee { FirstName = "Иван", LastName = "Иванов", PhoneNumber = "123-456-7890", DateOfBirth = DateTime.Now.AddYears(-20), Position = "Manager" };
        var employee2 = new Employee { FirstName = "Мария", LastName = "Петрова", PhoneNumber = "098-765-4321", DateOfBirth = DateTime.Now.AddYears(-25), Position = "Director" };
        _employeeService.AddEmployee(employee1);
        _employeeService.AddEmployee(employee2);

        // Act
        var employees = _employeeService.GetEmployees(phoneNumber: "123-456-7890");

        // Assert
        Assert.Contains(employee1, employees);
        Assert.DoesNotContain(employee2, employees);
    }

    [Fact]
    public void GetEmployees_ShouldReturnEmployees_WhenFilteredByPosition()
    {
        // Arrange
        var employee1 = new Employee { FirstName = "Иван", LastName = "Иванов", DateOfBirth = DateTime.Now.AddYears(-20), Position = "Manager" };
        var employee2 = new Employee { FirstName = "Мария", LastName = "Петрова", DateOfBirth = DateTime.Now.AddYears(-25), Position = "Director" };
        _employeeService.AddEmployee(employee1);
        _employeeService.AddEmployee(employee2);

        // Act
        var employees = _employeeService.GetEmployees(position: "Manager");

        // Assert
        Assert.Contains(employee1, employees);
        Assert.DoesNotContain(employee2, employees);
    }

    [Fact]
    public void GetEmployees_ShouldReturnEmployees_WhenFilteredByDateOfBirthRange()
    {
        // Arrange
        var employee1 = new Employee { FirstName = "Иван", LastName = "Иванов", DateOfBirth = DateTime.Now.AddYears(-20), Position = "Manager" };
        var employee2 = new Employee { FirstName = "Мария", LastName = "Петрова", DateOfBirth = DateTime.Now.AddYears(-25), Position = "Director" };
        _employeeService.AddEmployee(employee1);
        _employeeService.AddEmployee(employee2);

        // Act
        var employees = _employeeService.GetEmployees(dateOfBirthFrom: DateTime.Now.AddYears(-22), dateOfBirthTo: DateTime.Now.AddYears(-18));

        // Assert
        Assert.Contains(employee1, employees);
        Assert.DoesNotContain(employee2, employees);
    }
}

