using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Shopping.Sales.Storage;
using Shopping.Sales.Storage.EntityFramework;
using Shopping.Storage;

namespace Shopping.Sales.Sqlite
{
    public static class StartupExtensions
    {
        public static IServiceCollection UseSqlite<TContext>(this IServiceCollection services, string connection) where TContext : DbContext
        {
            return services.AddDbContext<TContext>(options => options.UseSqlite(connection,
                sqlOptions => sqlOptions.MigrationsAssembly(typeof(TContext).Assembly.GetName().Name)));
        }
    }
}