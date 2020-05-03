using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaShop.Model
{
    public static class ExtendedOnModelCreating
    {
        public static void Seed(this ModelBuilder builder)
        {
            builder.Entity<Food>().HasData(
                new Food { Id = 1, Name="Pizza1", Price=5 },
                new Food { Id = 2, Name = "Pizza2", Price = 5 }
                );

        }
    }
}
