using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace stockDataEF.Models
{
    public class AppDbContext:DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<DatedPrice> DatedPrices { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {
            Database.EnsureCreated();
        }
    }
}
