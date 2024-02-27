﻿using KBM.WebApp.MVC.Models;
using System.Text;
using System.Text.Json;

namespace KBM.WebApp.MVC.Services;

public class AutenticacaoService : IAutenticacaoService
{
    private readonly HttpClient _httpClient;

    public AutenticacaoService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<UsuarioRespostaLogin> Login(UsuarioLogin usuarioLogin)
    {
        var loginContent = new StringContent(
            JsonSerializer.Serialize(usuarioLogin),
            Encoding.UTF8,
            "application/json");

        var response = await _httpClient.PostAsync("https://localhost:7016/api/identidade/autenticar", loginContent);

        return JsonSerializer.Deserialize<UsuarioRespostaLogin>(await response.Content.ReadAsStringAsync(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
    }

    public async Task<UsuarioRespostaLogin> Registro(UsuarioRegistro usuarioRegistro)
    {
        var registroContent = new StringContent(
            JsonSerializer.Serialize(usuarioRegistro),
            Encoding.UTF8,
            "application/json");

        var response = await _httpClient.PostAsync("https://localhost:7016/api/identidade/nova-conta", registroContent);

        return JsonSerializer.Deserialize<UsuarioRespostaLogin>(await response.Content.ReadAsStringAsync());
    }
}