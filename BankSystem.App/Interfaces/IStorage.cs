using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem.App.Interfaces
{
    public interface IStorage<T>
    {
        List<T> Get(Func<T, bool> filter);
        void Add(T item);
        void Update(T item);
        void Delete(T item);
    }
}

