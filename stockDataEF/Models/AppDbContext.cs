using Autofac.Extras.Moq;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using stock.Models;
using stockDataEF.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace stockDataEF.Models
{
    public class AppDbContext:IdentityDbContext<ApplicationUser>
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<DatedPrice> DatedPrices { get; set; }
        public DbSet<ExternalLogin> ExternalLogins { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {
            Database.EnsureCreated();
        }      
    }
}
