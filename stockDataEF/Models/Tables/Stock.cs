
using stockDataEF.Models.Tables;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace stockDataEF.Models
{
    public class Stock
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [MaxLength(200)]
        public string Name { get; set; }

        public DateTime LastUpdated { get; set; }
        public List<APIStrings> ProductsAPIStrings { get; set; } = new List<APIStrings>();
        public List<Product> Products { get; set; } = new List<Product>();
    }
}
