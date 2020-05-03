using PizzaShop.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaShop.Repository
{
    public class FoodRepository_cs : IRepository<Food, int>
    {
            private readonly MyDbContextC _myDbContext;

            public FoodRepository_cs(MyDbContextC myDbContext)
            {
                _myDbContext = myDbContext;
            }
            public void Create(Food food)
            {
                _myDbContext.Add(food);
                _myDbContext.SaveChanges();

            }

            public void Delete(int id)
            {
                Food food = _myDbContext.Foods.Find(id);
                if (food != null)
                    _myDbContext.Remove(food);
                _myDbContext.SaveChanges();
            }

            public Food GetById(int id)
            {
                return _myDbContext.Foods.Find(id);
            }

            public IEnumerable<Food> GetAll()
            {
                return _myDbContext.Foods;
            }

            public Food Update(Food food)
            {
                var foodModified = _myDbContext.Foods.Attach(food);
                 foodModified.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                _myDbContext.SaveChanges();
                return food;
            }
     
    }
}
