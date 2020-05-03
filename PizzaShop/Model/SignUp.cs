using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaShop.Model
{
    public class SignUp
    {
        //<int, CustomUserLogin, CustomUserRole, 

            [Required]
            [EmailAddress]
            [Remote(action: "CheckEmail", controller: "Account")]
            public string Email { get; set; }
            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }
            [Required]
            [Compare("Password", ErrorMessage = "Passwordi se ne podudaraju")]
            [Display(Name = "Confirm Password")]
            public string ConfirmPassword { get; set; }
            [Required]
            public string City { get; set; }
            [Required]
            public string PhoneNumber { get; set; }
            public string Name { get; set; }

    }
}
