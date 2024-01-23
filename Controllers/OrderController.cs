using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Books.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

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

        [HttpPost]
        public IActionResult CheckOut(Order order)
        {
            var cartItems = _cart.GetAllCartItems();
            _cart.CartItems = cartItems;

            if(_cart.CartItems.Count == 0)
            {
                ModelState.AddModelError("", "Cart is empty, please add a book first!");
            }

            if(ModelState.IsValid)
            {
                CreateOrder(order);
                _cart.ClearCart();
                return View("Checkout Complete", order);
            }
            return View(order);
        }

         public IActionResult CheckOutComplete(Order order)
        {
            return View(order);
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