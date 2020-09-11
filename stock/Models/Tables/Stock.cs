using stock.Buisness.APIRead;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace stock.Models
{
    public class Stock
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ProductsAPIString { get; set; }
        [NotMapped]
        public IStockRead StockRead { get; set; }
    }
}
