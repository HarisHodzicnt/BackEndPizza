using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaShop.Model
{
    public class Food:IEntity
    {
        public string Name { get; set; }
        public int Price { get; set; }
        [NotMapped]
        public IFormFile Photo { get; set; }
        public string PhotoPath { get; set; }
        public string Size { get; set; }
        public string Material { get; set; }

    }
}
