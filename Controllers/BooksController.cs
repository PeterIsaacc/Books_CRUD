using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;
using Cluster0.Models;
using Cluster0.Data;
public class BooksController : Controller
{
    private readonly BookstoreContext _context;

    public BooksController(BookstoreContext context)
    {
        _context = context;
    }

    // GET: Books
    public async Task<IActionResult> Index()
    {
        var books = await _context.Books.Find(_ => true).ToListAsync();
        return View(books);
    }

    // GET: Books/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Books/Create
    [HttpPost]
    public async Task<IActionResult> Create([Bind("BookName,Author,Rating,DateAdded")] Book book)
    {
        if (ModelState.IsValid)
        {
            await _context.Books.InsertOneAsync(book);
            return RedirectToAction(nameof(Index));
        }
        return View(book);
    }

    public async Task<IActionResult> Edit(string id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var book = await _context.Books.Find(b => b.Id == ObjectId.Parse(id)).FirstOrDefaultAsync();
        if (book == null)
        {
            return NotFound();
        }
        return View(book);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(string id, [Bind("Id,BookName,Author,Rating,DateAdded")] Book book)
    {
        if (id != book.Id.ToString())
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            var filter = Builders<Book>.Filter.Eq(b => b.Id, book.Id);
            var update = Builders<Book>.Update
                .Set(b => b.BookName, book.BookName)
                .Set(b => b.Author, book.Author)
                .Set(b => b.Rating, book.Rating)
                .Set(b => b.DateAdded, book.DateAdded);
            await _context.Books.UpdateOneAsync(filter, update);
            return RedirectToAction(nameof(Index));
        }
        return View(book);
    }

    // GET: Books/Delete/5
    public async Task<IActionResult> Delete(string id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var book = await _context.Books.Find(b => b.Id == ObjectId.Parse(id)).FirstOrDefaultAsync();
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
        var filter = Builders<Book>.Filter.Eq(b => b.Id, ObjectId.Parse(id));
        await _context.Books.DeleteOneAsync(filter);
        return RedirectToAction(nameof(Index));
    }
}
