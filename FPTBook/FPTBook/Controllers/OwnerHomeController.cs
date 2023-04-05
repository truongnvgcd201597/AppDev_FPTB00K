using Microsoft.AspNetCore.Mvc;

namespace FPTBook.Models
{
    public class OwnerHomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
