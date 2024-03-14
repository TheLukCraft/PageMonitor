using PageMonitor.Application.Interfaces;
using System.Runtime.CompilerServices;

namespace PageMonitor.WebAPI.Application.Auth
{
    public static class JwtAuthenticationDataProviderConfiguration
    {
        public static IServiceCollection AddJwtAuthenticationDataProvider(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<CookieSettings>(configuration.GetSection("CookieSettings"));
            services.AddScoped<IAuthenticationDataProvider, JwtAuthenticationDataProvider>();

            return services;
        }
    }
}