using KBM.WebApp.MVC.Models;
using Microsoft.AspNetCore.Components.RenderTree;
using Microsoft.AspNetCore.Mvc;

namespace KBM.WebApp.MVC.Controllers;

public class IdentidadeController : Controller
{
    [HttpGet]
    [Route("nova-conta")]
    public IActionResult Registro()
    {
        return View();
    }

    [HttpPost]
    [Route("nova-conta")]
    public async Task<IActionResult> Registro(UsuarioRegistro usuarioRegistro)
    {
        if(!ModelState.IsValid) return View(usuarioRegistro);

        return RedirectToAction("Index", "Home");
    }

    [HttpGet]
    [Route("login")]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login(UsuarioLogin usuarioLogin)
    {
        if (!ModelState.IsValid) return View(usuarioLogin);

        return RedirectToAction("Index", "Home");
    }

    [HttpGet]
    [Route("sair")]
    public async Task<IActionResult> Logout()
    {
        return RedirectToAction("Index", "Home");   
    }
}
