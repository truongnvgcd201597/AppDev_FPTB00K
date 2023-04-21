using System.Security.Claims;
using FPTBook.Data;
using FPTBook.Models;
using FPTBook.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FPTBook.Controllers;

[Authorize]
[AutoValidateAntiforgeryToken]
public class OrderController : Controller
{
    private readonly ApplicationDbContext _dbContext;
    private UserManager<ApplicationUser> _userManager;

    public OrderController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
    {
        _dbContext = context;
        _userManager = userManager;
    }

    [AutoValidateAntiforgeryToken]
    public IActionResult Index()
    {
        if (HttpContext.User.Identity != null && HttpContext.User.Identity.IsAuthenticated)
        {
            if (HttpContext.User.IsInRole(Role.Owner))
            {
                var user = from u in _dbContext.ApplicationUsers select u;
                var order = from o in _dbContext.Orders select o;
                OrdersDetail orderDetail = new OrdersDetail()
                {
                    Users = user.ToList(),
                    Orders = order.ToList()
                };
                return View(orderDetail);
            }
        }
        var users = from u in _dbContext.ApplicationUsers select u;
        var orders = from o in _dbContext.Orders select o;
        string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        OrdersDetail ordersDetail = new OrdersDetail()
        {
            Users = users.ToList().Where(u => u.Id == userId),
            Orders = orders.ToList().Where(o => o.UserId == userId)
        };
        return View(ordersDetail);
    }

    [HttpGet]
    [AutoValidateAntiforgeryToken]
    public IActionResult Detail(int orderId)
    {
        try
        {
            var order = _dbContext.Orders
                .Include(o => o.OrderOrderedBooks)
                    .ThenInclude(oob => oob.OrderedBook)
                        .ThenInclude(ob => ob.Book)
                .SingleOrDefault(o => o.Id == orderId);

            if (order == null) return NotFound();

            return View(order);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error occurred while getting order details: {ex.Message}");
            return View("Error");
        }
    }


    [HttpGet]
    [AutoValidateAntiforgeryToken]
    public async Task<IActionResult> CheckoutProceed(string id)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var orderedBook = await _dbContext.OrderedBooks
            .Where(u => u.UserId == userId).Include(x => x.Book).ToListAsync();
        return View(orderedBook);
    }
    
    [HttpGet]
    [HttpPost]
    [AutoValidateAntiforgeryToken]
    public async Task<IActionResult> CheckOut(string userId, int totalPrice)
    {
        try
        {
            var orderedBooks = await _dbContext.OrderedBooks
                .Where(ob => ob.UserId == userId && !ob.IsOrdered)
                .ToListAsync();

            foreach (var orderedBook in orderedBooks)
            {
                orderedBook.IsOrdered = true;
            }

            var newOrder = new Order
            {
                UserId = userId,
                TotalPrice = totalPrice,
                IsCheckedOut = true
            };

            var savedOrder = await _dbContext.Orders.AddAsync(newOrder);
            await _dbContext.SaveChangesAsync();

            int orderId = savedOrder.Entity.Id;
            var orderOrderedBooks = orderedBooks
                .Select(ob => new OrderOrderedBook
                {
                    OrderId = orderId,
                    OrderedBookId = ob.Id
                });

            await _dbContext.OrderOrderedBooks.AddRangeAsync(orderOrderedBooks);
            await _dbContext.SaveChangesAsync();

            TempData["SUCCESS"] = "Checked out successfully";
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
        catch (Exception ex)
        {
            TempData["ERROR"] = "An error occurred while checking out.";
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
    }
}