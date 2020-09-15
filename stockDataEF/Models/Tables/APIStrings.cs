using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace stockDataEF.Models.Tables
{
    public class APIStrings
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [MaxLength(400)]
        public string Name { get; set; }
        [Required]
        [MaxLength(400)]
        public string Description { get; set; }
        [Required]
        public int StockId { get; set; }
        public Stock Stock { get; set; }
    }
}
