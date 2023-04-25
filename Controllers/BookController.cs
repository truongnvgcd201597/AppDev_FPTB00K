using FPTBook.Data;
using FPTBook.Models;
using FPTBook.ViewModels;
using FPTBook.ViewModels.Book;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace FPTBook.Controllers;

[AutoValidateAntiforgeryToken]
public class BookController : Controller
{
    private ApplicationDbContext _dbContext;

    public BookController(ApplicationDbContext db)
    {
        _dbContext = db;
    }

    [HttpGet]
    [AutoValidateAntiforgeryToken]
    public async Task<IActionResult> Index()
    {

        var booksListAsync = await _dbContext.Books.Include(c => c.Category).ToListAsync();
        var categoriesName = from n in _dbContext.Categories
                             select n.Name;
        var categoryName = await _dbContext.Categories.Select(b => b.Name).ToListAsync();
        BookCategoryViewModels bookCategory = new BookCategoryViewModels()
        {
            Books = booksListAsync,
            Categories = new SelectList(categoryName)
        };
        return View(bookCategory);
    }

    [HttpPost]
    [AutoValidateAntiforgeryToken]
    public async Task<IActionResult> Index(string bookCategory, string searchString)
    {
        if (_dbContext.Books == null)
        {
            return Content("Unable to retrieve books from the database. Please try again later.");
        }
        var categoriesName = from n in _dbContext.Categories
                             select n.Name;
        var categoryName = _dbContext.Books.Select(b => b);
        var books = from b in _dbContext.Books
                    select b;
        if (!string.IsNullOrEmpty(searchString))
        {
            books = books.Include(c => c.Category)
                .Where(s => s.Title!.ToLower().Contains(searchString.ToLower()));
        }
        if (!string.IsNullOrEmpty(bookCategory))
        {
            books = books.Include(c => c.Category)
                         .Where(s => s.Category != null && s.Category.Name.Equals(bookCategory));
        }
        var viewModel = new BookCategoryViewModels()
        {
            Books = await books.ToListAsync(),
            Categories = new SelectList(categoriesName.ToList()),
        };
        return View(viewModel);

    }

    [HttpGet]
    [AutoValidateAntiforgeryToken]
    public async Task<IActionResult> Detail(int id)
    {
        var book = await _dbContext.Books
            .Include(c => c.Category)
            .FirstOrDefaultAsync(d => d.Id == id);
        if (book != null)
        {
            var bookRelated = await _dbContext.Books.Where(c => c.CategoryId == book.CategoryId).ToListAsync();
            BookDetail bookDetail = new BookDetail()
            {
                Book = book,
                Books = bookRelated
            };
            return View(bookDetail);
        }

        return Content("This book doesn't exist");
    }

    [Authorize]
    [AutoValidateAntiforgeryToken]
    public async Task<IActionResult> Delete(string id)
    {
        if (HttpContext.User.IsInRole("OWNER"))
        {
            int bookId = Int32.Parse(id);
            var books = await _dbContext.Books.FindAsync(bookId);
            if (books != null)
            {
                _dbContext.Books.Remove(books);
                await _dbContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
        }
        return RedirectToAction("Index");
    }

    [HttpGet]
    [Authorize]
    [AutoValidateAntiforgeryToken]
    public IActionResult Create()
    {
        var categoriesName = from n in _dbContext.Categories
                             select n.Name;
        BookCreate formBookCreate = new BookCreate()
        {
            Categories = new SelectList(categoriesName)
        };
        return View(formBookCreate);
    }

    [HttpPost]
    [Authorize]
    [AutoValidateAntiforgeryToken]
    public async Task<IActionResult> Create(BookCreate bookCreate)
    {
        Category categoryInDb = await _dbContext.Categories.FirstOrDefaultAsync(n => n.Name == bookCreate.Category);
        // if (categoryInDb == null)
        // {
        //     return Content("Error!");
        // }

        Book newBook = new Book()
        {
            CategoryId = categoryInDb.Id,
            Title = bookCreate.Title,
            Author = bookCreate.Author,
            Image = bookCreate.Image,
            Summary = bookCreate.Summary,
            Price = bookCreate.Price,
            UpdateDate = DateTime.Now
        };
        _dbContext.Books.Add(newBook);
        await _dbContext.SaveChangesAsync();
        return RedirectToAction("Detail", "Book", new { id = newBook.Id });
    }

    [HttpGet]
    [Authorize]
    [AutoValidateAntiforgeryToken]
    public async Task<IActionResult> Update(string id)
    {
        int bookId = Int32.Parse(id);
        var books = await _dbContext.Books.FindAsync(bookId);
        var categoriesName = from n in _dbContext.Categories
                             select n.Name;
        BookUpdate formBookUpdate = new BookUpdate()
        {
            Id = books.Id,
            Title = books.Title,
            Author = books.Author,
            Image = books.Image,
            Summary = books.Summary,
            Price = books.Price,
            Categories = new SelectList(categoriesName)
        };
        return View(formBookUpdate);
    }

    [HttpPost]
    [Authorize]
    [AutoValidateAntiforgeryToken]
    public async Task<IActionResult> Update(BookUpdate bookUpdate)
    {
        Category categoryInDb = await _dbContext.Categories.FirstOrDefaultAsync(n => n.Name == bookUpdate.Category);
        var book = await _dbContext.Books.FindAsync(bookUpdate.Id);
        book.Title = bookUpdate.Title;
        book.Author = bookUpdate.Author;
        book.Image = bookUpdate.Image;
        book.Summary = bookUpdate.Summary;
        book.Price = bookUpdate.Price;
        book.CategoryId = categoryInDb.Id;
        book.UpdateDate = DateTime.Now;
        await _dbContext.SaveChangesAsync();
        string bookId = $"{bookUpdate.Id}";
        return RedirectToAction("Detail", new { id = bookId });
    }
}
