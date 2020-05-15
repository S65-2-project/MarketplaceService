using marketplaceservice.DatastoreSettings;
using marketplaceservice.Repositories;
using marketplaceservice.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace marketplaceservice
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //DatabaseSettings
            services.Configure<MarketplaceDatabaseSettings>(
                Configuration.GetSection(nameof(MarketplaceDatabaseSettings)));
            services.AddSingleton<IMarketplaceDatabaseSettings>(sp =>
                sp.GetRequiredService<IOptions<MarketplaceDatabaseSettings>>().Value);

            //Repositories
            services.AddTransient<IMarketplaceRepository, MarketplaceRepository>();
            //Services
            services.AddTransient<IMarketplaceService, MarketplaceService>();
            //Controllers
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}