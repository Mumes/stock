using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace stock.Models
{
   public interface IProductRepository
    {
        Product Get(int id);

        Task<Product> GetAsync(int id);
        IEnumerable<Product> GetAll();
        Task<IEnumerable<Product>> GetAllAsync(Stock stock);
        Product Add(Product product);
        Product Update(Product cnangedProduct);
        Product Delete(int id);

    }
}
