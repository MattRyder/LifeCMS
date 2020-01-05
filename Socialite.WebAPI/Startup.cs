using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Socialite.WebAPI.Infrastructure.Services;
using Newtonsoft.Json.Serialization;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Socialite.WebAPI.Authorization.Jwt;
using Socialite.WebAPI.Startup;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Socialite.Infrastructure.Data;

namespace Socialite
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
            services.AddMediatR();

            services
            .AddSingleton<IJwtGenerationService, JwtGenerationService>()
            .AddTransient<IImageUploadService, S3ImageUploadService>();

            services
            .AddControllersWithViews()
            .AddNewtonsoftJson(json =>
            {
                json.SerializerSettings.ContractResolver = new DefaultContractResolver()
                {
                    NamingStrategy = new SnakeCaseNamingStrategy()
                };
            })
            .SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            // In production, the React files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/build";
            });

            services.SetupWebApi();

            services.SetupAws(Configuration);

            services.SetupEntityFramework(Configuration);

            services.SetupIdentity(Configuration);

            services.SetupSwagger(Configuration);

            var migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;

            services.AddSocialiteIdentityServer(Configuration, migrationsAssembly);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            var envIsDevelopment = env.EnvironmentName.Equals("Development");

            if (envIsDevelopment)
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseCors(builder =>
            {
                builder.AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod();
            });

            app.UseStaticFiles();

            app.UseIdentityServer();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if (envIsDevelopment)
                {
                    spa.UseReactDevelopmentServer(npmScript: "start");
                }
            });

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Socialite API v1");
            });

            UpdateDatabase(app);
        }

        private void UpdateDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                using (var socialiteDbContext = serviceScope.ServiceProvider.GetService<SocialiteDbContext>())
                {
                    ApplyMigrations(socialiteDbContext);
                }

                using (var identityDbContext = serviceScope.ServiceProvider.GetService<SocialiteIdentityDbContext>())
                {
                   ApplyMigrations(identityDbContext);
                }
            }
        }

        public void ApplyMigrations(DbContext context)
        {
            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }
        }
    }
}
