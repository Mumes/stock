using Buisness.Update;
using Microsoft.Extensions.Logging;
using stockDataEF.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Xunit;

namespace BuisnessTests
{
   
    public class UpdateStocksTest
    {
        [Fact]
        public async Task FullTest()
        {
            var loggerUpdateStocks = new LoggerFactory().CreateLogger<UpdateStocks>();
            var loggerStocks = new LoggerFactory().CreateLogger<SQLStockRepository>();
            var loggerProducts = new LoggerFactory().CreateLogger<SQLProductRepository>();
            var loggerDates = new LoggerFactory().CreateLogger<SQLDatedPriceRepository>();
            var dbOptions = new DbContextOptionsBuilder<AppDbContext>().UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=stockDb;Trusted_Connection=True;MultipleActiveResultSets=true").Options;
            var context = new AppDbContext(dbOptions);
            IRepository<Stock> repositoryStock = new SQLStockRepository(context,loggerStocks);
            IRepository<Product> repositoryProduct = new SQLProductRepository(context, loggerProducts);
            IRepository<DatedPrice> repositoryDate = new SQLDatedPriceRepository(context, loggerDates);
            var x = new UpdateStocks( loggerUpdateStocks,repositoryStock,repositoryProduct,repositoryDate);
            await x.Update();
        }
    }
}
