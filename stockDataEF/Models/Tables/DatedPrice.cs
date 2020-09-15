using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace stockDataEF.Models
{
    public class DatedPrice
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public DateTime DateOfOperation { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
