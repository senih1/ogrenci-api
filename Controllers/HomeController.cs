using Microsoft.AspNetCore.Mvc;
using ogrenci_sistemi.Models;
using System.Diagnostics;

namespace ogrenci_sistemi.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [Route("/form")]
        public IActionResult Form()
        {
            return View();
        }
    }
}
