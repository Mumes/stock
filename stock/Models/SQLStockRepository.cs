using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace stock.Models
{
    public class SQLStockRepository:IStockRepository
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
            var emp = context.Stocks.Find(id);
            if (emp != null)
                context.Stocks.Remove(emp);
            context.SaveChanges();

            return emp;
        }

        public IEnumerable<Stock> GetAll()
        {
            return context.Stocks;
        }

        public Stock Get(int id)
        {
            logger.LogTrace("Employee geting");
            var emp = context.Stocks.Find(id);
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
