using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestSharp;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace stock.Models
{
    public class MockProductRepository : IProductRepository
    {

        private List<Product> companiesList;

        public MockProductRepository()
        {
            companiesList = new List<Product>();
            {             
               //new Product{Id = 1, Name = "GazProm",Price = 100 },
               //new Product{Id = 1, Name = "MTS",Price = 100 },
               //new Product{Id = 1, Name = "Megafone",Price = 100 },
            };

        }
        public Product Add(Product product)
        {
            product.Id = companiesList.Max(e => e.Id) + 1;
            companiesList.Add(product);
            return product;
        }

        public Product Delete(int id)
        {
            Product product = companiesList.FirstOrDefault(e => e.Id == id);
            if (product != null) companiesList.Remove(product);
            return product;
        }

        public async Task<IEnumerable<Product>> GetAllAsync(Stock stock)
        {
            var client = new RestClient(stock.ProductsAPIString);
            var request = new RestRequest(Method.GET);
            IRestResponse response = await client.ExecuteAsync(request);
            if (response.IsSuccessful)
            {
                var content = JsonConvert.DeserializeObject<JToken>(response.Content)["securities"];
                       
                foreach (var product in content["data"].Children())
                {
                    if (product != null)
                    {
                        companiesList.Add(new Product
                        {
                            Id = companiesList.DefaultIfEmpty().Max(e =>e== null ? 0 :e.Id) + 1,
                            Name = product[2]==null? "null": product[2].Value<string>(),
                            Price = product[3].Value<double?>() == null ? 0.0 : product[3].Value<double>()
                        });
                    }
                }               
            }

            return companiesList;
        }

        public async Task<Product> GetAsync(int id)
        {

            var client = new RestClient($"http://iss.moex.com/iss/securities.json?q=sber");
            var request = new RestRequest(Method.GET);
            IRestResponse response = await client.ExecuteAsync(request);

            if (response.IsSuccessful)
            {
                var content = JsonConvert.DeserializeObject<JToken>(response.Content);

                //Get the league caption
                var name = content["metadata"].Value<string>();

               
            }

            return new Product { Id = 1, Name = response.Headers.ToString(), Price = 300 };
        }


        public Product Get(int id)
        {
            return companiesList.FirstOrDefault(c => c.Id == id);
        }

        public IEnumerable<Product> GetAll()
        {
            return companiesList;
        }

        public Product Update(Product cnangedProduct)
        {
            Product product = companiesList.FirstOrDefault(e => e.Id == cnangedProduct.Id);
            if (product != null)
            {
                product.Name = cnangedProduct.Name;
                product.Price = cnangedProduct.Price;
            }
            return product;
        }
    }
}
