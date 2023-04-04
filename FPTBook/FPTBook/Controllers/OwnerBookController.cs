using Microsoft.AspNetCore.Mvc;

namespace FPTBook.Controllers
{
    public class OwnerBookController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
