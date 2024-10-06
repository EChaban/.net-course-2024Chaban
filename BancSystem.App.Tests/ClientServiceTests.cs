﻿using BankSystem.App.Exceptions;
using BankSystem.App.Services;
using BankSystem.Data.Storages;
using BankSystem.Domain.Models;
using Xunit;
using System;
using System.Linq;

public class ClientServiceTests
{
    private readonly ClientService _clientService;
    private readonly ClientStorage _clientStorage;

    public ClientServiceTests()
    {
        _clientStorage = new ClientStorage();
        _clientService = new ClientService(_clientStorage);
    }

    [Fact]
    public void AddClient_ShouldThrowException_WhenClientIsUnderage()
    {
        // Arrange
        var client = new Client { DateOfBirth = DateTime.Now.AddYears(-17) };

        // Act & Assert
        Assert.Throws<ClientValidationException>(() => _clientService.AddClient(client));
    }

    [Fact]
    public void AddClient_ShouldThrowException_WhenClientHasNoPassport()
    {
        // Arrange
        var client = new Client { DateOfBirth = DateTime.Now.AddYears(-20) };

        // Act & Assert
        Assert.Throws<ClientValidationException>(() => _clientService.AddClient(client));
    }

    [Fact]
    public void AddClient_ShouldAddClient_WhenValidClient()
    {
        // Arrange
        var client = new Client { DateOfBirth = DateTime.Now.AddYears(-20), PassportNumber = "1234567890" };

        // Act
        _clientService.AddClient(client);

        // Assert
        Assert.Equal(1, _clientStorage.Count());
    }

    [Fact]
    public void AddAdditionalAccount_ShouldThrowException_WhenClientDoesNotExist()
    {
        // Arrange
        var client = new Client { DateOfBirth = DateTime.Now.AddYears(-20), PassportNumber = "1234567890" };
        var account = new Account(new Currency("USD", '$'), 100);

        // Act & Assert
        Assert.Throws<ClientValidationException>(() => _clientService.AddAdditionalAccount(client, account));
    }

    [Fact]
    public void AddAdditionalAccount_ShouldAddAccount_WhenClientExists()
    {
        // Arrange
        var client = new Client { DateOfBirth = DateTime.Now.AddYears(-20), PassportNumber = "1234567890" };
        var account = new Account(new Currency("USD", '$'), 100);
        _clientService.AddClient(client);

        // Act
        _clientService.AddAdditionalAccount(client, account);

        // Assert
        var storedAccounts = _clientStorage.GetAccountsByClient(client);
        Assert.Contains(account, storedAccounts);
    }

    [Fact]
    public void EditAccount_ShouldThrowException_WhenClientDoesNotExist()
    {
        // Arrange
        var client = new Client { DateOfBirth = DateTime.Now.AddYears(-20), PassportNumber = "1234567890" };
        var oldAccount = new Account(new Currency("USD", '$'), 100);
        var newAccount = new Account(new Currency("USD", '$'), 200);

        // Act & Assert
        Assert.Throws<ClientValidationException>(() => _clientService.EditAccount(client, oldAccount, newAccount));
    }

    [Fact]
    public void EditAccount_ShouldEditAccount_WhenClientExists()
    {
        // Arrange
        var client = new Client { DateOfBirth = DateTime.Now.AddYears(-20), PassportNumber = "1234567890" };
        var oldAccount = new Account(new Currency("USD", '$'), 100);
        var newAccount = new Account(new Currency("USD", '$'), 200);
        _clientService.AddClient(client);
        _clientService.AddAdditionalAccount(client, oldAccount);

        // Act
        _clientService.EditAccount(client, oldAccount, newAccount);

        // Assert
        var storedAccounts = _clientStorage.GetAccountsByClient(client);
        Assert.Contains(newAccount, storedAccounts);
        Assert.DoesNotContain(oldAccount, storedAccounts);
    }

    [Fact]
    public void GetClients_ShouldReturnClients_WhenClientsExist()
    {
        // Arrange
        var client1 = new Client { DateOfBirth = DateTime.Now.AddYears(-20), PassportNumber = "1234567890" };
        var client2 = new Client { DateOfBirth = DateTime.Now.AddYears(-25), PassportNumber = "0987654321" };
        _clientService.AddClient(client1);
        _clientService.AddClient(client2);

        // Act
        var clients = _clientService.GetClients();

        // Assert
        Assert.Contains(client1, clients);
        Assert.Contains(client2, clients);
    }

    [Fact]
    public void GetClients_ShouldReturnClients_WhenFilteredByFullName()
    {
        // Arrange
        var client1 = new Client { FirstName = "Иван", LastName = "Иванов", DateOfBirth = DateTime.Now.AddYears(-20), PassportNumber = "1234567890" };
        var client2 = new Client { FirstName = "Мария", LastName = "Петрова", DateOfBirth = DateTime.Now.AddYears(-25), PassportNumber = "0987654321" };
        _clientService.AddClient(client1);
        _clientService.AddClient(client2);

        // Act
        var clients = _clientService.GetClients(fullName: "Иван Иванов");

        // Assert
        Assert.Contains(client1, clients);
        Assert.DoesNotContain(client2, clients);
    }

    [Fact]
    public void GetClients_ShouldReturnClients_WhenFilteredByPhoneNumber()
    {
        // Arrange
        var client1 = new Client { FirstName = "Иван", LastName = "Иванов", PhoneNumber = "123-456-7890", DateOfBirth = DateTime.Now.AddYears(-20), PassportNumber = "1234567890" };
        var client2 = new Client { FirstName = "Мария", LastName = "Петрова", PhoneNumber = "098-765-4321", DateOfBirth = DateTime.Now.AddYears(-25), PassportNumber = "0987654321" };
        _clientService.AddClient(client1);
        _clientService.AddClient(client2);

        // Act
        var clients = _clientService.GetClients(phoneNumber: "123-456-7890");

        // Assert
        Assert.Contains(client1, clients);
        Assert.DoesNotContain(client2, clients);
    }

    [Fact]
    public void GetClients_ShouldReturnClients_WhenFilteredByPassportNumber()
    {
        // Arrange
        var client1 = new Client { FirstName = "Иван", LastName = "Иванов", DateOfBirth = DateTime.Now.AddYears(-20), PassportNumber = "1234567890" };
        var client2 = new Client { FirstName = "Мария", LastName = "Петрова", DateOfBirth = DateTime.Now.AddYears(-25), PassportNumber = "0987654321" };
        _clientService.AddClient(client1);
        _clientService.AddClient(client2);

        // Act
        var clients = _clientService.GetClients(passportNumber: "1234567890");

        // Assert
        Assert.Contains(client1, clients);
        Assert.DoesNotContain(client2, clients);
    }

    [Fact]
    public void GetClients_ShouldReturnClients_WhenFilteredByDateOfBirthRange()
    {
        // Arrange
        var client1 = new Client { FirstName = "Иван", LastName = "Иванов", DateOfBirth = DateTime.Now.AddYears(-20), PassportNumber = "1234567890" };
        var client2 = new Client { FirstName = "Мария", LastName = "Петрова", DateOfBirth = DateTime.Now.AddYears(-25), PassportNumber = "0987654321" };
        _clientService.AddClient(client1);
        _clientService.AddClient(client2);

        // Act
        var clients = _clientService.GetClients(dateOfBirthFrom: DateTime.Now.AddYears(-22), dateOfBirthTo: DateTime.Now.AddYears(-18));

        // Assert
        Assert.Contains(client1, clients);
        Assert.DoesNotContain(client2, clients);
    }


}
