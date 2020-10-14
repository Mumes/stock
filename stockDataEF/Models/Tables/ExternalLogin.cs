using stock.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace stockDataEF.Models.Tables
{
    public class ExternalLogin
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public int StockId { get; set; }
        public Stock Stock { get; set; }
        [ForeignKey("AspNetUsers")]
        public string ApplicationUserId { get; set; }
    }
}
