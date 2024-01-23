using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Books.Models
{
    public class Order
    {
        public int Id { get; set; }
        public List<OrderItem> OrderItems{ get; set; }
        public int OrderTotal { get; set; }
        public DateTime OrderPlaced { get; set; }

    }
}