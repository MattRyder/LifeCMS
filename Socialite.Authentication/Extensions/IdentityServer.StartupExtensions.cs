using System.Linq;
using System.Reflection;
using IdentityServer4.Configuration;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Socialite.Authentication.Application.Commands.Identity;
using Socialite.Authentication.Application.Responses;
using Socialite.Authentication.Authorization.IdentityServer;
using Socialite.Infrastructure.Data;
using Socialite.Infrastructure.Identity;
using Socialite.WebAPI.Application.Commands.Identity;

namespace Socialite.Authentication.Extensions
{
    public static partial class StartupExtensions
    {
        public static void AddSocialiteIdentityServer(this IServiceCollection services, IConfiguration configuration)
        {
            var identityServerDbConnectionString = configuration.GetConnectionString("SocialiteIdentityServer");

            var migrationsAssembly = typeof(SocialiteDbContext).GetTypeInfo().Assembly.GetName().Name;

            services
            .AddTransient<IRequestHandler<CreateIdentityUserCommand, CommandResponse>, CreateIdentityUserCommandHandler>()
            .AddTransient<IRequestHandler<LoginIdentityUserCommand, CommandResponse>, LoginIdentityUserCommandHandler>()
            .AddTransient<IRequestHandler<LogoutIdentityUserCommand, CommandResponse>, LogoutIdentityUserCommandHandler>();

            services.AddCors(options =>
               {
                   options.AddPolicy("CorsPolicy",
                       builder => builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader());
               });

            services
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
            .AddAspNetIdentity<SocialiteIdentityUser>()
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
            })
            .AddDeveloperSigningCredential();
        }

        public static void UseSocialiteIdentityServer(this IApplicationBuilder app)
        {
            app.UseIdentityServer();

            app.UseCors("CorsPolicy");

            app.ApplyMigrations<PersistedGrantDbContext>();

            app.ApplyMigrations<ConfigurationDbContext>();

            EnsureSeedData(app);
        }

        private static async void EnsureSeedData(IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();

            using var context = serviceScope.ServiceProvider.GetService<ConfigurationDbContext>();

            if (!await context.Clients.AnyAsync())
            {
                Config
                .GetApiResources()
                .Select((apiResourceModel) => apiResourceModel.ToEntity())
                .ToList()
                .ForEach((apiResourceEntity) => context.ApiResources.Add(apiResourceEntity));

                Config
                .GetClients()
                .Select((clientModel) => clientModel.ToEntity())
                .ToList()
                .ForEach((clientEntity) => context.Clients.Add(clientEntity));

                context.SaveChanges();
            }
        }
    }
}
