using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace stock.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "Ваш Email")]
        [Remote(action: "IsEmailInUse", controller: "account")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Недействительный Email")]
        public string Email { get; set; }
        [Required]
        [Display(Name = "Пароль")]
        [DataType(DataType.Password, ErrorMessage = "Недействительный Email")]

        public string Password { get; set; }
        [Required]
        [Display(Name = "Подтвердите пароль")]
        [DataType(DataType.Password, ErrorMessage = "Недействительный Email")]
        [Compare("Password", ErrorMessage = "Неверный пароль")]
        public string ConfirmPassword { get; set; }
        
    }
}
