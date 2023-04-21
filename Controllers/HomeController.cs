using System.Diagnostics;
using FPTBook.Data;
using Microsoft.AspNetCore.Mvc;
using FPTBook.Models;
using Microsoft.EntityFrameworkCore;
using FPTBook.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FPTBook.Controllers;

[AutoValidateAntiforgeryToken]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ApplicationDbContext _appdbContext;
    private ApplicationDbContext _dbContext;

    public HomeController(ApplicationDbContext db)
    {
        _dbContext = db;
    }

   
    
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

    [AutoValidateAntiforgeryToken]
    public async Task<IActionResult> Shop()
    {
        List<Book> books = await _dbContext.Books.ToListAsync();
        return View(books);
    }

    public IActionResult AboutUs()
    {
        return View();
    }
    public IActionResult FAQ()
    {
        return View();
    }
    public IActionResult Contact()
    {
        return View();
    }
}