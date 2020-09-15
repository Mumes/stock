using stock.Buisness.APIRead.APIModels;
using stock.Models;
using stockDataEF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace stock.Buisness.APIRead
{
   public interface IStockRead
    {
        Stock Stock { get; set; }
        Task<string> ReadAPI(string url);
        IEnumerable<APIProduct>  DeserialiseProductsAsync();      
    }
}
