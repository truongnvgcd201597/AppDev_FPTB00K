using FPTBook.Data;
using FPTBook.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FPTBook.Controllers
{
    public class BooksController : Controller
    {
        private readonly MVCDemoDbContext mvcDemoDbContext;

        public BooksController(MVCDemoDbContext mvcDemoDbContext)
        {
            this.mvcDemoDbContext = mvcDemoDbContext;
        }

        // GET: Books/index
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var books = await mvcDemoDbContext.Books.ToListAsync();
            return View(books);
        }

        // GET: Books/Create
        [HttpGet]
        public IActionResult Create()
        {
            System.Diagnostics.Debug.WriteLine("testuser");
            ViewBag.Publishers = mvcDemoDbContext.Publishers.ToList();
            ViewBag.Categories=mvcDemoDbContext.Categories.ToList();
            return View();

        }
        // POST: Books/Create
        [HttpPost]
        public async Task<IActionResult> Create(Book addABookRequest)
        {
            var book = new Book()
            {
                CategoryID = addABookRequest.CategoryID,
                BookName = addABookRequest.BookName,
                BookPrice = addABookRequest.BookPrice,
                Description = addABookRequest.Description,
                Image = addABookRequest.Image,
                BookCount = addABookRequest.BookCount,
                PublisherID = addABookRequest.PublisherID
            };

            await mvcDemoDbContext.Books.AddAsync(book);
            await mvcDemoDbContext.SaveChangesAsync();

            return RedirectToAction("Create");
        }

    }
}
