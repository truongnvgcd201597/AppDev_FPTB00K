using Microsoft.AspNetCore.Mvc;

namespace FPTBook.Controllers
{
    public class AdminOrderController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
