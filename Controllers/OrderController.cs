using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Books.Models;
using Microsoft.AspNetCore.Mvc;

namespace Books.Controllers
{
    public class OrderController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly Cart _cart;
       
        public OrderController(ApplicationDbContext context, Cart cart)
        {
            _context = context;
            _cart = cart;
        }

        public IActionResult Checkout()
        {
            return View();
        }

        public void CreateOrder(Order order)
        {
            order.OrderPlaced = DateTime.Now;

            var cartItems = _cart.CartItems;

            foreach (var item in cartItems)
            {
                var orderItem = new OrderItem()
                {
                    Quantity = item.Quantity,
                    BookId = item.Book.Id,
                    OrderId = order.Id,
                    Price = (int)(item.Book.Price * item.Quantity),

                };
                order.OrderItems.Add(orderItem);
                order.OrderTotal += orderItem.Price;
            }
            _context.Orders.Add(order);
            _context.SaveChanges();
        }
    }
}