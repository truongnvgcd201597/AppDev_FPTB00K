using Microsoft.AspNetCore.Mvc;

namespace FPTBook.Controllers
{
    public class OwnerHomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
