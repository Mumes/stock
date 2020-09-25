using Newtonsoft.Json.Linq;
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
        Task<JToken> ReadAPI(string url);
        Task<IEnumerable<APIProduct>>  DeserialiseProductsAsync();      
    }
}
