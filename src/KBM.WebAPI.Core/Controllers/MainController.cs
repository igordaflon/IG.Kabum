﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace KBM.WebAPI.Core.Controllers;

[ApiController]
public abstract class MainController : Controller
{
    protected ICollection<string> Erros = new List<string>();

    protected ActionResult CustomResponse(object? result = null)
    {
        if (OperacaoValida())
        {
            return Ok(result);
        }

        return BadRequest(new ValidationProblemDetails(new Dictionary<string, string[]>
            {
                { "Mensagens", Erros.ToArray() }
            }));
    }

    protected ActionResult CustomResponse(ModelStateDictionary modelState)
    {
        var erros = modelState.Values.SelectMany(e => e.Errors);
        foreach (var erro in erros)
        {
            AdicionarErroProcessamento(erro.ErrorMessage);
        }

        return CustomResponse();
    }

    protected void AdicionarErroProcessamento(string erro)
    {
        Erros.Add(erro);
    }

    protected bool OperacaoValida()
    {
        return !Erros.Any();
    }
}
