using System.Diagnostics;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;
using Socialite.Domain.AggregateModels.StatusAggregate;
using Socialite.Domain.Common;
using Socialite.Infrastructure.Data;
using Socialite.Infrastructure.Repositories;
using Socialite.WebAPI.Application.Commands.Status;
using Socialite.WebAPI.Queries.Status;
using Swashbuckle.AspNetCore.Swagger;

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

            services.AddEntityFrameworkSqlServer().AddDbContext<StatusContext>();


            services.AddTransient<IStatusRepository, StatusRepository>()
                    .AddTransient<IDomainEventDispatcher, DomainEventDispatcher>()
                    .AddTransient<IRequestHandler<CreateStatusCommand, bool>, CreateStatusCommandHandler>()
                    .AddTransient<IStatusQueries, StatusQueries>(s =>
                    {
                        return new StatusQueries(Configuration["ConnectionStrings:Socialite"]);
                    });

            services.AddMvc()
            .AddJsonOptions(json =>
            {
                json.SerializerSettings.ContractResolver = new DefaultContractResolver()
                {
                    NamingStrategy = new SnakeCaseNamingStrategy()
                };
            })
            .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

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

            app.UseHttpsRedirection();
            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Socialite API v1");
            });
        }
    }
}
