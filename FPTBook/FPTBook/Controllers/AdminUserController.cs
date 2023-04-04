using Microsoft.AspNetCore.Mvc;

namespace FPTBook.Controllers
{
    public class AdminUserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
