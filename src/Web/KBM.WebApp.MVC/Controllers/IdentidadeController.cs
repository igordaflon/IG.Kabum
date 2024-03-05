﻿using KBM.WebApp.MVC.Models;
using KBM.WebApp.MVC.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace KBM.WebApp.MVC.Controllers;

public class IdentidadeController : MainController
{
    private readonly IAutenticacaoService _autenticacaoService;

    public IdentidadeController(IAutenticacaoService autenticacaoService)
    {
        _autenticacaoService = autenticacaoService;
    }

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

        var resposta = await _autenticacaoService.Registro(usuarioRegistro);

        if(ResponsePossuiErros(resposta.ResponseResult))
            return View(usuarioRegistro);

        await RealizarLogin(resposta);

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

        var resposta = await _autenticacaoService.Login(usuarioLogin);

        if (ResponsePossuiErros(resposta.ResponseResult))
            return View(usuarioLogin);

        await RealizarLogin(resposta);

        return RedirectToAction("Index", "Home");
    }

    [HttpGet]
    [Route("sair")]
    public async Task<IActionResult> Logout()
    {
        return RedirectToAction("Index", "Home");   
    }

    private async Task RealizarLogin(UsuarioRespostaLogin resposta)
    {
        var token = ObterTokenFormatado(resposta.AccessToken);

        var claims = new List<Claim> {
            { new Claim("JWT", resposta.AccessToken) }
        };
        claims.AddRange(token.Claims);

        var claimIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

        var authProperties = new AuthenticationProperties
        {
            ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(60),
            IsPersistent = true
        };

        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimIdentity), authProperties);
    }

    private static JwtSecurityToken ObterTokenFormatado(string jwtToken)
    {
        return new JwtSecurityTokenHandler().ReadToken(jwtToken) as JwtSecurityToken;
    }
}
