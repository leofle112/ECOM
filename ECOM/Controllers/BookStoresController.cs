using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ECOM.Data;
using ECOM.Models;
using Microsoft.AspNetCore.Authorization;

namespace ECOM.Controllers
{
    [Authorize(Roles ="Admin")]
    public class BookStoresController : Controller
    {
        private readonly ECOMContext _context;

        public BookStoresController(ECOMContext context)
        {
            _context = context;
        }

        // GET: BookStores
        public async Task<IActionResult> Index()
        {
            return View(await _context.BookStores.ToListAsync());
        }

        // GET: BookStores/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookStore = await _context.BookStores
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bookStore == null)
            {
                return NotFound();
            }

            return View(bookStore);
        }

        // GET: BookStores/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: BookStores/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] BookStore bookStore)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bookStore);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(bookStore);
        }

        // GET: BookStores/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookStore = await _context.BookStores.FindAsync(id);
            if (bookStore == null)
            {
                return NotFound();
            }
            return View(bookStore);
        }

        // POST: BookStores/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] BookStore bookStore)
        {
            if (id != bookStore.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bookStore);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookStoreExists(bookStore.Id))
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
            return View(bookStore);
        }

        // GET: BookStores/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookStore = await _context.BookStores
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bookStore == null)
            {
                return NotFound();
            }

            return View(bookStore);
        }

        // POST: BookStores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bookStore = await _context.BookStores.FindAsync(id);
            _context.BookStores.Remove(bookStore);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookStoreExists(int id)
        {
            return _context.BookStores.Any(e => e.Id == id);
        }
    }
}
