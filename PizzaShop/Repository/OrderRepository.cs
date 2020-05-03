using PizzaShop.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaShop.Repository
{
    public class OrderRepository:IRepository<Order, int>
    {
        private readonly MyDbContextC _myDbContext;

        public OrderRepository(MyDbContextC myDbContext)
        {
            _myDbContext = myDbContext;
        }
        public void Create(Order order)
        {
            _myDbContext.Add(order);
            _myDbContext.SaveChanges();

        }

        public void Delete(int id)
        {
            Order order = _myDbContext.Orders.Find(id);
            if (order != null)
                _myDbContext.Remove(order);
            _myDbContext.SaveChanges();
        }

        public Order GetById(int id)
        {
            return _myDbContext.Orders.Find(id);
        }

        public IEnumerable<Order> GetAll()
        {
            return _myDbContext.Orders;
        }

        public Order Update(Order order)
        {
            var orderModified = _myDbContext.Orders.Attach(order);
            orderModified.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _myDbContext.SaveChanges();
            return order;
        }
    }
}
