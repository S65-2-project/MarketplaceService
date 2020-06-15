using MarketplaceService.DatastoreSettings;
using MarketplaceService.Repositories;
using MarketplaceService.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using marketplaceservice.Helpers;
using marketplaceservice.Settings;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using MessageBroker;
using marketplaceservice.MessageHandlers;

namespace MarketplaceService
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
            // jwt settings
            var jwtSettingsSection = Configuration.GetSection("JwtSettings");
            services.Configure<JwtSettings>(jwtSettingsSection);

            var appSettings = jwtSettingsSection.Get<JwtSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.SecretJWT);
            services.AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                    };
                });

            //DatabaseSettings
            services.Configure<MarketplaceDatabaseSettings>(
                Configuration.GetSection(nameof(MarketplaceDatabaseSettings)));
            services.AddSingleton<IMarketplaceDatabaseSettings>(sp =>
                sp.GetRequiredService<IOptions<MarketplaceDatabaseSettings>>().Value);


            //Repositories
            services.AddTransient<IDelegateRepository, DelegateRepository>();
            services.AddTransient<IDAppRepository, DAppRepository>();
            //Helpers
            services.AddTransient<IJwtIdClaimReaderHelper, JwtIdClaimReaderHelper>();
            //Services
            services.AddTransient<IDelegateService, DelegateService>();
            services.AddTransient<IDAppService, DAppService>();
            //Controllers
            services.AddControllers();
            //Message Consumer
            services.AddMessageConsumer(Configuration["MessageQueueSettings:Uri"],
                "MarketplaceService",
                builder => builder.WithHandler<DeleteUserMessageHandler>("delete-user")
                    .WithHandler<UpdateUserMessageHandler>("update-user"));
            services.AddCors();

            services.AddHealthChecks().AddCheck("healthy", () => HealthCheckResult.Healthy());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod()
                .WithExposedHeaders("X-Pagination")
            );

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

            app.UseHealthChecks("/", new HealthCheckOptions
            {
                Predicate = r => r.Name.Contains("healthy")
            });
        }
    }
}