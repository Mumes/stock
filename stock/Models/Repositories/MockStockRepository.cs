using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace stock.Models
{
    public class MockStockRepository:IRepository<Stock>
    {
        private List<Stock> stocksList;
        public MockStockRepository()
        {
            stocksList = new List<Stock>()
            {
                new Stock{Id = 1, Name = "Московская биржа", ProductsAPIString = @"https://iss.moex.com/iss/engines/stock/markets/shares/boards/TQBR/securities.json"}
            };

        }
        public Stock Add(Stock stock)
        {
            stock.Id = stocksList.Max(e => e.Id) + 1;
            stocksList.Add(stock);
            return stock;
        }

        public Stock Delete(int id)
        {
            Stock stock = stocksList.FirstOrDefault(e => e.Id == id);
            if (stock != null) stocksList.Remove(stock);
            return stock;
        }

      
        public Stock Get(int id)
        {
            return stocksList.FirstOrDefault(c => c.Id == id);
        }

        public IEnumerable<Stock> GetAll()
        {
            return stocksList;
        }

        public Stock Update(Stock cnangedStock)
        {
            Stock stock = stocksList.FirstOrDefault(e => e.Id == cnangedStock.Id);
            if (stock != null)
            {
                stock.Name = cnangedStock.Name;
            }
            return stock;
        }
    }
}
