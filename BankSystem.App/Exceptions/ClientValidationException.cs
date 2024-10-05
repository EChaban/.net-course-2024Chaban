using System;

namespace BankSystem.App.Exceptions
{
    public class ClientValidationException : Exception
    {
        public ClientValidationException(string message) : base(message)
        {
        }
    }
}
