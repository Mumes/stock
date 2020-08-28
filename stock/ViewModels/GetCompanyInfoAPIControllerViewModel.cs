using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace stock.ViewModels
{
    public class GetCompanyInfoAPIControllerViewModel
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Company name")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Current stock price")]
        public double Price { get; set; }
    }
}
