using Microsoft.OpenApi.Models;

namespace KBM.Identidade.API.Configuracoes;

public static class SwaggerConfig
{
    public static IServiceCollection AddSwaggerConfiguration(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Kabum Identity API",
                Description = "Essa API tem por objetivo lidar com o gerenciamento de usuários utilizando o AspNetIdentity",
                Contact = new OpenApiContact { Name = "Igor Daflon do Couto" }
            });
        });

        return services;
    }

    public static WebApplication UseSwaggerConfiguration(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
            });
        }

        return app;
    }
}
