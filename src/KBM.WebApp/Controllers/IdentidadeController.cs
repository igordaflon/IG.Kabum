using Microsoft.AspNetCore.Mvc;

namespace KBM.WebApp.Controllers
{
	public class IdentidadeController : Controller
	{
		[HttpGet]
		[Route("login")]
		public IActionResult Login(string? returnUrl = null)
		{
			ViewData["ReturnUrl"] = returnUrl;
			return View();
		}
	}
}
