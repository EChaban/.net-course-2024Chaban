using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BankSystem.Domain.Models
{
    public class Employee : Person
    {
        public string Position { get; set; }
        public decimal Salary { get; set; }
        public string Contract { get; set; }

        public string PhoneNumber { get; set; }

        public Employee() 
        { 
            Position = string.Empty;
            Contract = string.Empty;
            PhoneNumber = string.Empty;
        }
        public Employee(string firstName, string lastName, DateTime dateOfBirth, string position, decimal salary, string phoneNumber, string contract)
            : base(firstName, lastName, dateOfBirth)
        {
            Position = position;
            Salary = salary;
            PhoneNumber = phoneNumber;
            Contract = contract;
        }

        public override string ToString()
        {
            return $"{base.ToString()}, Position: {Position}, Salary: {Salary}, Contract: {Contract}";
        }
    }
}
