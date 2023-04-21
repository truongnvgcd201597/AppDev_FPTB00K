using FPTBook.Data;
using FPTBook.Models;
using FPTBook.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FPTBook.Controllers;

[Authorize]
[AutoValidateAntiforgeryToken]
public class CategoryRequestController : Controller
{
    private ApplicationDbContext _dbContext;

    public CategoryRequestController(ApplicationDbContext db)
    {
        _dbContext = db;
    }

    [AutoValidateAntiforgeryToken]
    public async Task<IActionResult> Index()
    {
        var categories = await _dbContext.CategoryRequests.ToListAsync();
        return View(categories);
    }
    
    [AutoValidateAntiforgeryToken]
    public async Task<IActionResult> Reject(int id)
    {
        var categories = await _dbContext.CategoryRequests.FindAsync(id);

        if (categories != null)
        {
            _dbContext.CategoryRequests.Remove(categories);
            await _dbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        return RedirectToAction("Index");
    }
    
    [HttpGet]
    [AutoValidateAntiforgeryToken]
    public async Task<IActionResult> Create()
    {
        CategoryRequestCreate formCreateCategory = new CategoryRequestCreate();
        return View(formCreateCategory);
    }

    [HttpPost]
    [AutoValidateAntiforgeryToken]
    public async Task<IActionResult> Create(CategoryRequestCreate categoryCreate)
    {
        if (ModelState.IsValid)
        {
            CategoryRequest newCategory = new CategoryRequest()
            {
                Name = categoryCreate.Name,
                CreatedAt = DateTime.Now,
                Is_Approved = false
            };
            var savedCategory = await _dbContext.CategoryRequests.AddAsync(newCategory);
            await _dbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        return RedirectToAction("Create");
    }
    
    [HttpGet]
    [HttpPost]
    [AutoValidateAntiforgeryToken]
    public async Task<IActionResult> Confirm(int id)
    {
        var categories = await _dbContext.CategoryRequests.FindAsync(id);
        var category = await _dbContext.Categories.FirstOrDefaultAsync(c => categories != null && c.Name == categories.Name);
        var cate = await _dbContext.Categories.OrderByDescending(c => c.Id).FirstOrDefaultAsync();
        int categoryId = cate!.Id;

        if (categories != null && category == null)
        {
            var newCate = new Category()
            {
                Id = categoryId + 1,
                Name = categories.Name
            };
            var saveCate = await _dbContext.Categories.AddAsync(newCate);
        }

        var removeRequest = _dbContext.CategoryRequests.Remove(categories);
        await _dbContext.SaveChangesAsync();
        return RedirectToAction("Index", "CategoryRequest");
    }
}