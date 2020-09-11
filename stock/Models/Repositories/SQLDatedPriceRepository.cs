using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace stock.Models
{
    public class SQLDatedPriceRepository : IRepository<DatedPrice>
    {

        private readonly AppDbContext context;
        private readonly ILogger<SQLDatedPriceRepository> logger;

        public SQLDatedPriceRepository(AppDbContext context, ILogger<SQLDatedPriceRepository> logger)
        {
            this.context = context;
            this.logger = logger;
        }
        public DatedPrice Add(DatedPrice item)
        {
            context.Add(item);
            context.SaveChanges();
            return item;
        }

        public DatedPrice Delete(int id)
        {
            var item = context.DatedPrices.Find(id);
            if (item != null)
                context.DatedPrices.Remove(item);
            context.SaveChanges();
            return item ;
        }

        public DatedPrice Get(int id)
        {
            var item = context.DatedPrices.Find(id);
            return item;
        }

        public IEnumerable<DatedPrice> GetAll()
        {
            return context.DatedPrices;
        }

        public DatedPrice Update(DatedPrice cnangedItem)
        {
            var emp = context.Attach(cnangedItem);
            emp.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return cnangedItem;
        }
    }
}
