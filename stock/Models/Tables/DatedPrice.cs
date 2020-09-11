using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace stock.Models
{
    public class DatedPrice
    {
        public int Id { get; set; }
        public double Price { get; set; }
        public DateTime DateOfOperation { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
