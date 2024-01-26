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
    public class BestsellerController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BestsellerController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Bestseller
        public async Task<IActionResult> Index()
        {
            return View(await _context.Bestseller.ToListAsync());
        }

        // GET: Bestseller/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bestseller = await _context.Bestseller
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bestseller == null)
            {
                return NotFound();
            }

            return View(bestseller);
        }

        // GET: Bestseller/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Bestseller/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Author,Description,ISBN,DatePublished,Price,Image")] Bestseller bestseller)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bestseller);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(bestseller);
        }

        // GET: Bestseller/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bestseller = await _context.Bestseller.FindAsync(id);
            if (bestseller == null)
            {
                return NotFound();
            }
            return View(bestseller);
        }

        // POST: Bestseller/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Author,Description,ISBN,DatePublished,Price,Image")] Bestseller bestseller)
        {
            if (id != bestseller.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bestseller);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BestsellerExists(bestseller.Id))
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
            return View(bestseller);
        }

        // GET: Bestseller/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bestseller = await _context.Bestseller
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bestseller == null)
            {
                return NotFound();
            }

            return View(bestseller);
        }

        // POST: Bestseller/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bestseller = await _context.Bestseller.FindAsync(id);
            _context.Bestseller.Remove(bestseller);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BestsellerExists(int id)
        {
            return _context.Bestseller.Any(e => e.Id == id);
        }
    }
}
