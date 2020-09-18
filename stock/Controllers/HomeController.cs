using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using stock.Buisness.APIRead.Stocks;
using stock.Models;
using stock.ViewModels;
using stockDataEF.Models;
using stockDataEF.Models.Tables;

namespace stock.Models
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IRepository<Stock> stockRepository;
        private readonly IRepository<Product> productRepository;
        private readonly IRepository<DatedPrice> priceRepository;

        public HomeController(ILogger<HomeController> logger, IRepository<Stock> stockRepository, IRepository<Product> productRepository,IRepository<DatedPrice> priceRepository)
        {
            this.stockRepository = stockRepository;
            this.productRepository = productRepository;
            this.priceRepository = priceRepository;
            _logger = logger;
        }

        [HttpGet]
        public ViewResult Add()
        {
            var model = new Stock();
            model.ProductsAPIStrings.Add(new APIStrings());
            return View(model);
        }

        [HttpPost]
        public IActionResult Add(Stock model)
        {
            if (ModelState.IsValid)
            {            
                stockRepository.Add(model);
                return RedirectToAction("details", new { id = model.Id });
            }
            return View();
        }

        public IActionResult Index()
        {
            var model =  stockRepository.GetAll();
            return View(model);
        }

        public async  Task<IActionResult> Read(int Id)
        {            
            var stock = stockRepository.Get(Id);
            var readStockMethod = new MoscowStockRead();
            await readStockMethod.ReadAPI(stock.ProductsAPIStrings.FirstOrDefault(s=>s.Description=="чтение приборов").Name);

            var model= readStockMethod.DeserialiseProductsAsync();

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

                var checkDuplicatePriceFromDB = priceRepository.GetAll().FirstOrDefault(d => d.DateOfOperation == product.DatedPrice.DateOfOperation&&
                                                                                             d.ProductId==product.Product.Id);
                if (checkDuplicatePriceFromDB == null)
                {
                    priceRepository.Add(product.DatedPrice);
                }
               
               
            }         
            return View(model);
        }
        public  IActionResult Details(int id)
        {
            var model = new DetailsViewModel
            {
                Product = productRepository.GetAll().Where(p => p.StockId == id),
                DatedPrices = priceRepository.GetAll()
            }; 
            return View(model);
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
