using KBM.Identidade.API.Models;
using KBM.WebAPI.Core.Controllers;
using KBM.WebAPI.Core.Identidade;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace KBM.Identidade.API.Controllers;

[Route("api/identidade")]
public class IdentidadeController : MainController
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly AppSettings _appSettings;

    public IdentidadeController(UserManager<IdentityUser> userManager,
                                SignInManager<IdentityUser> signInManager,
                                IOptions<AppSettings> appSettings)
    {
        _userManager = userManager;
        _appSettings = appSettings.Value;
        _signInManager = signInManager;
    }

    [HttpPost("nova-conta")]
    public async Task<ActionResult> Registrar(UsuarioRegistro usuarioRegistro)
    {
        if (!ModelState.IsValid) return CustomResponse(ModelState);

        var novoUsuario = new IdentityUser
        {
            UserName = usuarioRegistro.Email,
            Email = usuarioRegistro.Email,
            EmailConfirmed = true
        };

        var resultado = await _userManager.CreateAsync(novoUsuario, usuarioRegistro.Senha);

        if (resultado.Succeeded)
            return CustomResponse(await GerarJwt(usuarioRegistro.Email));

        foreach (var erro in resultado.Errors)
        {
            AdicionarErroProcessamento(erro.Description);
        }

        return CustomResponse();
    }

    [HttpPost("autenticar")]
    public async Task<ActionResult> Autenticar(UsuarioLogin usuarioLogin)
    {
        if (!ModelState.IsValid) return CustomResponse(ModelState);

        var resultado = await _signInManager.PasswordSignInAsync(usuarioLogin.Email, usuarioLogin.Senha, false, true);

        if (resultado.Succeeded)        
            return CustomResponse(await GerarJwt(usuarioLogin.Email));        

        if (resultado.IsLockedOut)
        {
            AdicionarErroProcessamento("Usuário temporariamente bloqueado por tentativas inválidas");
            return CustomResponse();
        }

        AdicionarErroProcessamento("Usuário ou Senha incorretos");
        return CustomResponse();
    }


    private async Task<UsuarioRespostaLogin> GerarJwt(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        var claims = await _userManager.GetClaimsAsync(user);

        var identityClaims = await ObterClaimsUsuario(claims, user);
        var encodedToken = CodificarToken(identityClaims);

        return ObterRespostaToken(encodedToken, user, claims);
    }

    private string CodificarToken(ClaimsIdentity identityClaims)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
        var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
        {
            Issuer = _appSettings.Emissor,
            Audience = _appSettings.ValidoEm,
            Subject = identityClaims,
            Expires = DateTime.UtcNow.AddHours(_appSettings.ExpiracaoHoras),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        });

        return tokenHandler.WriteToken(token);
    }

    private UsuarioRespostaLogin ObterRespostaToken(string encodedToken, IdentityUser user, IEnumerable<Claim> claims)
    {
        return new UsuarioRespostaLogin
        {
            AccessToken = encodedToken,
            ExpiresIn = TimeSpan.FromHours(_appSettings.ExpiracaoHoras).TotalSeconds,
            UsuarioToken = new UsuarioToken
            {
                Id = user.Id,
                Email = user.Email,
                Claims = claims.Select(c => new UsuarioClaim { Type = c.Type, Value = c.Value })
            }
        };
    }

    private async Task<ClaimsIdentity> ObterClaimsUsuario(ICollection<Claim> claims, IdentityUser user)
    {
        var userRoles = await _userManager.GetRolesAsync(user);

        claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id));
        claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));
        claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
        claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, ToUnixEpochDate(DateTime.UtcNow).ToString()));
        claims.Add(new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(DateTime.UtcNow).ToString(), ClaimValueTypes.Integer64));
        foreach (var userRole in userRoles)
        {
            claims.Add(new Claim("role", userRole));
        }

        var identityClaims = new ClaimsIdentity();
        identityClaims.AddClaims(claims);

        return identityClaims;
    }

    private static long ToUnixEpochDate(DateTime date)
        => (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);

}
