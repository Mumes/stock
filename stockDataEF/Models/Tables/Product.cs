using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace stockDataEF.Models
{
    public class Product
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [MaxLength(200)]
        public string Name { get; set; }
        public List<DatedPrice> DatedPrices { get; set; } = new List<DatedPrice>();
        public int StockId { get; set; }
        public Stock Stock { get; set; }
    }
}
