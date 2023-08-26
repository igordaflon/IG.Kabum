namespace KBM.Identidade.API.Models;

public class UsuarioToken
{
    public string Id { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public IEnumerable<UsuarioClaim> Claims { get; set; } = Enumerable.Empty<UsuarioClaim>();
}
