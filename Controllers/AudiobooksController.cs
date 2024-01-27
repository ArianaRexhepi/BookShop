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
    public class AudiobooksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AudiobooksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Audiobooks
       public async Task<IActionResult> Index(string searchString, string minPrice, string maxPrice, string sort)
        {
            var audiobooks = _context.Audiobooks.Select(b => b);

            if (!string.IsNullOrEmpty(searchString))
            {
                audiobooks = audiobooks.Where(b => b.Title.Contains(searchString) || b.Author.Contains(searchString));
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
                audiobooks = audiobooks.Where(b => b.Price < 50);
            }

            if (Request.Query.ContainsKey("50to100"))
            {
                audiobooks = audiobooks.Where(b => b.Price >= 50 && b.Price <= 100);
            }

            if (Request.Query.ContainsKey("over100"))
            {
                audiobooks = audiobooks.Where(b => b.Price > 100);
            }
            switch (sort)
            {
                case "oldest":
                    audiobooks = audiobooks.OrderBy(item => item.DatePublished);
                    break;

                case "newest":
                    audiobooks = audiobooks.OrderByDescending(item => item.DatePublished);
                    break;

                // Default case (when "All" is selected)
                default:
                    audiobooks = audiobooks.OrderByDescending(item => item.DatePublished);
                    break;
            }

            return View(await audiobooks.AsNoTracking().ToListAsync());
        }


        // GET: Audiobooks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var audiobooks = await _context.Audiobooks
                .FirstOrDefaultAsync(m => m.Id == id);
            if (audiobooks == null)
            {
                return NotFound();
            }

            return View(audiobooks);
        }

        // GET: Audiobooks/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Audiobooks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Author,Description,ISBN,DatePublished,Price,Image")] Audiobooks audiobooks)
        {
            if (ModelState.IsValid)
            {
                _context.Add(audiobooks);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(audiobooks);
        }

        // GET: Audiobooks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var audiobooks = await _context.Audiobooks.FindAsync(id);
            if (audiobooks == null)
            {
                return NotFound();
            }
            return View(audiobooks);
        }

        // POST: Audiobooks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Author,Description,ISBN,DatePublished,Price,Image")] Audiobooks audiobooks)
        {
            if (id != audiobooks.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(audiobooks);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AudiobooksExists(audiobooks.Id))
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
            return View(audiobooks);
        }

        // GET: Audiobooks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var audiobooks = await _context.Audiobooks
                .FirstOrDefaultAsync(m => m.Id == id);
            if (audiobooks == null)
            {
                return NotFound();
            }

            return View(audiobooks);
        }

        // POST: Audiobooks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var audiobooks = await _context.Audiobooks.FindAsync(id);
            _context.Audiobooks.Remove(audiobooks);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AudiobooksExists(int id)
        {
            return _context.Audiobooks.Any(e => e.Id == id);
        }
    }
}
