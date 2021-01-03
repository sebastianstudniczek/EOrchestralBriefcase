using EOrchestralBriefcase.Application;
using EOrchestralBriefcase.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using MicroElements.Swashbuckle.FluentValidation;
using Swashbuckle.AspNetCore.Swagger;
using FluentValidation.AspNetCore;
using EOrchestralBriefcase.WebAPI.Filters;

namespace EOrchestralBriefcase.WebAPI
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplication();
            services.AddInfrastructure(Configuration);
            //services.AddCors(options =>
            //{
            //    options.AddPolicy("Open", options => options.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
            //});

            services
                .AddControllers(options => options.Filters.Add(new ApiExceptionFilter()))
                .AddFluentValidation(config =>
                    config.ValidatorFactoryType = typeof(HttpContextServiceProviderValidatorFactory));


            services.AddRouting(options => options.LowercaseUrls = true);

            services.AddSwaggerGen(config =>
            {
                config.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "EOrchestral Briefcase API"
                });
                config.AddFluentValidationRules();
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            //app.UseCors("Open");
            //app.UseAuthentication();
            //app.UseAuthorization();

            app.UseSwagger();
            app.UseSwaggerUI(config =>
            {
                config.SwaggerEndpoint("v1/swagger.json", "EOrchestral Briefcase API");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
