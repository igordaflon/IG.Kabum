using KBM.Identidade.API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace KBM.Identidade.API.Controllers;

[Route("api/identidade")]
[ApiController]
public class AuthController : Controller
{
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly UserManager<IdentityUser> _userManager;
    
    public AuthController(SignInManager<IdentityUser> signInManager, 
                          UserManager<IdentityUser> userManager)
    {
        _signInManager = signInManager;
        _userManager = userManager;
    }

    [HttpPost("nova-conta")]
    public async Task<ActionResult> Registrar(UsuarioRegistro usuarioRegistro)
    {
        if(!ModelState.IsValid) return BadRequest();

        var usuario = new IdentityUser
        {
            UserName = usuarioRegistro.Email,
            Email = usuarioRegistro.Email,
            EmailConfirmed = true
        };

        var resultado = await _userManager.CreateAsync(usuario, usuarioRegistro.Senha);

        if (resultado.Succeeded)
        {
            await _signInManager.SignInAsync(usuario, false);
            return Ok();
        }

        return BadRequest();
    }

    [HttpPost("autenticar")]
    public async Task<ActionResult> Login(UsuarioLogin usuarioLogin)
    {
        if (!ModelState.IsValid) return BadRequest();

        var resultado = await _signInManager.PasswordSignInAsync(usuarioLogin.Email, usuarioLogin.Senha, false, true);

        if (resultado.Succeeded)
        {
            return Ok();
        }

        return BadRequest();
    }
}
