using Microsoft.AspNetCore.Mvc;

namespace KBM.Catalogo.API.Controllers
{
    public class CatalogoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
