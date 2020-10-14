using Microsoft.AspNetCore.Identity;
using stockDataEF.Models;
using stockDataEF.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace stock.Models
{
    public class ApplicationUser:IdentityUser
    {
        public List<ExternalLogin> AvaliableStocks { get; set; } = new List<ExternalLogin>();
    }
}
