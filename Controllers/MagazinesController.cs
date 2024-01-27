#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Books.Models;

namespace Books.Controllers
{
    public class MagazinesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MagazinesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Magazines
       public async Task<IActionResult> Index(string searchString, string minPrice, string maxPrice, string sort)
        {
            var magazines = _context.Magazines.Select(b => b);

            if (!string.IsNullOrEmpty(searchString))
            {
                magazines = magazines.Where(b => b.Title.Contains(searchString) || b.Author.Contains(searchString));
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
                magazines = magazines.Where(b => b.Price < 50);
            }

            if (Request.Query.ContainsKey("50to100"))
            {
                magazines = magazines.Where(b => b.Price >= 50 && b.Price <= 100);
            }

            if (Request.Query.ContainsKey("over100"))
            {
                magazines = magazines.Where(b => b.Price > 100);
            }
            switch (sort)
            {
                case "oldest":
                    magazines = magazines.OrderBy(item => item.DatePublished);
                    break;

                case "newest":
                    magazines = magazines.OrderByDescending(item => item.DatePublished);
                    break;

                // Default case (when "All" is selected)
                default:
                    magazines = magazines.OrderByDescending(item => item.DatePublished);
                    break;
            }

            return View(await magazines.AsNoTracking().ToListAsync());
        }


        // GET: Magazines/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var magazines = await _context.Magazines
                .FirstOrDefaultAsync(m => m.Id == id);
            if (magazines == null)
            {
                return NotFound();
            }

            return View(magazines);
        }

        // GET: Magazines/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Magazines/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Author,Description,ISBN,DatePublished,Price,Image")] Magazines magazines)
        {
            if (ModelState.IsValid)
            {
                _context.Add(magazines);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(magazines);
        }

        // GET: Magazines/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var magazines = await _context.Magazines.FindAsync(id);
            if (magazines == null)
            {
                return NotFound();
            }
            return View(magazines);
        }

        // POST: Magazines/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Author,Description,ISBN,DatePublished,Price,Image")] Magazines magazines)
        {
            if (id != magazines.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(magazines);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MagazinesExists(magazines.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(magazines);
        }

        // GET: Magazines/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var magazines = await _context.Magazines
                .FirstOrDefaultAsync(m => m.Id == id);
            if (magazines == null)
            {
                return NotFound();
            }

            return View(magazines);
        }

        // POST: Magazines/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var magazines = await _context.Magazines.FindAsync(id);
            _context.Magazines.Remove(magazines);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MagazinesExists(int id)
        {
            return _context.Magazines.Any(e => e.Id == id);
        }
    }
}
