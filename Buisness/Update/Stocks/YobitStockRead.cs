using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using stock.Buisness.APIRead;
using stock.Buisness.APIRead.APIModels;
using stockDataEF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Buisness.Update.Stocks
{
    class YobitStockRead : IStockRead
    {
        public Stock Stock { get; set; }

        public int UpdateTimeSeconds { get; } = 20000;

        public DateTime LastUpdated { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

      
        public async Task<IEnumerable<APIProduct>> DeserialiseProductsAsync()
        {
            var companiesList = new List<APIProduct>();
            string allPairs = "";
            int counter = 0;
            var pairs =
                await ReadAPI(@"https://yobit.net/api/3/info");
            foreach (var pair in pairs["pairs"].Children())
            {
                counter++;
                if (counter == 50) break;
                var jProperty = pair.ToObject<JProperty>();
                if (Regex.IsMatch(jProperty.Name,@"_btc$"))
                {
                    allPairs += '-' + jProperty.Name;
                }
               // companiesList.Add(new APIProduct { new Product {Name = pair.Value() } })
            }
            allPairs = allPairs.TrimStart('-');

            var products = await ReadAPI(@$"https://yobit.net/api/3/ticker/{allPairs}");
            foreach (var product in products.Children())
            {
                var jProperty = product.ToObject<JProperty>();                
                companiesList.Add(new APIProduct 
                {
                    Product = new Product
                    { 
                        Name = jProperty.Name
                    },
                    DatedPrice = new DatedPrice
                    { 
                        Price = product.Children().FirstOrDefault(p => p.HasValues == true)["last"].Value<double>(),
                        DateOfOperation = DateTimeOffset.FromUnixTimeSeconds(
                            product.Children().FirstOrDefault(p => p.HasValues == true)["updated"].Value<int>())
                                                     .DateTime.ToLocalTime()
            }
                });
            }
            return companiesList;
        }

        public async Task<JToken> ReadAPI(string url)
        {
            var companiesList = new List<Product>();
            var client = new RestClient(url);
            var request = new RestRequest(Method.GET);
            IRestResponse response = await client.ExecuteAsync(request);
            if (response.IsSuccessful)
            {
                return JsonConvert.DeserializeObject<JToken>(response.Content);
            }
            return "";
        }
    }
}
