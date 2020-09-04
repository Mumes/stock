using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using stock.Models;

namespace stock.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IStockRepository stockRepository;
        private readonly IProductRepository productRepository;

        public HomeController(ILogger<HomeController> logger, IStockRepository stockRepository, IProductRepository productRepository)
        {
            this.stockRepository = stockRepository;
            this.productRepository = productRepository;
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


        public async Task<IActionResult> Details(int Id)
        {
            var model = await productRepository.GetAllAsync(stockRepository.GetAll().Where(s=> s.Id == Id).FirstOrDefault());
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
