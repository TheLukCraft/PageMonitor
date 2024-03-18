using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using PageMonitor.Application.Interfaces;
using PageMonitor.Application.Logic.Abstractions;
using PageMonitor.Application.Services;
using PageMonitor.Application.Validators;

namespace PageMonitor.Application
{
    public static class DefaultDIConfiguration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<ICurrentAccountProvider, CurrentAccountProvider>();
            return services;
        }

        public static IServiceCollection AddValidators(this IServiceCollection services)
        {
            services.AddValidatorsFromAssemblyContaining(typeof(BaseQueryHandler));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            return services;
        }
    }
}