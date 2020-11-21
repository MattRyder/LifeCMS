using System.Linq;
using System.Reflection;
using IdentityServer4.Configuration;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using LifeCMS.Services.Identity.API.Application.Commands.Identity;
using LifeCMS.Services.Identity.API.Authorization.IdentityServer;
using LifeCMS.Services.Identity.Infrastructure;
using LifeCMS.Services.Identity.Infrastructure.Data;
using LifeCMS.Services.Identity.Infrastructure.Exensions;
using LifeCMS.Services.Identity.Infrastructure.Responses;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LifeCMS.Services.Identity.API.Extensions
{
    public static partial class StartupExtensions
    {
        public static void AddLifeCMSIdentityServer(this IServiceCollection services, IConfiguration configuration)
        {
            var identityServerDbConnectionString = configuration.GetConnectionString("LifeCMSIdentityServer");

            var migrationsAssembly = typeof(LifeCMSIdentityDbContext).GetTypeInfo().Assembly.GetName().Name;

            services
            .AddTransient<IRequestHandler<CreateIdentityUserCommand, BasicResponse>, CreateIdentityUserCommandHandler>()
            .AddTransient<IRequestHandler<LoginIdentityUserCommand, BasicResponse>, LoginIdentityUserCommandHandler>()
            .AddTransient<IRequestHandler<LogoutIdentityUserCommand, BasicResponse>, LogoutIdentityUserCommandHandler>();

            var builder = services
            .AddIdentityServer(options =>
            {
                options.Events.RaiseErrorEvents = true;
                options.Events.RaiseInformationEvents = true;
                options.Events.RaiseFailureEvents = true;
                options.Events.RaiseSuccessEvents = true;

                options.UserInteraction = new UserInteractionOptions
                {
                    LoginUrl = "/accounts/login",
                    LogoutUrl = "/accounts/logout",
                    ErrorUrl = "/accounts/error"
                };
            })
            .LoadSigningCredential(
                configuration.GetValue<string>("Certificates:SigningCredential")
            )
            .AddAspNetIdentity<LifeCMSIdentityUser>()
            .AddConfigurationStore(options =>
            {
                options.ConfigureDbContext = db =>
                    db.UseMySql(
                        identityServerDbConnectionString,
                        sql => sql.MigrationsAssembly(migrationsAssembly)
                    );
            })
            .AddOperationalStore(options =>
            {
                options.ConfigureDbContext = db =>
                    db.UseMySql(
                        identityServerDbConnectionString,
                        sql => sql.MigrationsAssembly(migrationsAssembly)
                    );
            });


            builder.Services.ConfigureExternalCookie(options =>
            {
                options.Cookie.IsEssential = true;
                options.Cookie.SameSite = SameSiteMode.Unspecified;
            });

            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.IsEssential = true;
                options.Cookie.SameSite = SameSiteMode.Unspecified;
            });
        }

        public static void UseLifeCMSIdentityServer(this IApplicationBuilder app, IConfiguration configuration)
        {
            app.UseIdentityServer();

            app.UseCors("CorsPolicy");

            app.ApplyMigrations<PersistedGrantDbContext>();

            app.ApplyMigrations<ConfigurationDbContext>();

            EnsureSeedData(
                app,
                configuration.GetSection("ApiResources"),
                configuration.GetSection("Clients")
            );
        }

        private static async void EnsureSeedData(
            IApplicationBuilder app,
            IConfigurationSection apiResourcesConfigurationSection,
            IConfigurationSection clientsConfigurationSection
        )
        {
            using var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();

            using var context = serviceScope.ServiceProvider.GetService<ConfigurationDbContext>();

            if (!await context.ApiResources.AnyAsync())
            {
                Config
                .GetApiResources(apiResourcesConfigurationSection)
                .Select((apiResourceModel) => apiResourceModel.ToEntity())
                .ToList()
                .ForEach((apiResourceEntity) => context.ApiResources.Add(apiResourceEntity));

                context.SaveChanges();
            }

            if (!await context.Clients.AnyAsync())
            {
                Config
                .GetClients(clientsConfigurationSection)
                .Select((clientModel) => clientModel.ToEntity())
                .ToList()
                .ForEach((clientEntity) => context.Clients.Add(clientEntity));

                context.SaveChanges();
            }
        }
    }
}
