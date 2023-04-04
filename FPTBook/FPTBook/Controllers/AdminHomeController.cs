using Microsoft.AspNetCore.Mvc;

namespace FPTBook.Controllers
{
    public class AdminHomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
