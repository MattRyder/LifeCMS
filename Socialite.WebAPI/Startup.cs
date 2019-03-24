using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Logging;
using Newtonsoft.Json.Serialization;
using Socialite.Domain.AggregateModels.PostAggregate;
using Socialite.Domain.AggregateModels.StatusAggregate;
using Socialite.Domain.AggregateModels.UsersAggregate;
using Socialite.Infrastructure.Data;
using Socialite.Infrastructure.Repositories;
using Socialite.WebAPI.Application.Commands.Posts;
using Socialite.WebAPI.Application.Commands.Statuses;
using Socialite.WebAPI.Application.Commands.Users;
using Socialite.WebAPI.Application.Queries.Posts;
using Socialite.WebAPI.Application.Queries.Users;
using Socialite.WebAPI.Queries.Posts;
using Socialite.WebAPI.Queries.Statuses;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.AspNetCore.Authorization;
using Socialite.WebAPI.Authorization.IdentityServer;
using Socialite.WebAPI.Authorization.Handlers;
using Socialite.Domain.AggregateModels.AlbumAggregate;
using Socialite.WebAPI.Application.Commands.Albums;
using Amazon.S3;
using Socialite.WebAPI.Infrastructure.Services;
using Socialite.WebAPI.Application.Queries.Albums;

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
            var connectionString = Configuration["ConnectionStrings:Socialite"];

            services.Configure<S3ImageUploadOptions>(Configuration.GetSection("AmazonS3"));

            IdentityModelEventSource.ShowPII = true;

            services.AddMediatR();

            services.AddDefaultAWSOptions(Configuration.GetAWSOptions());
            services.AddAWSService<IAmazonS3>();

            services.AddEntityFrameworkSqlServer().AddDbContext<SocialiteDbContext>(opts =>
            {
                opts.UseMySql(connectionString);
            });

            services
            .AddSingleton<IAuthorizationHandler, HasScopeHandler>()
            .AddTransient<IImageUploadService, S3ImageUploadService>()
            .AddTransient<IDbConnectionFactory, MySqlDbConnectionFactory>(f =>
            {
                return new MySqlDbConnectionFactory(connectionString);
            });

            SetupPost(services);
            SetupStatus(services);
            SetupUser(services);
            SetupAlbum(services);

            services
                .AddIdentityServer(configuration =>
                {
                    configuration.IssuerUri = "http://localhost:5000";
                })
                .AddInMemoryApiResources(Config.GetApis())
                .AddInMemoryIdentityResources(Config.GetIdentityResources())
                .AddInMemoryClients(Config.GetClients())
                .AddDeveloperSigningCredential();

            services
            .AddAuthorization(options =>
            {
                options.AddPolicy("StatusReadPolicy", policy => policy.Requirements.Add(new HasScopeRequirement("status:read", "http://localhost:5000")));
            })
            .AddAuthentication("Bearer")
            .AddJwtBearer("Bearer", configureOptions =>
            {
                configureOptions.Audience = "http://localhost:5000";
                configureOptions.Authority = "http://localhost:5000";
                configureOptions.RequireHttpsMetadata = false;
            });

            services.AddMvc()
            .AddJsonOptions(json =>
            {
                json.SerializerSettings.ContractResolver = new DefaultContractResolver()
                {
                    NamingStrategy = new SnakeCaseNamingStrategy()
                };
            })
            .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Version = "1", Title = "Socialite API" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseAuthentication();

            app.UseIdentityServer();

            app.UseHttpsRedirection();

            app.UseMvc();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Socialite API v1");
            });
        }

        private void SetupStatus(IServiceCollection services)
        {
            services.AddTransient<IStatusRepository, StatusRepository>()
                    .AddTransient<IRequestHandler<CreateStatusCommand, bool>, CreateStatusCommandHandler>()
                    .AddTransient<IStatusQueries, StatusQueries>();
        }

        private void SetupPost(IServiceCollection services)
        {
            services.AddTransient<IPostRepository, PostRepository>()
                    .AddTransient<IRequestHandler<CreatePostCommand, bool>, CreatePostCommandHandler>()
                    .AddTransient<IPostQueries, PostQueries>();
        }

        private void SetupUser(IServiceCollection services)
        {
            services.AddTransient<IUserRepository, UserRepository>()
                    .AddTransient<IRequestHandler<CreateUserCommand, bool>, CreateUserCommandHandler>()
                    .AddTransient<IUserQueries, UserQueries>();
        }

        private void SetupAlbum(IServiceCollection services)
        {
            services.AddTransient<IAlbumRepository, AlbumRepository>()
                    .AddTransient<IRequestHandler<CreateAlbumCommand, bool>, CreateAlbumCommandHandler>()
                    .AddTransient<IRequestHandler<UploadPhotoCommand, bool>, UploadPhotoCommandHandler>()
                    .AddTransient<IAlbumQueries, AlbumQueries>();
        }
    }
}
