using Microsoft.Extensions.DependencyInjection;
using PageMonitor.Application.Interfaces;
using PageMonitor.Application.Services;

namespace PageMonitor.Application
{
    public static class DefaultDIConfiguration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<ICurrentAccountProvider, CurrentAccountProvider>();
            return services;
        }
    }
}