using Microsoft.AspNetCore.Mvc;

namespace FPTBook.Controllers
{
    public class HomePageController : Controller
    {
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
}
