﻿using Microsoft.AspNetCore.Authentication.Cookies;

namespace KBM.WebApp.MVC.Configuration;

public static class IdentityConfig
{
    public static void AddIdentityConfiguration(this IServiceCollection services)
    {
        services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
                options.LoginPath = "/login";
                options.AccessDeniedPath = "/acesso-negado";
            });
    }

    public static void UseIdentityConfiguration(this WebApplication app)
    {        
        app.UseAuthentication();
        app.UseAuthorization();
    }
}
