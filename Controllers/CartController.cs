using System.Security.Claims;
using FPTBook.Data;
using FPTBook.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace FPTBook.Controllers;

[Authorize]
[AutoValidateAntiforgeryToken]
public class CartController : Controller
{
    private readonly ApplicationDbContext _dbContext;
    private readonly UserManager<ApplicationUser> _userManager;
    

    public CartController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
    {
        _dbContext = context;
        _userManager = userManager;
    }
    
    [AutoValidateAntiforgeryToken]
    public async Task<IActionResult> Index()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var orderedBook = await _dbContext.OrderedBooks
             .Where(u => u.UserId == userId).Include(x => x.Book).ToListAsync();

        return View(orderedBook);
    }
    [HttpGet]
    [HttpPost]
    [Authorize]
    [AutoValidateAntiforgeryToken]
    public async Task<IActionResult> Create(int bookId, string userId)
    {
        if (!ModelState.IsValid)
        {
            return RedirectToAction("Index", "Book");
        }
        var existingBook = await _dbContext.OrderedBooks.FirstOrDefaultAsync(x => x.BookId == bookId && x.UserId == userId && x.IsOrdered == false);
        if (existingBook == null)
        {
            OrderedBook orderedBook = new OrderedBook()
            { 
                UserId = userId,
                BookId = bookId,
                Quantity = 1,
                IsOrdered = false
            };
            var saveBook = await _dbContext.OrderedBooks.AddAsync(orderedBook);
            await _dbContext.SaveChangesAsync();
            return RedirectToAction("Index", "Book");
        }
        existingBook.Quantity += 1;
        await _dbContext.SaveChangesAsync();
        return RedirectToAction("Index", "Book");
    }

    [HttpGet]
    [HttpPost]
    [AutoValidateAntiforgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        var orderedBook = await _dbContext.OrderedBooks.FindAsync(id);
        var deleteBook = _dbContext.OrderedBooks.Remove(orderedBook);
        await _dbContext.SaveChangesAsync();
        return RedirectToAction("Index");
    }

    [AutoValidateAntiforgeryToken]
    public async Task<IActionResult> IncreaseQuantity(int id)
    {
        var cartBookInDb = await _dbContext.OrderedBooks.FindAsync(id);
        if (cartBookInDb == null) return NotFound();

        cartBookInDb.Quantity++;
        await _dbContext.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    [AutoValidateAntiforgeryToken]
    public async Task<IActionResult> DecreaseQuantity(int id)
    {
        var cartBookInDb = await _dbContext.OrderedBooks.FindAsync(id);
        if (cartBookInDb == null) return NotFound();

        if (--cartBookInDb.Quantity < 1)
            _dbContext.OrderedBooks.Remove(cartBookInDb);

        await _dbContext.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }
}