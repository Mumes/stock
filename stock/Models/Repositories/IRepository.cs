using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace stock.Models
{
    public interface IRepository<T>
    {
        T Get(int id);
        IEnumerable<T> GetAll();
        T Add(T item);
        T Update(T cnangedItem);
        T Delete(int id);
    }
}
