using Microsoft.AspNetCore.Mvc;

namespace Example.Controllers
{
	[ApiController]
	[Route("api/Users")]
	public class HomeController : ControllerBase
	{

		[HttpGet]
		public IActionResult GetUsers()
        {
            var remoteIpAddress = HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
            var Users = new[]
			{
				new { Name = "Oleg" },
				new { Name = "Ivan" },
			};

			return Ok(Users);
		}
	}
}