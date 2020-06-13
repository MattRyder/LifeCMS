using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;
using IdentityServer4.AccessTokenValidation;
using LifeCMS.Services.ContentCreation.API.Infrastructure.Services;
using LifeCMS.Services.ContentCreation.API.Infrastructure.Websocket;
using LifeCMS.Services.ContentCreation.API.Infrastructure.Policies;

namespace LifeCMS.Services.ContentCreation.API.Startup
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
                options.AddPolicy(WebApiCorsPolicy.GetName(), WebApiCorsPolicy.GetPolicy());

                options.AddPolicy(
                    WebsocketCorsPolicy.GetName(),
                    WebsocketCorsPolicy.GetPolicy(
                        allowedOrigins: Configuration.GetSection("Cors:Websocket:AllowedOrigins").Get<string[]>()
                    )
                );
            });

            services.AddSignalR();

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

            services.AddLifeCMSWebApi(Configuration);

            services.AddLifeCMSAws(Configuration);

            services.AddLifeCMSSwagger();
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

            app.UseCors(WebsocketCorsPolicy.GetName());

            app.UseCors(WebApiCorsPolicy.GetName());

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}"
                ).RequireCors(WebApiCorsPolicy.GetName());

                endpoints.MapHub<ContentCreationClient>("/services/websocket");
            });

            app.UseLifeCMSWebApi();

            app.UseLifeCMSSwagger();
        }
    }
}
