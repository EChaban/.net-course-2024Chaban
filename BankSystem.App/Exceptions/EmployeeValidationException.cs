using System;

namespace BankSystem.App.Exceptions
{
    public class EmployeeValidationException : Exception
    {
        public EmployeeValidationException(string message) : base(message)
        {
        }
    }
}