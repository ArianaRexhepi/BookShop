using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Books.Models
{
    public class Cart
    {
        private readonly ApplicationDbContext _context;

        public Cart(ApplicationDbContext context)
        {
            _context = context;
        }
        public string Id { get; set; }
        public List<CartItem> CartItems { get; set; }

        public static Cart GetCart(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;

            var context = services.GetService<ApplicationDbContext>();
            string cartId = session.GetString("Id") ?? Guid.NewGuid().ToString();

            session.SetString("Id", cartId);

            return new Cart(context) { Id = cartId };
        }

        public List<CartItem> GetAllCartItems()
        {
            return CartItems ??
                    (CartItems = _context.CartItems.Where(ci => ci.CartId == Id)
                    .Include(ci => ci.Book)
                    .ToList());
        }

        public int GetCartTotal()
        {
           return (int)_context.CartItems
        .Where(ci => ci.CartId == Id)
        .Select(ci => ci.Book.Price * ci.Quantity)
        .Sum();
        }



    }
}