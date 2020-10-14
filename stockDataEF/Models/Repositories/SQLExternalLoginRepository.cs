using Microsoft.Extensions.Logging;
using stockDataEF.Models.Tables;
using System;
using System.Collections.Generic;
using System.Text;

namespace stockDataEF.Models.Repositories
{
    public class SQLExternalLoginRepository : IRepository<ExternalLogin>
    {
        private readonly AppDbContext context;
        private readonly ILogger<SQLExternalLoginRepository> logger;

        public SQLExternalLoginRepository(AppDbContext context, ILogger<SQLExternalLoginRepository> logger)
        {
            this.context = context;
            this.logger = logger;
        }
        public ExternalLogin Add(ExternalLogin item)
        {
            context.Add(item);
            context.SaveChanges();
            return item;
        }

        public ExternalLogin Delete(int id)
        {
            var item = context.ExternalLogins.Find(id);
            if (item != null)
                context.ExternalLogins.Remove(item);
            context.SaveChanges();
            return item;
        }

        public ExternalLogin Get(int id)
        {
            var item = context.ExternalLogins.Find(id);
            return item;
        }

        public IEnumerable<ExternalLogin> GetAll()
        {
            return context.ExternalLogins;
        }

        public ExternalLogin Update(ExternalLogin cnangedItem)
        {
            var emp = context.Attach(cnangedItem);
            emp.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return cnangedItem;
        }
    }
}
