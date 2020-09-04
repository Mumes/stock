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
        private readonly IProductRepository _productRepository;
        private readonly IStockRepository _stockRepository;

        public GetProductInfoAPIController(IProductRepository _productRepository, IStockRepository _stockRepository)
        {
            this._productRepository = _productRepository;
            this._stockRepository = _stockRepository;
        }
        public async Task<IActionResult> Index()
        {
            var model = await _productRepository.GetAllAsync(new Stock());
            return View(model);
        }

    }
}
