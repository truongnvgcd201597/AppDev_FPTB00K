using FPTBook.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace FPTBook.Controllers
{
    public class CustomerHomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}