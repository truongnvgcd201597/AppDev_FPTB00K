using Microsoft.AspNetCore.Mvc;

namespace FPTBook.Controllers
{
    public class CustomerBookController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
