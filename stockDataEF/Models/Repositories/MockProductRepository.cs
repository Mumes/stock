using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace stockDataEF.Models
{
    public class MockProductRepository 
    {

        private List<Product> companiesList;

        public MockProductRepository()
        {
            companiesList = new List<Product>();
            {             
               //new Product{Id = 1, Name = "GazProm",Price = 100 },
               //new Product{Id = 1, Name = "MTS",Price = 100 },
               //new Product{Id = 1, Name = "Megafone",Price = 100 },
            };

        }
        public Product Add(Product product)
        {
            product.Id = companiesList.Max(e => e.Id) + 1;
            companiesList.Add(product);
            return product;
        }

        public Product Delete(int id)
        {
            Product product = companiesList.FirstOrDefault(e => e.Id == id);
            if (product != null) companiesList.Remove(product);
            return product;
        }

       

       


        public Product Get(int id)
        {
            return companiesList.FirstOrDefault(c => c.Id == id);
        }

        public IEnumerable<Product> GetAll()
        {
            return companiesList;
        }

        public Product Update(Product cnangedProduct)
        {
            Product product = companiesList.FirstOrDefault(e => e.Id == cnangedProduct.Id);
            if (product != null)
            {
                product.Name = cnangedProduct.Name;
            }
            return product;
        }
    }
}
