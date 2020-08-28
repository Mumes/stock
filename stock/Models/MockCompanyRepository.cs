using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestSharp;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace stock.Models
{
    public class MockCompanyRepository : ICompanyRepository
    {

        private List<Company> companiesList;

        public MockCompanyRepository()
        {
            companiesList = new List<Company>
            {
                new Company{Id = 1, Name = "GazProm",Price = 100 },
                new Company{Id = 1, Name = "MTS",Price = 100 },
                new Company{Id = 1, Name = "Megafone",Price = 100 },
            };

        }
        public Company Add(Company company)
        {
            company.Id = companiesList.Max(e => e.Id) + 1;
            companiesList.Add(company);
            return company;
        }

        public Company Delete(int id)
        {
            Company company = companiesList.FirstOrDefault(e => e.Id == id);
            if (company != null) companiesList.Remove(company);
            return company;
        }



        public async Task<Company> GetAsync(int id)
        {

            var client = new RestClient($"http://iss.moex.com/iss/history/engines/stock/markets/index/securities.json?date=2010-08-20");
            var request = new RestRequest(Method.GET);
            IRestResponse response = await client.ExecuteAsync(request);

            if (response.IsSuccessful)
            {
                var content = JsonConvert.DeserializeObject<JToken>(response.Content);

                //Get the league caption
                var name = content["metadata"].Value<string>();

               
            }

            return new Company { Id = 1, Name = response.Headers.ToString(), Price = 300 };
        }


        public Company Get(int id)
        {
            return companiesList.FirstOrDefault(c => c.Id == id);
        }

        public IEnumerable<Company> GetAll()
        {
            return companiesList;
        }

        public Company Update(Company cnangedCompany)
        {
            Company company = companiesList.FirstOrDefault(e => e.Id == cnangedCompany.Id);
            if (company != null)
            {
                company.Name = cnangedCompany.Name;
                company.Price = cnangedCompany.Price;
            }
            return company;
        }
    }
}
