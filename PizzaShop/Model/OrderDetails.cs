using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaShop.Model
{
    public class OrderDetails:IEntity
    {
 
        [ForeignKey("Food")]
        public int FoodId { get; set; }
        public virtual Food Food { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        [ForeignKey("Order")]
        public int OrderId { get; set; }
        public string Size { get; set; }
        public string Aditional { get; set; }
        [NotMapped]
        public string Address { get; set; }
        public string Price { get; set; }

    }
}
