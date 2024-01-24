using System;
using System.Collections.Generic;

namespace Books.Areas.Identity.Data
{
    public partial class Book
    {
        public Book()
        {
            CartItems = new HashSet<CartItem>();
            OrderItems = new HashSet<OrderItem>();
        }

        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Author { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Isbn { get; set; } = null!;
        public DateTime DatePublished { get; set; }
        public decimal Price { get; set; }
        public string? Image { get; set; }

        public virtual ICollection<CartItem> CartItems { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
}
