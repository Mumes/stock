using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using stockDataEF.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace stockDataEF.Models
{
    public class SQLStockRepository:IRepository<Stock>
    {
        private readonly AppDbContext context;
        private readonly ILogger<SQLStockRepository> logger;

        public SQLStockRepository(AppDbContext context, ILogger<SQLStockRepository> logger)
        {
            this.context = context;
            this.logger = logger;
        }



        public Stock Add(Stock stock)
        {
            context.Add(stock);
            context.SaveChanges();
            return stock;
        }

        public Stock Delete(int id)
        {
            var stock = context.Stocks.Find(id);
            if (stock != null)
                context.Stocks.Remove(stock);
            context.SaveChanges();
            return stock;
        }

        public IEnumerable<Stock> GetAll()
        {
            return context.Stocks;
        }

        public Stock Get(int id)
        {
            logger.LogTrace("stock geting");
            var emp = context.Stocks.Include(s=>s.ProductsAPIStrings).Include(p=>p.Products).FirstOrDefault(s=>s.Id ==id);
            return emp;
        }

        public Stock Update(Stock changedStock)
        {
            var emp = context.Attach(changedStock);
            emp.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return changedStock;
        }
    }
}
