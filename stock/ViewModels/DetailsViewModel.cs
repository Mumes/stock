using stock.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace stock.ViewModels
{
    public class DetailsViewModel
    {
        public IEnumerable<Product> Product { get; set; }
        public IEnumerable<DatedPrice> DatedPrices { get; set; }
    }
}
