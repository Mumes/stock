using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace stock.Models
{
    public class SQLProductRepository : IRepository<Product>
    {
        private readonly AppDbContext context;
        private readonly ILogger<SQLProductRepository> logger;

        public SQLProductRepository(AppDbContext context, ILogger<SQLProductRepository> logger)
        {
            this.context = context;
            this.logger = logger;
        }
        public Product Add(Product product)
        {
            context.Entry(product).State = EntityState.Unchanged;
            context.Add(product);
            context.SaveChanges();
            return product;
        }

        public Product Delete(int id)
        {
            var product = context.Products.Find(id);
            if (product != null)
                context.Products.Remove(product);
            context.SaveChanges();
            return product;
        }

        public Product Get(int id)
        {
            logger.LogTrace("Product geting");
            var product = context.Products.Find(id);
            return product;
        }

        public IEnumerable<Product> GetAll()
        {
            return context.Products;
        }
        
        public Product Update(Product cnangedProduct)
        {
            var emp = context.Attach(cnangedProduct);
            emp.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return cnangedProduct;
        }
    }
}
