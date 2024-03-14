using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace PageMonitor.Infrastructure.Auth
{
    public static class JWTAuthConfiguration
    {
        public static IServiceCollection AddJwtAuth(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<JWTAuthenticationOptions>(configuration.GetSection("JwtAuthentication"));
            services.AddSingleton<JwtManager>();

            return services;
        }
    }
}