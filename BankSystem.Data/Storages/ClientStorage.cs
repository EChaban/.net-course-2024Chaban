using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BankSystem.Domain.Models;

namespace BankSystem.Data.Storages
{
    public class ClientStorage
    {
        private List<Client> _clients = new List<Client>();

        public void AddClient(Client client)
        {
            _clients.Add(client);
        }

        public void AddClientList(List<Client> clients)
        {
            _clients.AddRange(clients);
        }

        public int Count()
        {
            return _clients.Count;
        }

        public Client GetYoungestClient()
        {
            if (_clients.Count == 0) return null;
            return _clients.MaxBy(c => c.DateOfBirth);
        }

        public Client GetOldestClient()
        {
            if (_clients.Count == 0) return null;
            return _clients.MinBy(c => c.DateOfBirth);
        }

        public int GetAverageAge()
        {
            if (_clients.Count == 0) return 0;

            int totalAge = _clients.Sum(c =>
            {
                int age = DateTime.Now.Year - c.DateOfBirth.Year;

                if (DateTime.Now.Month < c.DateOfBirth.Month || (DateTime.Now.Month == c.DateOfBirth.Month && DateTime.Now.Day < c.DateOfBirth.Day))
                {
                    age--;
                }

                return age;
            });

            return totalAge / _clients.Count;
        }
    }
}
