using Microsoft.AspNetCore.Mvc;

namespace KBM.WebApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
