using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using stock.Models;

namespace stock.Controllers
{
    
    public class GetProductInfoAPIController:Controller
    {
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<Stock> _stockRepository;

        public GetProductInfoAPIController(IRepository<Product> _productRepository, IRepository<Stock> _stockRepository)
        {
            this._productRepository = _productRepository;
            this._stockRepository = _stockRepository;
        }
        public async Task<IActionResult> Index()
        {
            var model =  _productRepository.GetAll();
            return View(model);
        }

    }
}
