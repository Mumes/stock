using stock.Models;
using stockDataEF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace stock.Buisness.APIRead.APIModels
{
    public class APIProduct
    {     
       public Product Product { get; set; }
       public DatedPrice DatedPrice { get; set; }
    }
}
