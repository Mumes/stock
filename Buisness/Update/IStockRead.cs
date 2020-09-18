using stock.Buisness.APIRead.APIModels;
using stockDataEF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace stock.Buisness.APIRead
{
   public interface IStockRead
    {
        public int UpdateTimeSeconds { get; }
        public DateTime LastUpdated{ get; set; }
        Stock Stock { get; set; }
        Task<string> ReadAPI(string url);
        IEnumerable<APIProduct>  DeserialiseProductsAsync();      
    }
}
