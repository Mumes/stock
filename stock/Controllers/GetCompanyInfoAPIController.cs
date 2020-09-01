using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using stock.Models;

namespace stock.Controllers
{
    
    public class GetCompanyInfoAPIController:Controller
    {
        private readonly ICompanyRepository _companyRepository;

        public GetCompanyInfoAPIController(ICompanyRepository _companyRepository)
        {
            this._companyRepository = _companyRepository;
        }
        public async Task<IActionResult> Index()
        {
            var model = await _companyRepository.GetAllAsync();
            return View(model);
        }

    }
}
