using Microsoft.AspNetCore.Mvc;

namespace AutorizationRoles.Controllers
{
    public class RecController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.Name = User.Identity.Name;
            return View();
        }
    }
}
