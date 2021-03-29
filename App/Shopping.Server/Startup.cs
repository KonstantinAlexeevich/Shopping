using System;
using Domain.Foundation.DependencyInjection;
using Domain.Foundation.EventStore.DependencyInjection;
using EventStore.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Shopping.Sales;
using Shopping.Sales.Infrastructure;
using Shopping.Sales.Orders;
using Shopping.Server.Areas.Identity;
using Shopping.Server.Data;

namespace Shopping.Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
            services.UseSqlite<ApplicationDbContext>(Configuration.GetConnectionString("SqlLiteConnection"));
            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddRazorPages();
            services.AddServerSideBlazor();
            services
                .AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<IdentityUser>
                >();
            services.AddDatabaseDeveloperPageExceptionFilter();
            services.AddSingleton<WeatherForecastService>();

            services.AddSingleton(
                ctx
                    => ConfigureEventStore(
                        Configuration.GetConnectionString("EventStoreConnection"),
                        ctx.GetService<ILoggerFactory>()
                    )
            );

            services.AddDomainFoundation(x => x.AddAssemblies(typeof(Order).Assembly));
            services.AddEventNames(x =>
            {
                x.NameSalesEvents();
            });
            services.AddSalesAggregateStores();
        }

        static EventStoreClient ConfigureEventStore(string connectionString, ILoggerFactory loggerFactory)
        {
            var settings = EventStoreClientSettings.Create(connectionString);
            settings.ConnectionName = "ShoppingApp";
            settings.LoggerFactory = loggerFactory;
            return new EventStoreClient(settings);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}