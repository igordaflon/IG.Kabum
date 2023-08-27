using KBM.WebAPI.Core.Usuario;

namespace KBM.WebApp.Configurations;

public static class DependencyInjectionConfig
{
	public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
		services.AddScoped<IAspNetUser, AspNetUser>();
	}
}
