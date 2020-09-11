using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace stock.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int StockId { get; set; }
        public Stock Stock { get; set; }
    }
}
