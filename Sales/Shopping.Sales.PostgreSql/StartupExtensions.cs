using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Shopping.Sales.Storage;
using Shopping.Sales.Storage.EntityFramework;

namespace Shopping.Storage.PostgreSql
{
    public static class StartupExtensions
    {
        public static IServiceCollection UseNpgsql<TContext>(this IServiceCollection services, string connection) where TContext : DbContext
        {
            return services.AddDbContext<TContext>(options => options.UseNpgsql(connection));
        }

        public static DbContextOptionsBuilder UseNpgsql(this DbContextOptionsBuilder dbOptions, string connection)
        {
            return dbOptions.UseNpgsql(connection,
                options => options.MigrationsAssembly(typeof(StartupExtensions).Assembly.GetName().Name));
        }
    }
}