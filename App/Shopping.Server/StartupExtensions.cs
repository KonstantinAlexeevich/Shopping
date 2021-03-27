using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Shopping.Sales.Storage;
using Shopping.Sales.Storage.EntityFramework;

namespace Shopping.Server
{
    public static class StartupExtensions
    {
        public static IServiceCollection UseNpgsql<TContext>(this IServiceCollection services, string connection) where TContext : DbContext
        {
            return services.AddDbContext<TContext>(options => options.UseNpgsql(connection,
                npgSqlOptions => npgSqlOptions.MigrationsAssembly(typeof(StartupExtensions).Assembly.GetName().Name)));
        }

        public static IServiceCollection UseSqlite<TContext>(this IServiceCollection services, string connection) where TContext : DbContext
        {
            return services.AddDbContext<TContext>(options => options.UseSqlite(connection,
                sqlOptions => sqlOptions.MigrationsAssembly(typeof(TContext).Assembly.GetName().Name)));
        }
        
    }
}