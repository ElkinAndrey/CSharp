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
            return Ok("�������� ���� �� �������������� �������������");
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet]
        [Route("AllAuthorized")]
        public IActionResult AllAuthorized()
        {
            return Ok("�������� ���� ��������������");
        }

        [Authorize(AuthenticationSchemes = "Bearer", Policy = "FirstSection")]
        [HttpGet]
        [Route("FirstSection")]
        public IActionResult FirstSection()
        {
            return Ok("������� ��������� ������ ������� ��������� � ��������������");
        }

        [Authorize(AuthenticationSchemes = "Bearer", Policy = "SecondSection")]
        [HttpGet]
        [Route("SecondSection")]
        public IActionResult SecondSection()
        {
            return Ok("������� ��������� ������ ������� ��������� � ��������������");
        }

        [Authorize(AuthenticationSchemes = "Bearer", Policy = "Administrator")]
        [HttpGet]
        [Route("Admin")]
        public IActionResult Admin()
        {
            return Ok("������� ��������� ������ ��������������");
        }
    }
}