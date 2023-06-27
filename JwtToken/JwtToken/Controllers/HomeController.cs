using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace JwtToken.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        [Route("AllowAnonymous")]
        public IActionResult AllowAnonymous()
        {
            return Ok("Доступно даже не авторизованным пользователям");
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet]
        [Route("AllAuthorized")]
        public IActionResult AllAuthorized()
        {
            return Ok("Доступно всем авторизованным");
        }

        [Authorize(AuthenticationSchemes = "Bearer", Policy = "FirstSection")]
        [HttpGet]
        [Route("FirstSection")]
        public IActionResult FirstSection()
        {
            return Ok("Область доступная только первому менеджеру и администратору");
        }

        [Authorize(AuthenticationSchemes = "Bearer", Policy = "SecondSection")]
        [HttpGet]
        [Route("SecondSection")]
        public IActionResult SecondSection()
        {
            return Ok("Область доступная только второму менеджеру и администратору");
        }

        [Authorize(AuthenticationSchemes = "Bearer", Policy = "Administrator")]
        [HttpGet]
        [Route("Admin")]
        public IActionResult Admin()
        {
            return Ok("Область доступная только администратору");
        }
    }
}