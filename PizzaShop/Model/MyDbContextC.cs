using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaShop.Model
{
    public class MyDbContextC : IdentityDbContext<ExtendedUser, IdentityRole<int>, int>
    {
        public MyDbContextC(DbContextOptions<MyDbContextC> option) : base(option)
        {

        }

        public DbSet<Order> Orders { get; set; }

        public DbSet<Food> Foods { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }
 
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Seed();
        }

    }
}

