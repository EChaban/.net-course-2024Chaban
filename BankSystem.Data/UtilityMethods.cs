using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem.Data
{
    public static class UtilityMethods
    {
        public static int CalculateAge(this DateTime dateOfBirth)
        {
            int age = DateTime.Now.Year - dateOfBirth.Year;

            if (DateTime.Now.Month < dateOfBirth.Month || (DateTime.Now.Month == dateOfBirth.Month && DateTime.Now.Day < dateOfBirth.Day))
            {
                age--;
            }

            return age;
        }
    }
}
