namespace KBM.Identidade.API.Models;

public class UsuarioRespostaLogin
{
    public string AccessToken { get; set; } = string.Empty;
    public double ExpiresIn { get; set; }
    public UsuarioToken UsuarioToken { get; set; } = new();
}
