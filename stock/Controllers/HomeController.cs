using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using stock.Buisness.APIRead.APIModels;
using stock.Buisness.APIRead.Stocks;
using stock.Models;
using stock.ViewModels;

namespace stock.Controllers
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
            return View();
        }

        [HttpPost]
        public IActionResult Add(Stock model)
        {
            if (ModelState.IsValid)
            {
                var stock = new Stock
                {
                    Name = model.Name,
                    ProductsAPIString = model.ProductsAPIString
                };
                stockRepository.Add(stock);
                return RedirectToAction("details", new { id = stock.Id });
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
            stock.StockRead = new MoscowStockRead();
            await stock.StockRead.ReadAPI(stock.ProductsAPIString);

            var model= stock.StockRead.DeserialiseProductsAsync();

            foreach (var product in model)
            {
                product.Product.StockId = Id;
                product.Product.Stock = stock;
                
                var checkDuplicateProductFromDB = productRepository.GetAll().FirstOrDefault(d => d.Name == product.Product.Name);
                if (checkDuplicateProductFromDB != null)
                {
                    product.DatedPrice.Product = checkDuplicateProductFromDB;
                }
                else 
                {
                    product.DatedPrice.Product = product.Product;
                    productRepository.Add(product.Product);
                }                                 
                priceRepository.Add(product.DatedPrice);
            }         
            return View(model);
        }
        public  IActionResult Details(int id)
        {
            var stock = stockRepository.Get(id);
            stock.StockRead = new MoscowStockRead();

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
