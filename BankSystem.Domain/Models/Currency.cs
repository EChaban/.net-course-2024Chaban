using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BankSystem.Domain.Models
{
    public struct Currency
    {
        public string Code { get; set; } // Код валюты, например: USD, EUR, RUB
        public char Symbol { get; set; }  // Символ валюты, например: $, €, ₽

        public Currency(string code, char symbol)
        {
            Code = code;
            Symbol = symbol;
        }

        public override string ToString()
        {
            return $"{Code} - {Symbol}";
        }

        public static bool operator ==(Currency a, Currency b)
        {
            return a.Code == b.Code && a.Symbol == b.Symbol;
        }

        public static bool operator !=(Currency a, Currency b)
        {
            return !(a == b);
        }

        public override bool Equals(object obj)
        {
            if (obj is Currency currency)
            {
                return this == currency;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Code, Symbol);
        }
    }
}
