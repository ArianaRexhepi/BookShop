using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Books.Data;
using Books.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Books.Controllers
{
    [AllowAnonymous]
    public class MagStoreController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MagStoreController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize]
        public async Task<IActionResult> Index(string searchString, string minPrice, string maxPrice, int? pageNumber)
        {
            var books = _context.Magazines.Select(b => b);

            if (!string.IsNullOrEmpty(searchString))
            {
                books = books.Where(b => b.Title.Contains(searchString) || b.Author.Contains(searchString));
            }
            // if (!string.IsNullOrEmpty(minPrice))
            // {
            //     var min = int.Parse(minPrice);

            //     books = books.Where(b => b.Price >= min);
            // }
            // if (!string.IsNullOrEmpty(maxPrice))
            // {
            //     var max = int.Parse(maxPrice);

            //     books = books.Where(b => b.Price <= max);
            // }
            if (Request.Query.ContainsKey("under50"))
            {
                books = books.Where(b => b.Price < 50);
            }

            if (Request.Query.ContainsKey("50to100"))
            {
                books = books.Where(b => b.Price >= 50 && b.Price <= 100);
            }

            if (Request.Query.ContainsKey("over100"))
            {
                books = books.Where(b => b.Price > 100);
            }

            int pageSize = 6;
            return View(await PaginatedList<Magazines>.CreateAsync(books.AsNoTracking(), pageNumber ?? 1, pageSize));
            // return View(await books.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Magazines
                .FirstOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }
    }
}