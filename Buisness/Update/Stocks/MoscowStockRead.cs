using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using stock.Buisness.APIRead.APIModels;
using stockDataEF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace stock.Buisness.APIRead.Stocks
{
    public class MoscowStockRead : IStockRead
    {
        public Stock Stock { get ; set; } 

        public int UpdateTimeSeconds { get; } = 20000;


        public DateTime LastUpdated { get; set; }

       
        

        public async   Task<IEnumerable<APIProduct>> DeserialiseProductsAsync()
        {
            var companiesList = new List<APIProduct>();
            var content = await ReadAPI(@"http://iss.moex.com/iss/engines/stock/markets/shares/boards/TQBR/securities.json");
            foreach (var product in content["data"].Children())
            {
                if (product != null)
                {
                    companiesList.Add(new APIProduct
                    {
                        Product = new Product { Name = product[2] == null ? "null" : product[2].Value<string>() },
                        DatedPrice = new DatedPrice
                        {
                            Price = product[3].Value<double?>() == null ? 0.0 : product[3].Value<double>(),
                            DateOfOperation = product[17].Value<string>() == "0000-00-00" ? DateTime.MinValue : product[17].Value<DateTime>()
                        }
                    }) ;
                }

            }
            return companiesList;
        }

        public async Task<JToken>  ReadAPI(string url)
        {
            var companiesList = new List<Product>();
            var client = new RestClient(url);
            var request = new RestRequest(Method.GET);
            IRestResponse response = await client.ExecuteAsync(request);
            if (response.IsSuccessful)
            {
                return  JsonConvert.DeserializeObject<JToken>(response.Content)["securities"];
                //return response.Content;
            }
                return "";
        }

        
    }
}
