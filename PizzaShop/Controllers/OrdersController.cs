using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using PizzaShop.Model;
using PizzaShop.Repository;

namespace PizzaShop.Controllers
{
    [Route("order")]
    public class OrdersController : Controller
    {
        public IRepository<Order, int> orderRepository;
        private readonly IHostingEnvironment hostingEnvironment;
        public OrderDetailsRepository orderDetailsRepository;

        public OrdersController(IRepository<Order, int> orderRepository, OrderDetailsRepository orderDetailsRepository, IHostingEnvironment hostingEnvironment)
        {
            this.orderRepository = orderRepository;
            this.hostingEnvironment = hostingEnvironment;
            this.orderDetailsRepository = orderDetailsRepository;

        }



        [HttpGet]
        public IActionResult Get()
        {
            IEnumerable<Order> orders = orderRepository.GetAll();
            List<OrderDetails[]> returnObj= new List<OrderDetails[]>() ;
            foreach (var order in orders)
            {
                var orderDetails =(OrderDetails[]) orderDetailsRepository.GetAll(order.Id);
                foreach(var detail in orderDetails)
                {
                    detail.Address = order.Adress;
                }
                if(orderDetails != null)
                {
                    returnObj.Add(orderDetails);
                }

            }
            return Ok(returnObj);
        }

        [Route("{id}")]
        [HttpGet]
        public IActionResult GetById(int id)
        {
            Order order = orderRepository.GetById(id);

            if (order != null)
                return Ok(order);
            return BadRequest(new { message = "Order is not found" });
        }

        [HttpPost]
        public IActionResult Create([FromBody]Order order)
        {
            if (ModelState.IsValid)
            {
                    orderRepository.Create(order);
             
                     return Ok(order);
            }
            else
            {
                ModelState.AddModelError("", "Error. Check the form data.");
            }
            return BadRequest(new { message = "error", ModelState });
        }

        [Route("orderDetails")]
        [HttpPost]
        public IActionResult Create([FromBody]OrderDetails orderdetails)
        {
            if (ModelState.IsValid)
            {
                if (orderdetails.OrderId > 0)
                {
                    Order order = new Order() { Id = orderdetails.OrderId, Adress = orderdetails.Address };
                    orderRepository.Update(order);
                }

                orderDetailsRepository.Create(orderdetails);
                return Ok(orderdetails);
            }
            else
            {
                ModelState.AddModelError("", "Error. Check the form data.");
            }
            return BadRequest(new { message = "error", ModelState });
        }




        [Route("delete/{id}")]
        [HttpGet]
        public IActionResult Delete(int id)
        {
            try
            {
                orderRepository.Delete(id);
                return Ok(id);

            }
            catch
            {
                return BadRequest(new { message = "Order not found" });
            }


        }


        [HttpPut]
        public IActionResult Update(Order order)
        {

            if (ModelState.IsValid)
            {

                Order orderUpdate = orderRepository.Update(order);

                if (orderUpdate != null)
                {
                    return Ok(orderUpdate);
                }
            }

            return BadRequest(new { message = "Order not found", ModelState });
        }


    }
}