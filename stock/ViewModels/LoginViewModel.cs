using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace stock.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Ваш Email")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Недействительный Email")]
        public string Email { get; set; }
        [Required]
        [Display(Name = "Пароль")]
        [DataType(DataType.Password, ErrorMessage = "Недействительный Email")]
        public string Password { get; set; }

        [Display(Name = "Запомнить")]
        public bool RememberMe { get; set; }
        public string ReturnUrl { get; set; }
        //public IList<AuthenticationScheme> ExternalLogins { get; set; }
    }
}
