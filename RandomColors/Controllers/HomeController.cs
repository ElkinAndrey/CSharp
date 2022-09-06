using Microsoft.AspNetCore.Mvc;
using RandomColors.Models;

namespace RandomColors.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            System.Drawing.Color color = Colors.Color();
            return View(color);
        }
    }
}
