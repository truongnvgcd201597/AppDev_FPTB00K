using Microsoft.AspNetCore.Mvc;

namespace FPTBook.Controllers
{
    public class CustomerAccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
