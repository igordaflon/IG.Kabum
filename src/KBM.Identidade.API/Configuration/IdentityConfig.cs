using KBM.WebAPI.Core.Identidade;

namespace KBM.Identidade.API.Configuration;

public static class IdentityConfig
{
    public static IServiceCollection AddIdentityConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddJwtConfiguration(configuration);

        return services;
    }
}
