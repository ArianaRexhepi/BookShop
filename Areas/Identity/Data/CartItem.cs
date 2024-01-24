using System;
using System.Collections.Generic;

namespace Books.Areas.Identity.Data
{
    public partial class CartItem
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public int Quantity { get; set; }
        public string CartId { get; set; } = null!;

        public virtual Book Book { get; set; } = null!;
    }
}
