using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace KBM.Identidade.API.Controllers;

[ApiController]
public abstract class MainController : Controller
{
    protected ICollection<string> Erros = new List<string>();

    protected ActionResult CustomReponse(object? resultado = null)
    {
        if (OperacaoValida())
        {
            return Ok(resultado);
        }

        return BadRequest(new ValidationProblemDetails(new Dictionary<string, string[]>
        {
            { "Mensagens", Erros.ToArray() },
        }));
    }

    protected ActionResult CustomReponse(ModelStateDictionary modelState)
    {
        var erros = modelState.Values.SelectMany(e => e.Errors);

        foreach (var erro in erros)
            AdicionarErroProcessamento(erro.ErrorMessage);

        return CustomReponse();
    }

    protected bool OperacaoValida()
        => !Erros.Any();

    protected void AdicionarErroProcessamento(string erro)
        => Erros.Add(erro);

    protected void LimparErroProcessamento()
        => Erros.Clear();
}
