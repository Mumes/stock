using Buisness.Update.Stocks;
using Microsoft.Extensions.Logging;
using stock.Buisness.APIRead;
using stock.Buisness.APIRead.Stocks;
using stockDataEF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
namespace Buisness.Update
{
    public class UpdateStocks
    {
        private readonly ILogger<UpdateStocks> _logger;
        private readonly IRepository<Stock> stockRepository;
        private readonly IRepository<Product> productRepository;
        private readonly IRepository<DatedPrice> priceRepository;
        private Dictionary<string,IStockRead> stocksMethods;
        public UpdateStocks(ILogger<UpdateStocks> logger, IRepository<Stock> stockRepository, 
            IRepository<Product> productRepository, IRepository<DatedPrice> priceRepository)
        {
            this.stockRepository = stockRepository;
            this.productRepository = productRepository;
            this.priceRepository = priceRepository;
            _logger = logger;
            stocksMethods = new Dictionary<string,IStockRead>();
            stocksMethods.Add("Московская биржа",new MoscowStockRead());
            stocksMethods.Add("Yobit",new YobitStockRead());

        }

        public async Task Update()
        {
            foreach (var stock in stockRepository.GetAll().ToList())
            {               
                var readStockMethod = stocksMethods[stock.Name];
              

                var model = await readStockMethod.DeserialiseProductsAsync();

                foreach (var product in model)
                {

                    var checkDuplicateProductFromDB = productRepository.GetAll().FirstOrDefault(d => d.Name == product.Product.Name);
                    if (checkDuplicateProductFromDB != null)
                    {
                        product.DatedPrice.Product = checkDuplicateProductFromDB;
                        product.Product = checkDuplicateProductFromDB;
                    }
                    else
                    {
                        product.DatedPrice.Product = product.Product;
                        product.Product.Stock = stock;
                        productRepository.Add(product.Product);
                    }

                    var checkDuplicatePriceFromDB = priceRepository.GetAll().FirstOrDefault(d => d.DateOfOperation == product.DatedPrice.DateOfOperation &&
                                                                                                 d.ProductId == product.Product.Id);
                    if (checkDuplicatePriceFromDB == null)
                    {
                        priceRepository.Add(product.DatedPrice);
                    }

                }
                stock.LastUpdated = DateTime.Now;
                stockRepository.Update(stock);
            }                                 
        }
    }
}
