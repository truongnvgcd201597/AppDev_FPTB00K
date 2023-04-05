using Microsoft.AspNetCore.Mvc;

namespace FPTBook.Models
{
    public class CustomerHomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult AboutUs()
        {
            return View();
        }
    }
}
