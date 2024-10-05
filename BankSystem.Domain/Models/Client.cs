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
        public string ContractInfo { get; set; }
        public string PhoneNumber { get; set; }
        public string PassportNumber { get; set; }

        public Client()
        {
            ContractInfo = string.Empty;
            PhoneNumber = string.Empty;
            PassportNumber = string.Empty;
        }

        public Client(string firstName, string lastName, DateTime dateOfBirth, int clientID, string phoneNumber, string contractInfo, string passportNumber)
            : base(firstName, lastName, dateOfBirth)
        {
            ClientId = clientID;
            PhoneNumber = phoneNumber;
            ContractInfo = contractInfo;
            PassportNumber = passportNumber;
        }
    }
}
