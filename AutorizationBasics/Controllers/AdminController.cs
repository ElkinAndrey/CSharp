using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace AutorizationBasics.Controllers
{
    // Указывает, что для класса или метода, к которому применяется этот атрибут, требуется указанная авторизация.
    // Если атрибут над классом, то все методы будут закрыты этим атрибутом
    // При попытке войти без авторизации произойдет ошибка
    // Могут попасть только авторизованные пользователи
    [Authorize] 
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [AllowAnonymous] // Могут попасть не только авторизованные пользователи
        public IActionResult Login(string returnUrl) // Принимает строку, куда передиректить пользователя, когда от залогинется
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous] // Могут попасть не только авторизованные пользователи
        public async Task<IActionResult> Login(LoginViewModel model) // Проверка введенной информации
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var claims = new List<Claim>
            {
                new Claim("Demo", "Value")
            };
            var claimIdentity = new ClaimsIdentity(claims, "Cookie");
            var claimPricipal = new ClaimsPrincipal(claimIdentity);
            await HttpContext.SignInAsync("Cookie", claimPricipal); // Добавление куки

            return Redirect(model.ReturnUrl);
        }

        public IActionResult LogOff()
        {

            HttpContext.SignOutAsync("Cookie"); // Удаление куки
            return Redirect("/Home/Index");
        }
    }

    public class LoginViewModel
    {
        [Required] // Проверка достоверности
        public string UserName { get; set; }

        [Required] // Проверка достоверности
        public string Password { get; set; }

        [Required] // Проверка достоверности
        public string ReturnUrl { get; set; }
    }
}
