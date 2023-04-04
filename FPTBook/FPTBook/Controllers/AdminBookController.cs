using Microsoft.AspNetCore.Mvc;

namespace FPTBook.Controllers
{
    public class AdminBookController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
