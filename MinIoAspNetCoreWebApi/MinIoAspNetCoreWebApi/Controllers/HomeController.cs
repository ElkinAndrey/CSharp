using Microsoft.AspNetCore.Mvc;
using Minio.AspNetCore;

namespace MinIoAspNetCoreWebApi.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class HomeController : ControllerBase
	{
		MinIoRepository client;
		public HomeController(IMinioClientFactory factory)
		{
			client = new MinIoRepository(factory);
		}

		[HttpPost]
		[Route("[action]")]
		public async Task Add(string? name)
		{
			if (name == null)
				name = "";

			await client.Add(name);
		}

		[HttpPost]
		[Route("[action]")]
		public async Task<FileStreamResult> Get(string name)
		{
			return await client.Get(name, File);
		}
	}
}