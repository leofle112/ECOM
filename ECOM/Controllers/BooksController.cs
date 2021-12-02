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
using ECOM.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace ECOM.Controllers
{
    [Authorize(Roles = "Admin")]

    public class BooksController : Controller
    {
        private readonly ECOMContext _context;
        private IWebHostEnvironment webHostEnvironment;
        private string fileName;
        private Stream fileStream;

        public BooksController(IWebHostEnvironment webHostEnvironment, ECOMContext context)
        {
            this.webHostEnvironment = webHostEnvironment;
            _context = context;

        }

        // GET: Books
        public async Task<IActionResult> Index()
        {
            var eCOMContext = _context.Books.Include(b => b.BookAuthor).Include(b => b.BookStore);
            return View(await eCOMContext.ToListAsync());
        }

        // GET: Books/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Books
                .Include(b => b.BookAuthor)
                .Include(b => b.BookStore)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // GET: Books/Create
        public IActionResult Create()
        {
            ViewData["BookAuthorId"] = new SelectList(_context.BookAuthors, "Id", "Name");
            ViewData["BookStoreId"] = new SelectList(_context.BookStores, "Id", "Name");
            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateBookViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var model = new Book()
                {
                    Name = vm.Name,
                    Descripton = vm.Descripton,
                    ISBN = vm.ISBN,
                    Price = vm.Price,
                    BookAuthorId = vm.BookAuthorId,
                    BookStoreId = vm.BookStoreId,

                };
                foreach (var item in vm.Pictures)
                {
                    model.BookPictures.Add(new BookPictures()
                    {
                        PictureUri = uploadImage(item),
                        Book = model
                    });
                }

                _context.Books.Add(model);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vm);
        }

        private string uploadImage(IFormFile item)
        {
            string fileName = null;
            if (item != null)
            {
                string uploadDir = Path.Combine(webHostEnvironment.WebRootPath, "Images");
                fileName = Guid.NewGuid().ToString() + "-" + item.FileName;
                string filePath = Path.Combine(uploadDir, fileName);
                using (var fileStram = new FileStream(filePath, FileMode.Create))
                {
                    item.CopyTo(fileStream);
                }

                return fileName;
        }

        // GET: Books/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            ViewData["BookAuthorId"] = new SelectList(_context.BookAuthors, "Id", "Id", book.BookAuthorId);
            ViewData["BookStoreId"] = new SelectList(_context.BookStores, "Id", "Name", book.BookStoreId);
            return View(book);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Name,Descripton,ISBN,Price,PictureUri,BookAuthorId,BookStoreId")] Book book)
        {
            if (id != book.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(book);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookExists(book.Id))
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
            ViewData["BookAuthorId"] = new SelectList(_context.BookAuthors, "Id", "Id", book.BookAuthorId);
            ViewData["BookStoreId"] = new SelectList(_context.BookStores, "Id", "Name", book.BookStoreId);
            return View(book);
        }

        // GET: Books/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Books
                .Include(b => b.BookAuthor)
                .Include(b => b.BookStore)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var book = await _context.Books.FindAsync(id);
            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookExists(string id)
        {
            return _context.Books.Any(e => e.Id == id);
        }
    }
}
