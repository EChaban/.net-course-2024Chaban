using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem.Domain.Models
{
    public class Account
    {
        public Currency Currency { get; set; }
        public decimal Amount { get; set; }

        public Account() { }

        public Account(Currency currency, decimal amount)
        {
            Currency = currency;
            Amount = amount;
        }

        public override bool Equals(object obj)
        {
            if (obj is Account account)
            {
                return Currency == account.Currency && Amount == account.Amount;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Currency, Amount);
        }
    }
}
