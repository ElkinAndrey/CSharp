using Microsoft.AspNetCore.Mvc;

namespace AutorizationRoles.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
