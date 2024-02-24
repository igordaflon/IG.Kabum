using KBM.WebApp.MVC.Models;

namespace KBM.WebApp.MVC.Services;

public interface IAutenticacaoService
{
    Task<string> Login(UsuarioLogin usuarioLogin);
    Task<string> Registro(UsuarioRegistro usuarioRegistro);
}
