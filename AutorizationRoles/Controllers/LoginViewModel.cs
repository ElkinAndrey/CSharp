using System.ComponentModel.DataAnnotations;

namespace AutorizationRoles.Controllers
{
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
