using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaShop.Model
{
    public class ExtendedUser : IdentityUser<int>
    {
        public string City { get; set; }
        public string Name { get; set; }

    }
}
