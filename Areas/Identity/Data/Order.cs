using System;
using System.Collections.Generic;

namespace Books.Areas.Identity.Data
{
    public partial class Order
    {
        public Order()
        {
            OrderItems = new HashSet<OrderItem>();
        }

        public int Id { get; set; }
        public int OrderTotal { get; set; }
        public DateTime OrderPlaced { get; set; }

        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
}
