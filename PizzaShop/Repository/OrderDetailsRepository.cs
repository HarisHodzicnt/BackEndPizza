using PizzaShop.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaShop.Repository
{
    public class OrderDetailsRepository
    {
        private readonly MyDbContextC _myDbContext;

        public OrderDetailsRepository(MyDbContextC myDbContext)
        {
            _myDbContext = myDbContext;
        }
        public void Create(OrderDetails orderDetails)
        {
            _myDbContext.Add(orderDetails);
            _myDbContext.SaveChanges();

        }

        public void Delete(int id)
        {
            OrderDetails orderDetails = _myDbContext.OrderDetails.Find(id);
            if (orderDetails != null)
                _myDbContext.Remove(orderDetails);
            _myDbContext.SaveChanges();
        }

        public OrderDetails GetById(int id)
        {
            return _myDbContext.OrderDetails.Find(id);
        }

        public IEnumerable<OrderDetails> GetAll(int orderId)
        {


            return _myDbContext.OrderDetails.Where(x=>x.OrderId==orderId).ToArray();
        }

        public OrderDetails Update(OrderDetails orderDetails)
        {
            var orderModified = _myDbContext.OrderDetails.Attach(orderDetails);
            orderModified.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _myDbContext.SaveChanges();
            return orderDetails;
        }
    }
}
