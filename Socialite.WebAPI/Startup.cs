using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Socialite.WebAPI.Infrastructure.Services;
using Newtonsoft.Json.Serialization;
using Socialite.WebAPI.Startup;
using IdentityServer4.AccessTokenValidation;
using Microsoft.IdentityModel.Logging;

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

            services.AddTransient<IImageUploadService, S3ImageUploadService>();

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder =>
                    {
                        builder
                        .AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                    }
                );
            });

            services
            .AddControllers()
            .AddNewtonsoftJson(json =>
            {
                json.SerializerSettings.ContractResolver = new DefaultContractResolver()
                {
                    NamingStrategy = new SnakeCaseNamingStrategy()
                };
            })
            .SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            services
            .AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
            .AddIdentityServerAuthentication(opts =>
            {
                var configuration = Configuration.GetSection("IdentityServerAuthentication");

                opts.ApiName = configuration.GetValue<string>("ApiName");

                opts.Authority = configuration.GetValue<string>("Authority");

                opts.RequireHttpsMetadata = configuration.GetValue<bool>("RequireHttpsMetadata");

                opts.SupportedTokens = SupportedTokens.Jwt;
            });

            services.AddSocialiteWebApi(Configuration);

            services.AddSocialiteAws(Configuration);

            services.AddSocialiteSwagger();
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

            app.UseCors();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });

            app.UseSocialiteWebApi();

            app.UseSocialiteSwagger();
        }
    }
}
