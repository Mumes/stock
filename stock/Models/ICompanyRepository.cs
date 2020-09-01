using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace stock.Models
{
   public interface ICompanyRepository
    {
        Company Get(int id);

        Task<Company> GetAsync(int id);
        IEnumerable<Company> GetAll();
        Task<IEnumerable<Company>> GetAllAsync();
        Company Add(Company company);
        Company Update(Company cnangedCompany);
        Company Delete(int id);

    }
}
