using FPTBook.Data;
using FPTBook.Models;
using FPTBook.ViewModels.Category;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FPTBook.Controllers;

[Authorize]
[AutoValidateAntiforgeryToken]
public class BookCategoryController : Controller
{
    private ApplicationDbContext _dbContext;
    private readonly ILogger<BookCategoryController> _logger;

    public BookCategoryController(ApplicationDbContext db)
    {
        _dbContext = db;
    }

    [AutoValidateAntiforgeryToken]
    public async Task<IActionResult> Index()
    {
        var categories = await _dbContext.Categories.ToListAsync();
        return View(categories);
    }

    [AutoValidateAntiforgeryToken]
    public async Task<IActionResult> Detail(string id)
    {
        try
        {
            int categoryId;
            if (!Int32.TryParse(id, out categoryId))
            {
                return Content("Invalid category ID");
            }

            var category = await _dbContext.Categories.FindAsync(categoryId);

            if (category == null)
            {
                return Content("This category doesn't exist");
            }

            return View(category);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while getting the category details");
            return Content("An error occurred while getting the category details");
        }
    }


    [HttpGet]
    [AutoValidateAntiforgeryToken]
    public async Task<IActionResult> Create()
    {
        CategoryCreate formCreateCategory = new CategoryCreate();
        return View(formCreateCategory);
    }

    [HttpPost]
    [AutoValidateAntiforgeryToken]
    public async Task<IActionResult> Create(CategoryCreate categoryCreate)
    {
        if (ModelState.IsValid)
        {
            Category newCategory = new Category()
            {
                Name = categoryCreate.Name
            };
            var savedCategory = await _dbContext.Categories.AddAsync(newCategory);
            await _dbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        return RedirectToAction("Create");
    }

    [AutoValidateAntiforgeryToken]
    public async Task<IActionResult> Delete(string id)
    {
        int categoryId = Int32.Parse(id);
        var categories = await _dbContext.Categories.FindAsync(categoryId);

        if (categories != null)
        {
            _dbContext.Categories.Remove(categories);
            await _dbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        return RedirectToAction("Index");
    }

    [HttpGet]
    [AutoValidateAntiforgeryToken]
    public async Task<IActionResult> Update(string id)
    {
        int categoryId = Int32.Parse(id);
        var categories = await _dbContext.Categories.FindAsync(categoryId);

        CategoryUpdate formUpdateCategory = new CategoryUpdate()
        {
            Id = categories.Id,
            Name = categories.Name
        };
        return View(formUpdateCategory);
    }

    [HttpPost]
    [AutoValidateAntiforgeryToken]
    public async Task<IActionResult> Update(CategoryUpdate categoryUpdate)
    {
        var category = await _dbContext.Categories.FindAsync(categoryUpdate.Id);
        category.Name = categoryUpdate.Name;
        await _dbContext.SaveChangesAsync();
        string cateId = $"{categoryUpdate.Id}";
        return RedirectToAction("Detail", new { id = cateId });
    }
}