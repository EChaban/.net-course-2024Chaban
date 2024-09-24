using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem.Domain.Models
{
    public class Client : Person
    {
        public int ClientId { get; set; }
        public decimal AccountBalance { get; set; }
        public string ContractInfo { get; set; }

        public string PhonNumber { get; set; }

        public Client(string firstName, string lastName, DateTime dateOfBirth, int clientID, decimal accountBalance, string phoneNumber, string contractInfo)
            : base(firstName, lastName, dateOfBirth)
        {
            ClientId = clientID;
            AccountBalance = accountBalance;
            PhonNumber = phoneNumber;
            ContractInfo = contractInfo;
        }

        public override string ToString()
        {
            return $"{base.ToString()}, Client ID: {ClientId}, Balance: {AccountBalance}";
        }
    }
}
