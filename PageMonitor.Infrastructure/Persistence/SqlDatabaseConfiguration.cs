// Ignore Spelling: Sql

using EFCoreSecondLevelCacheInterceptor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PageMonitor.Application.Interfaces;

namespace PageMonitor.Infrastructure.Persistence
{
    public static class SqlDatabaseConfiguration
    {
        public static IServiceCollection AddSqlDatabase(this IServiceCollection services, string connectionString)
        {
            Action<IServiceProvider, DbContextOptionsBuilder> sqloptions = (serviceProvider, options) => options.UseSqlServer(connectionString,
                o => o.UseQuerySplittingBehavior(QuerySplittingBehavior.SingleQuery))
            .AddInterceptors(serviceProvider.GetRequiredService<SecondLevelCacheInterceptor>());

            services.AddDbContext<IApplicationDbContext, MainDbContext>(sqloptions);

            return services;
        }
    }
}