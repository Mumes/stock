using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using stock.Buisness.APIRead.Stocks;
using stock.Models;
using stock.ViewModels;
using stockDataEF.Models;
using stockDataEF.Models.Tables;

namespace stock.Models
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IRepository<Stock> stockRepository;
        private readonly IRepository<Product> productRepository;
        private readonly IRepository<DatedPrice> priceRepository;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IRepository<ExternalLogin> externalLoginRepostory;

        public HomeController(ILogger<HomeController> logger, IRepository<Stock> stockRepository,
                                IRepository<Product> productRepository,IRepository<DatedPrice> priceRepository
                                , IRepository<ExternalLogin> externalLoginRepostory
                                ,UserManager<ApplicationUser> userManager)
        {
            this.stockRepository = stockRepository;
            this.productRepository = productRepository;
            this.priceRepository = priceRepository;
            this.userManager = userManager;
            this.externalLoginRepostory = externalLoginRepostory;
            _logger = logger;
        }

        [HttpGet]
        public ViewResult Add()
        {
            var model = new Stock();
            model.ProductsAPIStrings.Add(new APIStrings());
            return View(model);
        }

        public IActionResult AddStockList()
        {
            var model = stockRepository.GetAll();
            return View(model);
        }

        [HttpGet]
        public IActionResult AddStockConection()
        {
            return View();
        }

        [HttpPost]
        public async Task <IActionResult> AddStockConection( AddStockConnectionViewModel model, int id)
        {
            var user = await userManager.FindByNameAsync(User.Identity.Name);
            ExternalLogin externalLogin = new ExternalLogin
            {
                Login = model.Login,
                Password = model.Password,
                StockId = id,
               // ApplicationUserId = user.Id
            };
            user.AvaliableStocks.Add(externalLogin);
            
            IdentityResult result = await userManager.UpdateAsync(user);
            // externalLoginRepostory.Add(externalLogin);
            return RedirectToAction("index");
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

        public async Task<IActionResult> Index()
        {
           // var model =  stockRepository.GetAll();
            var model = await userManager.FindByNameAsync(User.Identity.Name);
            List<Stock> stocks = new List<Stock>();
            foreach (var stock in externalLoginRepostory.GetAll().Where(p=>p.ApplicationUserId == model.Id).Select(p=>p.StockId).ToList())
                stocks.Add(stockRepository.Get(stock));
            return View(stocks);
        }

        public async  Task<IActionResult> Read(int Id)
        {            
            var stock = stockRepository.Get(Id);
            var readStockMethod = new MoscowStockRead();
           

            var model= await readStockMethod.DeserialiseProductsAsync();

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
