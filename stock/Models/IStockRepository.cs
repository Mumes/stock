using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace stock.Models
{
    public interface IStockRepository
    {
        Stock Get(int id);
        IEnumerable<Stock> GetAll();
        Stock Add(Stock stock);
        Stock Update(Stock cnangedStock);
        Stock Delete(int id);

    }
}
